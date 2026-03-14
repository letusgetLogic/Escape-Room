namespace Escape_Room_2;

class Room
{
    public static int Row; // Reihe
    public static int Column; // Spalten
    public static char[,] Pos; // Position
    public static int i; // Reihe
    public static int j; // Spalte
    public static int Distance; // Abstand zum Rand für den mittigen Spielbereich.

    /// <summary>
    /// Den Char "Pos" initialisieren.
    /// </summary>
    public static void SetArray()
    {
        Pos = new char[Row, Column];
    }

    /// <summary>
    /// Gibt jedem Feld ein Zeichen.
    /// </summary>
    public static void SetCharsInArray()
    {
        for (i = 0; i < Row; i++)
        {
            for (j = 0; j < Column; j++)
            {
                if (i == 0 || i == Row - 1 || j == 0 || j == Column - 1)
                {
                    SetCharOnSides(i, j);
                }
                else
                {
                    Pos[i, j] = '.'; // Leere Fläche im Inneren.
                }

                SetFigureAndItem(i, j);
            }
        }
    }

    /// <summary>
    /// Initialisiert Chars an die Seiten und Ecken des Raums.
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    private static void SetCharOnSides(int i, int j)
    {
        if (i == Door.i && j == Door.j && Level.SetSize == true)
        {
            if (Check.ItemIsCollected == false) // Item nicht gesammelt.
                Pos[i, j] = 'T'; // Tür 
            else
                Pos[i, j] = ' '; // Item gesammelt.

            if (Level.Hard == true && Check.ItemIsCollected == true && i == Quiz.i && j == Quiz.j)
            {
                if (Check.QuizIsCollected == false)
                    Pos[i, j] = '?'; // Quiz erscheint, wenn Item gesammelt wurde.
                else
                    Pos[i, j] = ' '; // Quiz gesammelt.
            }
        }
        else
            Pos[i, j] = '#'; // Wände an den Rändern.
    }
    
    /// <summary>
    /// Initialisiert Chars an die Figur und das Item.
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    private static void SetFigureAndItem(int i, int j)
    {
        if (Level.SetSize == true && i == Figure.i && j == Figure.j)
        {
            if (Level.GameOver == false)
                Pos[i, j] = 'F'; // Spielfigur
            else
                Pos[i, j] = ' '; // Spielfigur ist nicht mehr zu sehen / außerhalb des Raums.
        }
        else if (Level.SetSize == true && i == Item.i && j == Item.j)
        {
            if (Check.ItemIsCollected == false)
                Pos[i, j] = 'I'; // Item
            else
                Pos[i, j] = ' '; // Item gesammelt.
        }
    }

    /// <summary>
    /// Printet Raum, Wände, Tür, Spielfigur, Item in der Console aus.
    /// </summary>
    public static void Print()
    {
        for (i = 0; i < Row; i++)
        {
            for (int n = 0; n < Distance; n++) Console.Write(" "); // Abstand zum linken Rand des Console-Fensters.

            for (j = 0; j < Column; j++)
            {
                if (Pos[i, j] == 'T') Console.ForegroundColor = ConsoleColor.Red;

                Console.Write(Pos[i, j] + " ");

                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Berechnet Abtand zum Fensterrand.
    /// </summary>
    public static void CalculateDistance()
    {
        Distance = Console.BufferWidth / 2 - Column; // Da zwischen Chars ein " " sich befindet, werden Anzahl der ausgeprinteten Spalten des Raums doppelt so viel sein.
    } //                                                Die Hälfte ausgeprinteten Spalten des Raums gleich Hälfte der tatsächlichen Spalten des Arrays.
    
}