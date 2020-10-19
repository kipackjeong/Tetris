using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Xml;

public class StaticScreen // layout that does not change.
{


    #region Fields
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

    private bool _runGame = true;


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
        Console.SetCursorPosition(10, 19);
        Console.WriteLine(_horBorder);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.SetCursorPosition(10, 40);
        Console.WriteLine(_horBorder);


        // vertical
        for (int i = 0; i < _verBorder.Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(8, 19 + i);
            Console.WriteLine(_verBorder[i]);
        }

        for (int i = 0; i < _verBorder.Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(30, 19 + i);
            Console.WriteLine(_verBorder[i]);
        }
        #endregion

        #region OUTER Border
        // horizontal
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(0, 18);
        Console.WriteLine(_horOuter);
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(0, 18 + _verBorder.Length);
        Console.WriteLine(_horOuter);


        // vertical
        // left
        for (int i = 0; i < _verOuter.Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 19 + i);
            Console.WriteLine(_verOuter[i]);
        }
        // right
        for (int i = 0; i < _verBorder.Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(62, 19 + i);
            Console.WriteLine(_verOuter[i]);
        }
        #endregion

        #region Title
        Console.ForegroundColor = ConsoleColor.Green;
        Console.SetCursorPosition(0, 10);
        Console.Write(_title); 
        #endregion

        #region Key Button Display
        for (int y = 0; y < _keyButtons.Length; ++y)
        {
            Console.SetCursorPosition(44, 30 + y);
            for (int x = 0; x < _keyButtons[y].Length; ++x)
            {
                Console.Write(_keyButtons[y][x]);
            }
        } 
        #endregion
    }

    public void GameOver()
    {
        string answer = "";
        Console.SetCursorPosition(15,25);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("GAME OVER");
        Console.ReadLine();
        Console.SetCursorPosition(15, 25);
        Console.Write("Try One More Time?");
        Console.SetCursorPosition(15, 26);
        Console.Write("Y/N");
        while (answer != "n" && answer != "y")
        {
            Console.SetCursorPosition(15, 27);
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
