namespace Escape_Room_2;

class Quiz
{
    // Koordinaten vom Quiz.
    public static int i;
    public static int j;

    /// <summary>
    /// Setzt Quiz auf dem Türfeld, nicht sichtbar wenn Item noch nicht gesammelt wurde.
    /// </summary>
    public static void Spawn()
    {
        i = Door.i;
        j = Door.j;
    }


    // --- Quiz Level --- ///

    public static int  NumTest;
    public static int  CorrectResult; // Anzahl richtigen Ergebnisse.
    public static int  WrongResult; // Anzahl falschen Ergebnisse.
    public static int  UsedCheatSheet; // Anzahl benutzten Spickzettels.
    public static bool Busted; // Beim Spicken erwischt.
    public static bool SetGameOver; // Level beendet.

    private static Random _rnd = new Random();

    private static string _input; // Eingabe des Spielers.
    private static int _rowTest;

    private static int _levelTest;
    private const int Easy = 1;
    private const int Medium = 2;
    private const int Hard = 3;

    private static int _a; // 1. Nummer im Test.
    private static int _b; // 2. Nummer im Test.
    private static int _o; // Zufall Operator.
    private static char _operator;
    private static int _solution;

    private const int MaxHitpoint = 10;
    private const int LowHP = 4; // Ab dieser HP Grenze runtergezählt kann der Spickzettel nicht genutzt werden.
    private const int HPBarArea = 15; // HP Leistenbereich.
    private static int _hitpoint = 10;
    private static string _output; // HP Veränderung anzeigen.

    private static int _cheatSheet; // Spickzettel.
        
    /// <summary>
    /// Mathe-Prüfung fängt an.
    /// </summary>
    public static void MathExam()
    {
        PrintIntro();

        Console.Beep();
        Console.Beep();

        SetDefaultValue();

        while (_hitpoint > 0)
        {
            PrintClassRoom();

            if (SetGameOver == true) break;
        }
        if (SetGameOver == false) PrintOutro(); // nur ausprinten wenn Level nicht mit der Taste 'B' beendet wird.

        Console.Clear();
    }

    /// <summary>
    /// Intro von Quiz-Level.
    /// </summary>
    public static void PrintIntro()
    {
        Program.Display(40, 12, "!!! Du hast Herr Müller getroffen !!!");
        Program.Display(40, 14, "Herr Müller: 'Wen sehe ich denn da?'");
        Program.Display(40, 15, "             'Jetzt gibt es einen Überraschung-Test!'");
        Program.Display(Console.BufferWidth / 2 - 6, Console.BufferHeight - 1, "(Drück Enter)");

        ConsoleKey consoleKey = Console.ReadKey().Key;
        if (consoleKey == ConsoleKey.Enter)
        {
            Console.Clear();
            return;
        }
        else PrintIntro();
    }

    /// <summary>
    /// Level ausprinten.
    /// </summary>
    public static void PrintClassRoom()
    {
        NumTest++;
        int rowRoom = 2;
        Program.Display(46, 0, "------- Class Room --------");
        Program.Display(46, rowRoom++, "# # # # # # # # # # # # # #");
        Program.Display(46, rowRoom++, "#                         #");  BarHP(); Console.Write(_output);
        Program.Display(46, rowRoom++, "#       Mathe Lehrer      #");
        Program.Display(46, rowRoom++, "#            M            #");
        Program.Display(46, rowRoom++, "#           ===           #");
        Program.Display(46, rowRoom++, "#                         #");
        Program.Display(46, rowRoom++, "#    ===    ===    ===    #");
        Program.Display(46, rowRoom++, "#     o      F      o     #");
        Program.Display(46, rowRoom++, "#                         #");
        Program.Display(46, rowRoom++, "#    ===    ===    ===    #");
        Program.Display(46, rowRoom++, "#     o      o      o     #");
        Program.Display(46, rowRoom++, "#         Schüler         #");
        Program.Display(46, rowRoom++, "# # # # # # # # # # # # # #");

        int rowInventory = 20;
        Program.Display(25, rowInventory += 2, $"Inventar: {_cheatSheet} Spickzettel - Drück I");
        Program.Display(25, rowInventory += 2, $"(Achtung! Wenn der Lehrer {LowHP} HP oder niedriger hat, wird er aggresiver!)");
        Program.Display(25, Console.BufferHeight - 1, $"(Drück B, um die Prüfung abzugeben und aus dem Klassenraum zu gehen)");

        _rowTest = 14;
        RandomTest();
        Program.Display(53, _rowTest += 2, $"- Aufgabe {NumTest} -");
        Program.Display(46, _rowTest += 2, $"{_a} {_operator} {_b} = "); Input();
    }

    /// <summary>
    /// Eingabe des Spielers.
    /// </summary>
    private static void Input()
    {
        _input = Console.ReadLine();

        if (_input == "b") // Level beenden
        {
            SetGameOver = true;
            Highscores.QuizPoints = 0;
        }
        else
        {
            CheckTest();
        }
    }

