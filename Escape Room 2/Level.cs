namespace Escape_Room_2;

class Level
{
    public static string Mode;

    public static bool SetSize = false; // ist dafür da, damit man die Raumgröße ohne Neustart nochmal verändern kann.
    public static bool GameOver = false;
    public static bool Easy = false;
    public static bool Medium = false;
    public static bool Hard = false;

    public static int RoomSizeMinColumn = 2;
    public static int RoomSizeMinColumnMediumHard = 10;
    public static int RoomSizeMaxColumn = 50;
    public static int RoomSizeMinRow = 2;
    public static int RoomSizeMinRowMediumHard = 10;
    public static int RoomSizeMaxRow = 20;
    public static int j; // angegebene Breite des Raums.
    public static int i; // angegebene Höhe des Raums.

    /// <summary>
    /// Level aufrufen.
    /// </summary>
    public static void Play()
    {
        AskLevelMode();

        while (SetSize != true)
        {
            SetRoomSize();
            Room.CalculateDistance();
            Room.SetArray();
            Room.SetCharsInArray();
            Room.Print();
            AskConfirm();
            Console.Clear();
        }
        Door.Spawn();

        if (Hard == true) Quiz.Spawn();

        Figure.Spawn();
        Item.Spawn();

        Highscores.CalculateShortWay();

        Room.SetCharsInArray();
        Room.Print();

        while (GameOver != true)
        {
            Room.SetCharsInArray();

            Check.PrintRoomAndTipp();
            
            Figure.Print();

            if (Check.ItemIsCollected == false) Item.Print();

            Console.SetCursorPosition(0, 0);

            Movement.PressKey();

            Room.SetCharsInArray(); // damit den Char 'F' gesetzt wird und die Bewegung vom Item nur auf dem Char '.' zugewiesem werden kann.
            Check.ItemCollect();

            if (Easy == false && Check.ItemIsCollected == false) Item.Move();

            Check.QuizCollect();
        }
        Console.Clear();
    }

    /// <summary>
    /// Auswahl Spielmodus von Spieler.
    /// </summary>
    private static void AskLevelMode()
    {
        Console.WriteLine($"{"",-20}Spielmodus:");
        Console.WriteLine("");
        Console.WriteLine($"{"",-20}1 = Easy");
        Console.WriteLine($"{"",-20}2 = Medium");
        Console.WriteLine($"{"",-20}3 = Hard");
        Console.WriteLine("");
        Console.Write($"{"",-20}Gib die Nummer an: ");

        string input = Console.ReadLine();

        int.TryParse(input, out i);

        Console.Clear();

        switch (i)
        {
            case 1: Easy   = true; Mode = "Easy";   return;
            case 2: Medium = true; Mode = "Medium"; return;
            case 3: Hard   = true; Mode = "Hard";   return;
        }
        AskLevelMode();
    }

    /// <summary>
    /// Raumgröße festlegen.
    /// </summary>
    private static void SetRoomSize()
    {
        Console.Write($"{"",-20}Gib die Breite des Raums an (mind. ");

        Console.Write(Easy == true ? RoomSizeMinColumn : RoomSizeMinColumnMediumHard);

        Console.Write($", max. {RoomSizeMaxColumn} Spalten): ");

        string inputj = Console.ReadLine();

        int.TryParse(inputj, out j);


        Console.Write($"{"",-20}Gib die Höhe des Raums an (mind. ");

        Console.Write(Easy == true ? RoomSizeMinRow : RoomSizeMinRowMediumHard);

        Console.Write($", max. {RoomSizeMaxRow} Reihen): ");

        string inputi = Console.ReadLine();

        int.TryParse(inputi, out i);

        if (i >= (Easy == true ? RoomSizeMinRow : RoomSizeMinRowMediumHard) && i <= RoomSizeMaxRow &&
            j >= (Easy == true ? RoomSizeMinColumn : RoomSizeMinColumnMediumHard) && j <= RoomSizeMaxColumn)
        {
            Room.Row = i + 2; // + 2 Wände
            Room.Column = j + 2; // + 2 Wände
            Console.WriteLine();
            return;
        }
        Console.WriteLine();
        Console.WriteLine($"{"",-20}Das sind keine gültige Zahlen.\n");
        Console.WriteLine($"{"",-20}Drück beliebige Taste!");
        Console.ReadKey();
        Console.Clear();
        SetRoomSize();
    }

    /// <summary>
    /// Die Angaben der Raumgröße bestätigen.
    /// </summary>
    private static void AskConfirm()
    {
        Console.WriteLine($"\n{"",-20}- Bestätige mit ENTER oder ändere die Raumgröße mit einer anderen beliebigen Taste -");

        ConsoleKey consolekey = Console.ReadKey().Key;

        if (consolekey == ConsoleKey.Enter)
        {
            SetSize = true;
        }
    }

    /// <summary>
    /// Setzt die Werte zurück
    /// </summary>
    public static void SetDefault()
    {
        SetSize = false; // ist dafür da, damit man die Raumgröße ohne Neustart nochmal verändern kann.
        GameOver = false;
        Easy = false;
        Medium = false;
        Hard = false;

        i = 0; // angegebene Höhe des Raums.
        j = 0; // angegebene Breite des Raums.

        Movement.CountMoves = 0;
        
    }
}