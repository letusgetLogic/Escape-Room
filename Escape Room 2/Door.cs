namespace Escape_Room_2;

class Door
{
    // Koordinaten von der Tür.
    public static int i;
    public static int j;

    private static Random rnd = new Random();

    /// <summary>
    /// Gibt eine zufällige Position der Tür an.
    /// </summary>
    public static void Spawn()
    {
        i = rnd.Next(0, Room.Row);
        j = rnd.Next(0, Room.Column);

        if ((i == 0 || i == Room.Row - 1) && (j == 0 || j == Room.Column - 1)) // Neue Koordinate, wenn Tür in Ecken ist.
            Spawn();
        if (!(i == 0 || i == Room.Row - 1) && !(j == 0 || j == Room.Column - 1)) // Neue Koordinate, wenn Tür nicht am Rand ist.
            Spawn();
    }

    /// <summary>
    /// Printet die Tür aus.
    /// </summary>
    public static void IsPrinted()
    {
        if (Level.Hard == true)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Program.Display(Room.Distance + (j * 2), i, "?");

            Console.ResetColor();
        }
        else
        {
            Program.Display(Room.Distance + (j * 2), i, " ");
        }
    }
}