    /// <summary>
    /// Checkt das Ergebnis.
    /// </summary>
    private static void CheckTest()
    {
        int numInput;

        // Gibt vom Input eine Nummer aus oder ein "i" mit Bedingungen Spickzettel > 0 und Lebenspunkte vom Herr Müller > LowHP.
        if (int.TryParse(_input, out numInput) || (_input == "i" && _cheatSheet > 0 && _hitpoint > LowHP))
        {
            if (numInput == _solution || _input == "i")
            {
                Program.Display(46, _rowTest += 2, "Das ist richtig.");
                CorrectResult++;

                if (_input == "i")
                {
                    Console.Write($"Das Ergebnis ist {_solution}.");
                    _cheatSheet--;
                    UsedCheatSheet++;
                    Highscores.QuizPoints -= 4;
                }
                if (_levelTest == Easy)
                {
                    _hitpoint -= 1;
                    _output = "- 1 HP";
                }
                if (_levelTest == Medium)
                {
                    _hitpoint -= 2;
                    _output = "- 2 HP";
                }
                if (_levelTest == Hard)
                {
                    _hitpoint -= 4;
                    _output = "- 4 HP";
                }
            }
            else
            {
                Program.Display(46, _rowTest += 2, "Das ist falsch. Die richtige Antwort ist " + _solution);
                WrongResult++;
                Highscores.QuizPoints -= 2;
                Console.Beep();

                if (_hitpoint < MaxHitpoint)
                {
                    _hitpoint++;
                    _output = "+ 1 HP";
                }
            }
        }
        else // Wenn Spickzettel gezogen wird und HP nicht über LowHP ist, wird der Spieler erwischt.
        {
            if (_input == "i" && _cheatSheet > 0 && !(_hitpoint > LowHP))
            {
                Program.Display(46, _rowTest += 2, "Herr Müller: 'Erwischt! Du kommst später zum Lehrerzimmer!'");
                Busted = true;
                _cheatSheet -= 1;
                _hitpoint += 2;
                _output = "+ 2 HP";
                Highscores.QuizPoints -= 4;
            }
            else // Kein gültiges Ergebnis.
            {
                Program.Display(46, _rowTest += 2, "Das ist keine gültige Zahl.");

                if (_hitpoint < MaxHitpoint)
                {
                    _hitpoint++;
                    _output = "+ 1 HP";
                    Highscores.QuizPoints --;
                }
            }
            WrongResult++;
            Console.Beep();
        }
        Console.Beep();
        Console.ReadKey();
        Console.Clear();
    }

    /// <summary>
    /// Gibt den Schwierigkeitsgrad, 2 Operanden und 1 Operator per Zufall an.
    /// </summary>
    public static void RandomTest()
    {
        _levelTest = _rnd.Next(1, 4); // Zufall Testlevel.

        _o = _rnd.Next(1, 4); // Zufall Operator.
            
        if (_levelTest == Easy)
        {
            _a = _rnd.Next(1, 10);
            _b = _rnd.Next(1, 10);
        }
        if (_levelTest == Medium)
        {
            _a = _rnd.Next(10, 101);
            _b = _rnd.Next(1, 11);
            _o = 3;
        }
        if (_levelTest == Hard)
        {
            _a = _rnd.Next(10, 101);
            _b = _rnd.Next(10, 101);
            _o = 3;
        }
        if (_o == 1) { _operator = '+'; _solution = _a + _b; } // Sagt, was aus der Console ausgeben wird und wie die 2 Operanden berechnet werden.
        if (_o == 2) { _operator = '-'; _solution = _a - _b; }
        if (_o == 3) { _operator = '*'; _solution = _a * _b; }
    }

    /// <summary>
    /// Trefferpunkte Leiste //////////
    /// </summary>
    public static void BarHP()
    {
        Console.ForegroundColor = ConsoleColor.Red;

        Console.Write("     HP ");

        for (int z = 0; z < _hitpoint; z++)
            Console.Write("/");

        for (int y = 0; y < HPBarArea - _hitpoint; y++)
            Console.Write(" ");

        Console.ResetColor();
    }

    /// <summary>
    /// "Test bestanden, hura!" + 2x Beep + ReadKey.
    /// </summary>
    public static void PrintOutro()
    {
        Program.Display(Console.BufferWidth / 2 - 10, Console.BufferHeight / 2, "Test bestanden, hura!");
        Console.Beep();
        Console.Beep();
        Console.ReadKey();
    }

    /// <summary>
    /// Setzt die Werte zurück.
    /// </summary>
    private static void SetDefaultValue()
    {
        NumTest = 0;
        CorrectResult = 0;
        WrongResult = 0;
        UsedCheatSheet = 0;
        Busted = false;
        SetGameOver = false;

        _output = "";
        _cheatSheet = 2;
        _hitpoint = MaxHitpoint;
        Highscores.QuizPoints = MaxHitpoint;
    }

}