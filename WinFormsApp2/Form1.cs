namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            // ������ �������� ������� ����
            DrawFoodChainElement(g, new Rectangle(50, 150, 100, 50), "�����");
            DrawFoodChainElement(g, new Rectangle(200, 100, 100, 50), "����������");
            DrawFoodChainElement(g, new Rectangle(200, 200, 100, 50), "����������");
            DrawFoodChainElement(g, new Rectangle(350, 50, 100, 50), "������");
            DrawFoodChainElement(g, new Rectangle(350, 150, 100, 50), "������");
            DrawFoodChainElement(g, new Rectangle(350, 250, 100, 50), "������");
            DrawFoodChainElement(g, new Rectangle(500, 150, 100, 50), "���-������");

            // ������ ������� ����� ����������
            DrawArrow(g, new Point(150, 175), new Point(200, 125));
            DrawArrow(g, new Point(150, 175), new Point(200, 225));
            DrawArrow(g, new Point(300, 125), new Point(350, 75));
            DrawArrow(g, new Point(300, 225), new Point(350, 275));
            DrawArrow(g, new Point(450, 175), new Point(500, 175));
        }

        private void DrawFoodChainElement(Graphics g, Rectangle rect, string text)
        {
            // ������ �������������
            g.FillRectangle(Brushes.LightGreen, rect);
            g.DrawRectangle(Pens.Black, rect);

            // ������ ����� � ������ ��������������
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            g.DrawString(text, Font, Brushes.Black, rect, sf);
        }

        private void DrawArrow(Graphics g, Point start, Point end)
        {
            // ������ �����
            g.DrawLine(Pens.Black, start, end);

            // ��������� ���� ��������
            float angle = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);

            // ������������ ������� �� ���� ��������
            g.TranslateTransform(end.X, end.Y);
            g.RotateTransform(angle * (180 / (float)Math.PI));

            // ������ ����������� ������� � ����� �����
            int arrowSize = 10;
            PointF[] arrowPoints = new PointF[3];
            arrowPoints[0] = new PointF(0, 0);
            arrowPoints[1] = new PointF(-arrowSize, -arrowSize / 2);
            arrowPoints[2] = new PointF(-arrowSize, arrowSize / 2);
            g.FillPolygon(Brushes.Black, arrowPoints);

            // ���������� ������� � �������� ���������
            g.ResetTransform();
        }
    }
}