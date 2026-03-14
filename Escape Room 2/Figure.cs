namespace Escape_Room_2;

class Figure
{
    // Koordinaten von der Spielfigur.
    public static int i;
    public static int j;

    // Vorherige Position.
    public static int _i;
    public static int _j;

    private static Random rnd = new Random();

    /// <summary>
    /// Gibt eine zufällige Position der Figur an.
    /// </summary>
    public static void Spawn()
    {
        i = rnd.Next(1, Room.Row - 1);
        j = rnd.Next(1, Room.Column - 1);

        if (i == Quiz.i && j == Quiz.j)
            Spawn();

        _i = i;
        _j = j;
    }

    /// <summary>
    /// Setzt den Spielfigur auf Position.
    /// </summary>
    public static void IsPrinted()
    {
        // Setzt die vorherige Position mit ".".
        Program.Display(Room.Distance + (_j * 2), _i, ".");

        // Wenn das Item eingesammelt wurde, wird die vorherige Position mit " " gesetzt.
        if (Check.ItemIsCollected && _i == Item.i && _j == Item.j)
            Program.Display(Room.Distance + (_j * 2), _i, " ");

        Console.ForegroundColor = ConsoleColor.Blue;

        // Setzt die aktuelle Position mit "F".
        Program.Display(Room.Distance + (j * 2), i, "F");

        Console.ResetColor();
    }
}