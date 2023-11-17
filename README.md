# Nahrungsnetz
Programmierungprojekt F | Nahrungsnetz Generator

## Anforderungen
Man soll 2 Tiere einfügen können, wobei das erste dem zweiten überlegen ist.
Es werden so viele Tiere hinzugefügt wie erwünscht ist, danach wird das Nahrungsnetz generiert.
Wichtig dabei ist, das wenn ein Tier X >1 Eingegeben wird, dass zum Tier X ein weiterer Pfeil hinzugefügt wird, anstatt es als seperates "neues" Tier zu speichern. 
Optionak kann der User das Nahrungsnetz via Screenshot speichern.

## Testfallspezifikation
| Vorbereitung                                     | Eingabe                 | Erwartete Ausgabe                                        |
| -------------------------------------------------|-------------------------|----------------------------------------------------------|
| Programm wird gestartet                              | `Tier 1` isst `Tier 2`  | `Tier 1` Pfeil zu `Tier 2`                               |
| Eingabe wird kontrolliert ob `Tier 1` oder `Tier 2` bereits vorhanden ist | Keine | Wenn `Tier 1` oder `Tier 2` bereits vorhanden ist, wird ein weiterer Pfeil zu `Tier 1` bzw `Tier 2` hinzugefügt, wenn alles in ordnung ist wird `Tier 1` und `Tier 2` im Array gespeichert |
| User gibt ein ob weiteres Tier hinzugefügt wird  | Ja oder Nein               | Wenn Ja = Wiederholng, wenn Nein Nahrungsnetz darstellen |
| User gibt ein ob ein Screenshot des Nahrungsnetzes erstellt werden soll | Ja oder Nein | Wenn Ja = Screenshot wird erstellt und gespeichert, Wenn nein = Programm beendet |
