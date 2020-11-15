using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Xml;

public class StaticScreen // layout that does not change.
{

    #region Fields/ Properties

    public static string PlayerName;
    public static Point BasicGrid = new Point(10, -8);
    private readonly StringBuilder _horBorder = new StringBuilder();
    private readonly StringBuilder _verBorder = new StringBuilder();
    private readonly StringBuilder _horOuter = new StringBuilder();
    private readonly StringBuilder _verOuter = new StringBuilder();
    private readonly string _title = $@"
                     _______ _______ _______ ______  ___ _______ 
                    |       |       |       |    _ ||   |       |
                      |   | |   |___  |   | |   |_|||   | |_____ 
                      |   | |    ___| |   | |    _ ||   |_____  |
                      |   | |   |___  |   | |   | |||   |_____| |
                      |___| |_______| |___| |___| |||___|_______|
";
    private readonly string[][] _keyButtons = new string[][]
    {
        new string[]{" "," "," ","","W" },
        new string[]{" "," "," ","▲"},
        new string[]{"","A","◀"," ", " ","▶", "D"},
        new string[]{" "," "," ","▼"},
        new string[]{"  Space"},
    }; 
    #endregion

    private bool _runGame = false;


    public StaticScreen()
    {
        BuildBorders();
        Console.WindowHeight = 40;
    }

    public void BuildBorders()
    {
        for (int i = 0; i < 22; i++)
        {
            _verBorder.Append("▧");
        }

        for (int i = 0; i < 10; i++)
        {
            _horBorder.Append("▧");
        }

        for (int i = 0; i < 22; i++)
        {
            _verOuter.Append("▧");
        }

        for (int i = 0; i < 32; i++)
        {
            _horOuter.Append("▧");
        }

    }
    /// <summary>
    /// renders static display of the game.
    /// </summary>
    public void StaticRender()
    {
        // static displays

        #region INNER Border
        // horizontal
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.SetCursorPosition(10 + BasicGrid.X, 19 + BasicGrid.Y);
        Console.WriteLine(_horBorder);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.SetCursorPosition(10 + BasicGrid.X, 40 + BasicGrid.Y);
        Console.WriteLine(_horBorder);


        // vertical
        for (int i = 0; i < _verBorder.Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(8 + BasicGrid.X, 19 + i + BasicGrid.Y);
            Console.WriteLine(_verBorder[i]);
        }

        for (int i = 0; i < _verBorder.Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(30 + BasicGrid.X, 19 + i + BasicGrid.Y);
            Console.WriteLine(_verBorder[i]);
        }
        #endregion

        #region OUTER Border
        // horizontal
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(0 + BasicGrid.X, 18 + BasicGrid.Y);
        Console.WriteLine(_horOuter);
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(0 + BasicGrid.X, 18 + _verBorder.Length + BasicGrid.Y);
        Console.WriteLine(_horOuter);


        // vertical
        // left
        for (int i = 0; i < _verOuter.Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0 + BasicGrid.X, 19 + i + BasicGrid.Y);
            Console.WriteLine(_verOuter[i]);
        }
        // right
        for (int i = 0; i < _verBorder.Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(62 + BasicGrid.X, 19 + i + BasicGrid.Y);
            Console.WriteLine(_verOuter[i]);
        }
        #endregion

        #region Title
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.SetCursorPosition(  BasicGrid.X, 10 + BasicGrid.Y);
        Console.Write(_title); 
        #endregion

        #region Key Button Display
        for (int y = 0; y < _keyButtons.Length; ++y)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(44 + BasicGrid.X, 30 + y + BasicGrid.Y);
            for (int x = 0; x < _keyButtons[y].Length; ++x)
            {
                Console.Write(_keyButtons[y][x]);
            }
        }
        #endregion
        // before games start
        while (_runGame == false)
        {
            GameStart();
        }
    }

    public void GameStart()
    {
        
        // Ask user to start
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(13 + BasicGrid.X, 25 + BasicGrid.Y);
        Console.WriteLine("Want to Start?");
        Console.SetCursorPosition(18 + BasicGrid.X, 27 + BasicGrid.Y);
        Console.WriteLine("Y/N");
        Console.SetCursorPosition(18 + BasicGrid.X, 27 + BasicGrid.Y);
        Console.CursorVisible = false;
        var answer = Console.ReadKey();
        if (answer.Key == ConsoleKey.Y)
        {
            _runGame = true;
        }
        else
        {
            return;
        }
        // set Playername before starting.
        Console.SetCursorPosition(12 + BasicGrid.X, 27 + BasicGrid.Y);
        Console.WriteLine("Enter your name: ");
        Console.SetCursorPosition(12 + BasicGrid.X, 28 + BasicGrid.Y);
        PlayerName = Console.ReadLine();
    }
    public void GameOver()
    {
        string answer = "";
        Console.SetCursorPosition(15 + BasicGrid.X, 25 + BasicGrid.Y);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("GAME OVER");
        Console.ReadLine();
        Console.SetCursorPosition(14 + BasicGrid.X, 25 + BasicGrid.Y);
        Console.Write("Try One More?");
        Console.SetCursorPosition(18 + BasicGrid.X, 26 + BasicGrid.Y);
        Console.Write("Y/N");
        while (answer != "n" && answer != "y")
        {
            Console.SetCursorPosition(15 + BasicGrid.X, 27 + BasicGrid.Y);
            answer = Console.ReadLine().ToLower();
        }
        if ("n" == answer)
        {
            _runGame = false;
        }
        else if ("y" == answer)
        {
            _runGame = true;
        }
    }
    public bool CheckToRun()
    {
        return _runGame;
    }


}
