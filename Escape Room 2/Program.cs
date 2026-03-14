namespace Escape_Room_2;

class Program
{
    /// <summary>
    /// Hauptmethode, in der er für die Spielwiederholung sich selbst aufruft.
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        PleaseEnterYourNameHere();

        PrintMenu();

        Level.Play();

        Highscores.CalculateScore();
        Highscores.InitializeSheet();
        PrintStatistics();
        PrintHighscores();

        PrintExtraFeatures();

        Level.SetDefault();
        Check.SetDefaultValue();
        Highscores.SetDefaultValue();

        Main(args);
    }

    /// <summary>
    /// Bitte geben Sie hier Ihren Namen ein.
    /// </summary>
    private static void PleaseEnterYourNameHere()
    {
        Display(46, 1, "# # # # # # # # # # # # # #");
        Display(46, 2, "# . . . . . . . . . . . . #");
        Display(46, 3, "# . . . Escape Room . . . #");
        Display(46, 4, "# . . . . . . . . . . . . #");
        Display(46, 5, "# # # # # # # # # # # # # #");
        Display(46, 8, "Deine Name: (Für Highscores)");
        Display(46, 10, "");

        Highscores.PlayerName = Console.ReadLine();

        int charCount = 0;

        foreach (char everyChar in Highscores.PlayerName.ToCharArray())
        {
            charCount++;
        }

        if (charCount > 13)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Display(46, 12, "Deine Name darf höchstens 13 Zeichen haben. Drück Enter!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
            Console.Clear();
            PleaseEnterYourNameHere();
        }
        Console.Clear();
    }

    /// <summary>
    /// Label, Intro, Steuerung.
    /// </summary>
    private static void PrintMenu()
    {
        Display(46, 1, "# # # # # # # # # # # # # #");
        Display(46, 2, "# . . . . . . . . . . . . #");
        Display(46, 3, "# . . . Escape Room . . . #");
        Display(46, 4, "# . . . . . . . . . . . . #");
        Display(46, 5, "# # # # # # # # # # # # # #");
        Display(10, 8, "Du befindest dich in einem 'Escape Room' und musst verschiedene Rätseln lösen um herauszukommen!");
        Display(10, 10, "Im ersten Level musst du dir das Item sammeln, um die verschlossene Tür zu öffnen.");

        Display(10, 12, "Spielelemente auf der Karte: ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Display(40, 13, "F = Spielfigur");
        Console.ForegroundColor = ConsoleColor.Green;
        Display(40, 14, "I = Item");
        Console.ForegroundColor = ConsoleColor.Red;
        Display(40, 15, "T = Tür ");
        Console.ResetColor();
        Display(40, 16, "# = Wand");
        
        Display(10, 18, "- Bewegung der Spielfigur mit WASD oder Pfeiltasten -");

        Display(46, 22, "> Drück ENTER zu starten <");
        Console.ReadKey();
        Console.Clear();
    }

    /// <summary>
    /// Gratulier-Text, Statistik und 2x Beep. 
    /// </summary>
    private static void PrintStatistics()
    {
        Console.Beep();
        Console.Beep();

        int row = 6;
        int col = Console.BufferWidth / 2 - 28;
        Display(col, row += 2, ">>> Du hast die Flucht aus dem Escape Room geschafft! <<<");
        Display(col, row += 4, $"Im {Level.Mode}-Modus und {Level.j} x {Level.i} Raum {Movement.CountMoves} Züge gebraucht.");

        if (Level.Hard == true)
        {
            if (Quiz.SetGameOver == false)
            {
                Display(col, row += 2, $"Dazu in der Mathe-Prüfung {Quiz.NumTest} Aufgaben gelöst,");
                Display(col, row += 2, $"davon {Quiz.CorrectResult} richtige und {Quiz.WrongResult} falsche Ergebnisse, und {Quiz.UsedCheatSheet} Spickzettel gebraucht.");

                if (Quiz.WrongResult == 0 && Quiz.UsedCheatSheet == 0)
                    Display(col, row += 2, "Und eine 1++ geschrieben! :O");
            }
            else Display(col, row += 2, $"Dazu in der Mathe-Prüfung eine 6 geschrieben! :(");

            if (Quiz.Busted == true)
                Display(col, row += 2, "Allerdings musst du jetzt dem Lehrerzimmer entkommen!");

            Display(col, row += 2, $"Score: {Highscores.Score}");
        }

        Display(Console.BufferWidth / 2 - 7, Console.BufferHeight - 1, "> Drück Enter <");
        Console.ReadKey();
        Console.Clear();
    }

    /// <summary>
    /// Die Highscores ausprinten.
    /// </summary>
    private static void PrintHighscores()
    {
        // 1. Reihe = Hälfte des Bildschirms - Hälfte der Highscore Slots - Hälfte der Überschrift und untenliegende leere Zeile

        int row = Console.BufferHeight / 2 - (int)(Highscores.HighscoresSheet.GetLength(0) / 2) - 1;
        int col = Console.BufferWidth / 2 - 10;

        Display(Console.BufferWidth / 2 - 7, row, "_ Highscores _");
        row++;

        for (int i = 1; i < Highscores.HighscoresSheet.GetLength(0) + 1; i++)
        {
            Display(col, row + i, i + ".");
            Display(col + 3, row + i, Highscores.HighscoresSheetName[i - 1]);
            Display(col + 17, row + i, Highscores.HighscoresSheet[i - 1].ToString());
        }
        Display(Console.BufferWidth / 2 - 7, Console.BufferHeight - 1, "> Drück Enter <");
        Console.ReadKey();
        Console.Clear();
    }

    /// <summary>
    /// Zusätzliche Funktionen und Beep.
    /// </summary>
    private static void PrintExtraFeatures()
    {
        Console.Beep();

        int col = 20;
        int row = Console.BufferHeight / 2 - 14;
        Display(Console.BufferWidth / 2 - 20, row += 2, "- Zusätzliche eingebaute Funktionen -");
        Display(col, row += 2, "");
        Display(col, row += 2, "-> Einführung der 3 Level-Modi");
        Display(col, row += 2, "-> Vorschau des Escape Rooms vor der Bestätigung");
        Display(col, row += 2, "-> Beep-Sounds");
        Display(col, row += 2, "-> Farben für die Spielobjekte");
        Display(col, row += 2, "-> Bewegung des Spielobjekts Item");
        Display(col, row += 2, "-> Quiz-Level: Level-Design und zufällig generierte Mathe Aufgaben mit Spickzetteln");
        Display(col, row += 2, "-> Counter für gebrauchte Züge, gelöste Aufgaben und etc.");
        Display(col, row += 2, "-> Highscores + Spiel-Loop");

        Display(Console.BufferWidth / 2 - 7, Console.BufferHeight - 1, "> Drück Enter <");
        Console.ReadKey();
        Console.Clear();
    }

    /// <summary>
    /// Setzt den Cursor auf Position und gibt Text aus.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="s"></param>
    public static void Display(int x, int y, string s)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(s);
    }
}