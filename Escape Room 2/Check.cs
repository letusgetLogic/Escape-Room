namespace Escape_Room_2;

class Check
{
    public static bool ItemIsCollected;
    public static bool QuizIsCollected;
    public static int DegreeOfDepair; // Grad der Verzweiflung wenn Spieler 10 mal in der Nähe vom Item ist, aber nicht bekommt.

    /// <summary>
    /// Wenn die Figur auf das Item ist, beept und Item gesammelt.
    /// </summary>
    public static void ItemCollect()
    {
        if (Figure.i == Item.i && Figure.j == Item.j - 1 ||
            Figure.i == Item.i && Figure.j == Item.j + 1 ||
            Figure.i == Item.i - 1 && Figure.j == Item.j ||
            Figure.i == Item.i + 1 && Figure.j == Item.j)
        {
            DegreeOfDepair++; // Grad der Verzweiflung wenn Spieler 10 mal in der Nähe vom Item ist, aber nicht bekommt.
        }

        if (ItemIsCollected == false && Figure.i == Item.i && Figure.j == Item.j)
        {
            Console.Beep();
            ItemIsCollected = true;
            Door.IsPrinted();
        }
    }

    /// <summary>
    /// Wenn die Figur auf das Quiz ist, beept 2x und Quiz gesammelt.
    /// </summary>
    public static void QuizCollect()
    {
        if (QuizIsCollected == false && Figure.i == Quiz.i && Figure.j == Quiz.j)
        {
            Console.Beep();
            Console.Beep();

            QuizIsCollected = true;

            Console.Clear();

            Quiz.MathExam();

            Room.Print();
        }
    }

    /// <summary>
    /// Raum und Tipp ausprinten.
    /// </summary>
    public static void PrintRoomAndTipp()
    {
        if (DegreeOfDepair == 10)
        {
            Console.SetCursorPosition(0, 0);
            Room.Print();
            Console.WriteLine();
            Console.WriteLine($"{"",-20} Tipp: Lauf einmal gegen dem Wand!");
        }
    }

    /// <summary>
    /// Setzt die Werte zurück. 
    /// </summary>
    public static void SetDefaultValue()
    {
        ItemIsCollected = false;
        QuizIsCollected = false;
        DegreeOfDepair = 0;
    }
}