using Escape_Room_2;

class Highscores
{
    public static int[] HighscoresSheet = new int[3]; // Highscore Tabelle mit 3 Slots
    public static string[] HighscoresSheetName = new string[3]; // Highscore Tabelle mit 3 Slots
    public static int QuizPoints;
    public static int Score;
    public static string PlayerName;

    private static int shortWayPoints;

    /// <summary>
    /// Den kürzesten Weg zwischen Figur und Item und Tür ausrechnen
    /// </summary>
    public static void CalculateShortWay()
    {
        int distanceBetweenFigureItem;

        int i = Figure.i - Item.i;
        if (i < 0) i *= -1; // Positive Zahl umwandeln

        int j = Figure.j - Item.j;
        if (j < 0) j *= -1; // Positive Zahl umwandeln 

        int distanceBetweenItemDoor;

        int i2 = Item.i - Door.i;
        if (i2 < 0) i2 *= -1; // Positive Zahl umwandeln

        int j2 = Item.j - Door.j;
        if (j2 < 0) j2 *= -1; // Positive Zahl umwandeln

        distanceBetweenFigureItem = i + j + 1; // 1 für MovesCount vom Rausgehen von Tür nach außen
        distanceBetweenItemDoor = i2 + j2 + 1;
        shortWayPoints = distanceBetweenFigureItem + distanceBetweenItemDoor;
    }

    /// <summary>
    /// Score ausrechnen
    /// </summary>
    public static void CalculateScore()
    {
        if (Level.Mode == "Hard" && Quiz.WrongResult == 0 && Quiz.UsedCheatSheet == 0) QuizPoints++; // Bonus Point

        Score = (DistancePointsInt() + QuizPoints) * MultiplierInt();
    }

    /// <summary>
    /// Höhe des Multiplikators abhängig von Schwierigkeiten
    /// </summary>
    /// <returns></returns>
    private static int MultiplierInt()
    {
        switch (Level.Mode)
        {
            case "Easy":   return 1;
            case "Medium": return 2;
            case "Hard": return 2;
        }
        return -1;
    }

    /// <summary>
    /// Wert > 0 => Bestrafung, Wert = 0 => Gleich, Wert < 0 => Belohnung
    /// </summary>
    /// <param name="distancePoints"></param>
    /// <returns></returns>
    private static int DistancePointsInt()
    {
        int distancePoints = Movement.CountMoves - shortWayPoints;

        switch (distancePoints)
        {
            case > 0: return shortWayPoints - distancePoints;
            case 0: return shortWayPoints;
            case < 0: return shortWayPoints + distancePoints;
        }
    }

    /// <summary>
    /// Initialisiert Highscore Tabelle
    /// </summary>
    public static void InitializeSheet()
    {
        int score = Score;
        string name = PlayerName;

        for (int i = 0; i < HighscoresSheet.Length; i++)
        {
            if (score > HighscoresSheet[i]) // Wenn Punkte höher als 1., 2. oder 3. sind,
            {
                int swapScore = HighscoresSheet[i]; 
                HighscoresSheet[i] = score; // dann werden Punkte dort ersetzt,
                score = swapScore;

                string swapName = HighscoresSheetName[i];
                HighscoresSheetName[i] = name; // sowie der dazugehörte Name.
                name = swapName;
            }
        }
    }

    /// <summary>
    /// Setzt die Werte zurück 
    /// </summary>
    public static void SetDefaultValue()
    {
        QuizPoints = 0;
        Score = 0; ;
        shortWayPoints = 0;
    }
}

