namespace Escape_Room_2;

class Movement
{
    public static int CountMoves;

    /// <summary>
    /// Bewegung der Spielfigur mit WASD / Pfeiltasten oder per Eingabe.
    /// </summary>
    public static void PressKey()
    {
        ConsoleKey consoleKey = Console.ReadKey().Key;

        Console.SetCursorPosition(0, 0); // Löscht den letzten Eintrag.
        Console.Write(" ");

        if (consoleKey == ConsoleKey.W || consoleKey == ConsoleKey.UpArrow)
        {
            CountMoves++;

            if (Figure.i == 0) // Wenn Figur auf dem Feld von der Tür ist, stellt mit der Taste W oder UP "GameOver" auf "true".
            {
                Level.GameOver = true;
            }
            else // Checkt ob vor der Spielfigur sich ein Wand oder eine Tür nicht befindet. Wenn es wahr ist, kann die Figur erst bewegen.
            {
                if (Room.Pos[Figure.i - 1, Figure.j] != '#' && Room.Pos[Figure.i - 1, Figure.j] != 'T')
                {
                    Figure._i = Figure.i;
                    Figure._j = Figure.j;
                    Figure.i -= 1;
                }
            }
        }
        if (consoleKey == ConsoleKey.A || consoleKey == ConsoleKey.LeftArrow)
        {
            CountMoves++;

            if (Figure.j == 0)
            {
                Level.GameOver = true;
            }
            else
            {
                if (Room.Pos[Figure.i, Figure.j - 1] != '#' && Room.Pos[Figure.i, Figure.j - 1] != 'T')
                {
                    Figure._i = Figure.i;
                    Figure._j = Figure.j;
                    Figure.j -= 1;
                }
            }
        }
        if (consoleKey == ConsoleKey.S || consoleKey == ConsoleKey.DownArrow)
        {
            CountMoves++;

            if (Figure.i == Room.Row - 1)
            {
                Level.GameOver = true;
            }
            else
            {
                if (Room.Pos[Figure.i + 1, Figure.j] != '#' && Room.Pos[Figure.i + 1, Figure.j] != 'T')
                {
                    Figure._i = Figure.i;
                    Figure._j = Figure.j;
                    Figure.i += 1;
                }
            }
        }
        if (consoleKey == ConsoleKey.D || consoleKey == ConsoleKey.RightArrow)
        {
            CountMoves++;

            if (Figure.j == Room.Column - 1)
            {
                Level.GameOver = true;
            }
            else
            {
                if (Room.Pos[Figure.i, Figure.j + 1] != '#' && Room.Pos[Figure.i, Figure.j + 1] != 'T')
                {
                    Figure._i = Figure.i;
                    Figure._j = Figure.j;
                    Figure.j += 1;
                }
            }
        }
    }
}