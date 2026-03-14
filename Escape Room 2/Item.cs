namespace Escape_Room_2;

class Item
{
    // Koordinaten vom Item.
    public static int i;
    public static int j;

    // Vorherige Position.
    public static int _i;
    public static int _j;

    private static Random rnd = new Random();

    /// <summary>
    /// Gibt eine zufällige Position des Items an.
    /// </summary>
    public static void Spawn()
    {
        i = rnd.Next(1, Room.Row - 1);
        j = rnd.Next(1, Room.Column - 1);

        if ((i == Quiz.i && j == Quiz.j) || (i == Figure.i && j == Figure.j))
            Spawn();

        _i = i;
        _j = j;
    }

    /// <summary>
    /// Zufällige Bewegung des Items.
    /// </summary>
    public static void Move()
    {
        // 0 = nach oben, 1 = nach links, 2 = nach unten, 3 = nach rechts.
        int n = rnd.Next(0, 4);

        if (n == 0) // Wenn vor Item nur den Char '.' sich befinden, dann bewegen.
        {
            if (Room.Pos[i, j - 1] == '.' || Room.Pos[i, j - 1] == ' ')
            {
                _i = i;
                _j = j;
                j -= 1;
                return;
            }
        }
        if (n == 1)
        {
            if (Room.Pos[i - 1, j] == '.' || Room.Pos[i - 1, j] == ' ')
            {
                _i = i;
                _j = j;
                i -= 1;
                return;
            }
        }
        if (n == 2)
        {
            if (Room.Pos[i, j + 1] == '.' || Room.Pos[i, j + 1] == ' ')
            {
                _i = i;
                _j = j;
                j += 1;
                return;
            }
        }
        if (n == 3)
        {
            if (Room.Pos[i + 1, j] == '.' || Room.Pos[i + 1, j] == ' ')
            {
                _i = i;
                _j = j;
                i += 1;
                return;
            }
        }
        Move();
    }

    /// <summary>
    /// Setzt das Item auf Position.
    /// </summary>
    public static void IsPrinted()
    {
        // Vorherige Position mit '.' überschreiben.
        Program.Display(Room.Distance + (_j * 2), _i, ".");

        Console.ForegroundColor = ConsoleColor.Green;

        // Item auf neue Position setzen.
        Program.Display(Room.Distance + (j * 2), i, "I");

        Console.ResetColor();
    }
}