using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Xml;

public class StaticScreen
{
    private string tetrisTitle = $@"
           _______ _______ _______ ______  ___ _______ 
          |       |       |       |    _ ||   |       |
            |   | |   |___  |   | |   |_|||   | |_____ 
            |   | |    ___| |   | |    _ ||   |_____  |
            |   | |   |___  |   | |   | |||   |_____| |
            |___| |_______| |___| |___| |||___|_______|
";
    private StringBuilder horBorder = new StringBuilder();
    private StringBuilder verBorder = new StringBuilder();
    private StringBuilder horOuter = new StringBuilder();
    private StringBuilder verOuter = new StringBuilder();
    private bool  RunGame = true;

    public string[][] keyButtons = new string[][]
    {
        new string[]{" "," "," ","","W" },
        new string[]{" "," "," ","▲"},
        new string[]{"","A","◀"," ", " ","▶", "D"},
        new string[]{" "," "," ","▼"},
        new string[]{"  Space"},
    };


    public StaticScreen()
    {

        BuildBorders();
        Console.WindowHeight = 40;
    }

    public void BuildBorders()
    {
        for (int i = 0; i < 22; i++)
        {
            verBorder.Append("▧");
        }

        for (int i = 0; i < 10; i++)
        {
            horBorder.Append("▧");
        }

        for (int i = 0; i < 22; i++)
        {
            verOuter.Append("▧");
        }

        for (int i = 0; i < 32; i++)
        {
            horOuter.Append("▧");
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
        Console.WriteLine(horBorder);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.SetCursorPosition(10, 40);
        Console.WriteLine(horBorder);


        // vertical
        for (int i = 0; i < verBorder.Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(8, 19 + i);
            Console.WriteLine(verBorder[i]);
        }

        for (int i = 0; i < verBorder.Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(30, 19 + i);
            Console.WriteLine(verBorder[i]);
        }
        #endregion

        #region OUTER Border
        // horizontal
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(0, 18);
        Console.WriteLine(horOuter);
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(0, 18 + verBorder.Length);
        Console.WriteLine(horOuter);


        // vertical
        // left
        for (int i = 0; i < verOuter.Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 19 + i);
            Console.WriteLine(verOuter[i]);
        }
        // right
        for (int i = 0; i < verBorder.Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(62, 19 + i);
            Console.WriteLine(verOuter[i]);
        }
        #endregion

        #region Title
        Console.ForegroundColor = ConsoleColor.Green;
        Console.SetCursorPosition(0, 10);
        Console.Write(tetrisTitle); 
        #endregion

        #region Key Button Display
        for (int y = 0; y < keyButtons.Length; ++y)
        {
            Console.SetCursorPosition(44, 30 + y);
            for (int x = 0; x < keyButtons[y].Length; ++x)
            {
                Console.Write(keyButtons[y][x]);
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
            RunGame = false;
        }
        else if ("y" == answer)
        {
            RunGame = true;
        }
    }
    public bool CheckToRun()
    {
        return RunGame;
    }


}
