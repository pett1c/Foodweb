using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace WinFormsApp2
{
    [Serializable]
    public class FoodChainData
    {
        public Dictionary<string, Rectangle> Animals { get; set; }
        public List<Tuple<string, string>> FoodLinks { get; set; }
    }

    public partial class Form1 : Form
    {
        private Dictionary<string, Rectangle> animals;
        private List<Tuple<string, string>> foodLinks;
        private TextBox textBoxPredator;
        private TextBox textBoxPrey;
        private Button buttonAddToChain;
        private Button buttonSave;
        private Button buttonLoad;

        public Form1()
        {
            InitializeComponent();
            animals = new Dictionary<string, Rectangle>();
            foodLinks = new List<Tuple<string, string>>();

            // Text-Eingabefelder
            textBoxPredator = new TextBox();
            textBoxPrey = new TextBox();

            // Button zum Hinzufügen zu einem Zweig
            buttonAddToChain = new Button();
            buttonAddToChain.Text = "Add";
            buttonAddToChain.Click += buttonAddToChain_Click;

            // Button zum Speichern von Zweigen
            buttonSave = new Button();
            buttonSave.Text = "Save";
            buttonSave.Click += buttonSave_Click;

            // Button zum Laden von Zweigen
            buttonLoad = new Button();
            buttonLoad.Text = "Load";
            buttonLoad.Click += buttonLoad_Click;

            // Position der Textfelder und Buttons
            textBoxPredator.Location = new Point(10, 10);
            textBoxPrey.Location = new Point(10, 40);
            buttonAddToChain.Location = new Point(10, 70);
            buttonSave.Location = new Point(10, 100);
            buttonLoad.Location = new Point(10, 130);

            // Hinzufügen von Textfeldern und Buttons zu Form
            Controls.Add(textBoxPredator);
            Controls.Add(textBoxPrey);
            Controls.Add(buttonAddToChain);
            Controls.Add(buttonSave);
            Controls.Add(buttonLoad);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Zeichnen eines Zweigs
            DrawFoodChain(g);
        }

        private void buttonAddToChain_Click(object sender, EventArgs e)
        {
            // Abrufen von Werten aus Textfeldern
            string predator = textBoxPredator.Text;
            string prey = textBoxPrey.Text;

            // Hinzufügen einer Verknüpfung zu einem Zweig
            AddFoodLink(predator, prey);

            // Neuzeichnen der Form
            Invalidate();

            // Löschen von Textfeldern
            textBoxPredator.Clear();
            textBoxPrey.Clear();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            saveFileDialog.Title = "Speichern";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                SaveData(saveFileDialog.FileName);
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.Title = "Herunterladen";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                LoadData(openFileDialog.FileName);
            }
        }

        private void SaveData(string filePath)
        {
            FoodChainData data = new FoodChainData
            {
                Animals = animals,
                FoodLinks = foodLinks
            };

            string jsonData = JsonSerializer.Serialize(data);
            File.WriteAllText(filePath, jsonData);
        }

        private void LoadData(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                FoodChainData data = JsonSerializer.Deserialize<FoodChainData>(jsonData);

                animals = data.Animals ?? new Dictionary<string, Rectangle>();
                foodLinks = data.FoodLinks ?? new List<Tuple<string, string>>();

                Invalidate();
            }
        }

        private void DrawFoodChain(Graphics g)
        {
            foreach (var link in foodLinks)
            {
                DrawArrow(g, animals[link.Item1], animals[link.Item2]);
            }

            foreach (var animal in animals)
            {
                DrawFoodChainElement(g, animal.Key, animal.Value);
            }
        }

        private void DrawFoodChainElement(Graphics g, string animal, Rectangle rect)
        {
            g.FillRectangle(Brushes.LightGreen, rect);
            g.DrawRectangle(Pens.Black, rect);

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            g.DrawString(animal, SystemFonts.DefaultFont, Brushes.Black, rect, sf);
        }

        private void DrawArrow(Graphics g, Rectangle startRect, Rectangle endRect)
        {
            // Blockzentren
            Point startCenter = new Point(startRect.Left + startRect.Width / 2, startRect.Top + startRect.Height / 2);
            Point endCenter = new Point(endRect.Left + endRect.Width / 2, endRect.Top + endRect.Height / 2);

            // Coordiantes des Anfangs und des Endes der Linie
            float startX = startRect.Right; // beginnend mit dem rechten Teil von Block 1
            float startY = startCenter.Y;

            float endX = endRect.Left; // Ende links von Block 2
            float endY = endCenter.Y;

            // Linienzeichnung
            g.DrawLine(Pens.Black, startX, startY, endX, endY);

            // Pfeilreihenfolge (zur Verdeutlichung)
            float arrowSize = 6;
            float arrowSpacing = 5;

            float angle = (float)Math.Atan2(endY - startY, endX - startX);
            float length = (float)Math.Sqrt(Math.Pow(endX - startX, 2) + Math.Pow(endY - startY, 2));

            float currentPos = 0;
            while (currentPos < length)
            {
                float arrowX = startX + currentPos * (float)Math.Cos(angle);
                float arrowY = startY + currentPos * (float)Math.Sin(angle);

                g.TranslateTransform(arrowX, arrowY);
                g.RotateTransform(angle * (180 / (float)Math.PI));

                PointF[] arrowPoints = new PointF[3];
                arrowPoints[0] = new PointF(0, 0);
                arrowPoints[1] = new PointF(-arrowSize, -arrowSize / 2);
                arrowPoints[2] = new PointF(-arrowSize, arrowSize / 2);
                g.FillPolygon(Brushes.Black, arrowPoints);

                g.ResetTransform();

                currentPos += arrowSpacing;
            }
        }






        private void AddFoodLink(string predator, string prey)
        {
            if (!animals.ContainsKey(predator))
            {
                animals[predator] = new Rectangle(50 + animals.Count * 150, 150, 100, 50);
            }

            if (!animals.ContainsKey(prey))
            {
                animals[prey] = new Rectangle(50 + animals.Count * 150, 300, 100, 50);
            }

            foodLinks.Add(Tuple.Create(predator, prey));

            // Übertragung bestehender Zweigstellen und Hinzufügen einer neuen Zweigstelle (funktioniert wahrscheinlich nicht)
            int offset = 100;
            foreach (var animal in animals)
            {
                if (animal.Key != predator)
                {
                    animal.Value.Offset(0, offset);
                }
            }
            animals[predator].Offset(0, offset);
        }
    }
}
