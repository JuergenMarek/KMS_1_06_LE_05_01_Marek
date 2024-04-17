using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Programm
/// </summary>
class Program
{
    static void Main()
    {
        // Liste mit den vorhandenen Dateien erstellen
        string verzeichnis = Directory.GetCurrentDirectory();
        List<string> txtDateien = Directory.GetFiles(verzeichnis, "*.txt").ToList();

        // Fehlermeldung, wenn keine Datei im Verzeichnis vorhanden ist
        if (txtDateien.Count == 0)
        {
            Console.WriteLine("Keine Dateien im Verzeichnis gefunden.");
            Console.Write("Drücken Sie eine Taste, um das Programm zu beenden.");
            Console.ReadKey();
            return;
        }
        
        while (true)
        {
            // Auflistung der vorhandenen Dateien
            Console.WriteLine("Verfügbare Textdateien:");

            for (int i = 0; i < txtDateien.Count; i++)
            {
                // Ausgabe der Dateien mit ID und Dateiname, um dem Benutzer die Auswahl zu erleichtern
                Console.WriteLine($"{i + 1}: {Path.GetFileName(txtDateien[i])}");
            }

            // Abfrage der Nummer der Datei
            Console.Write("\nBitte geben Sie die Nummer der Datei ein, die Sie anzeigen möchten (x zum Beenden): ");
            string eingabeID = Console.ReadLine();

            // Programmende bei Eingabe von x
            if (eingabeID.ToLower() == "x")
                return;

            // Überprüfung, ob Datei vorhanden ist
            if (!int.TryParse(eingabeID, out int auswahl) || auswahl < 1 || auswahl > txtDateien.Count)
            {
                Console.WriteLine("Ungültige Eingabe. Bitte geben Sie eine gültige Nummer ein.");
                continue;
            }

            // Auswahl der Datei in der Liste
            string ausgewaehlteDatei = txtDateien[auswahl - 1];

            try
            {
                // Anzeige des Inhalts der Datei
                string inhaltDatei = File.ReadAllText(ausgewaehlteDatei);
                Console.WriteLine($"\nInhalt von {Path.GetFileName(ausgewaehlteDatei)}:\n{inhaltDatei}");

                // Abfrage, ob Bearbeitung gewünscht ist
                Console.Write("\nMöchten Sie den Inhalt bearbeiten? (j/n): ");
                string eingabeBearbeiten = Console.ReadLine();

                // Bearbeitung des Dateiinhalts
                if (eingabeBearbeiten.ToLower() == "j")
                {
                    Console.WriteLine("Geben Sie den neuen Text ein:");
                    string neuerText = Console.ReadLine();
                    File.WriteAllText(ausgewaehlteDatei, neuerText);
                    Console.WriteLine();
                    Console.WriteLine("Text erfolgreich aktualisiert.");
                    Console.WriteLine();
                    Console.Write("Drücken Sie eine Taste um fortzusetzen.");
                    Console.ReadKey();
                }
            }
            // Exceptionhandling
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Zugriff auf die Datei '{Path.GetFileName(ausgewaehlteDatei)}' verweigert.");
                Console.Write("Drücken Sie eine Taste, um das Programm zu beenden.");
                Console.ReadKey();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Die Datei '{Path.GetFileName(ausgewaehlteDatei)}' wurde nicht gefunden.");
                Console.Write("Drücken Sie eine Taste, um das Programm zu beenden.");
                Console.ReadKey();
            }
            catch (IOException)
            {
                Console.WriteLine($"Fehler beim Lesen oder Schreiben der Datei '{Path.GetFileName(ausgewaehlteDatei)}'.");
                Console.Write("Drücken Sie eine Taste, um das Programm zu beenden.");
                Console.ReadKey();
            }
            catch (Exception)
            {
                Console.WriteLine($"Ein Fehler ist aufgetreten.");
                Console.Write("Drücken Sie eine Taste, um das Programm zu beenden.");
                Console.ReadKey();
            }
        }
    }
}
