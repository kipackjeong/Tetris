using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Xml;

public class GAMESCREEN
{
    private string[][] ScreenBoard;
    public StringBuilder HorBorder = new StringBuilder();
    public StringBuilder VerBorder = new StringBuilder();
    public StringBuilder HorOuter = new StringBuilder();
    public StringBuilder VerOuter = new StringBuilder();
    public Alphabet tetris = new Alphabet();
    public bool  RunGame = true;

    public string[][] keyButtons = new string[][]
    {
        new string[]{" "," "," ","","W" },
        new string[]{" "," "," ","▲"},
        new string[]{"","A","◀"," ", " ","▶", "D"},
        new string[]{" "," "," ","▼"},
        new string[]{"  Space"},
    };


    public GAMESCREEN()
    {

        BuildBorders();
        Console.WindowHeight = 40;
    }

    public void BuildBorders()
    {
        for (int i = 0; i < 22; i++)
        {
            VerBorder.Append("▧");
        }

        for (int i = 0; i < 10; i++)
        {
            HorBorder.Append("▧");
        }

        for (int i = 0; i < 22; i++)
        {
            VerOuter.Append("▧");
        }

        for (int i = 0; i < 32; i++)
        {
            HorOuter.Append("▧");
        }

    }
    public void renderScreen()
    {
        /* 
         *  Border
        */
        // horizontal
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.SetCursorPosition(10, 19);
        Console.WriteLine(HorBorder);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.SetCursorPosition(10,40);
        Console.WriteLine(HorBorder);
        

        // vertical
        for (int i = 0; i < VerBorder.Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(8, 19 + i);
            Console.WriteLine(VerBorder[i]);
        }

        for (int i = 0; i < VerBorder.Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(30, 19 + i);
            Console.WriteLine(VerBorder[i]);
        }
        /* 
        *  Outer
        */
        // horizontal
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(0, 18);
        Console.WriteLine(HorOuter);
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(0, 18 + VerBorder.Length);
        Console.WriteLine(HorOuter);


        // vertical
        // left
        for (int i = 0; i < VerOuter.Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 19 + i);
            Console.WriteLine(VerOuter[i]);
        }
        // right
        for (int i = 0; i < VerBorder.Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(62, 19 + i);
            Console.WriteLine(VerOuter[i]);
        }



        // Tetris Title Text
        Console.ForegroundColor = ConsoleColor.Green;
        Console.SetCursorPosition(0,10);
        Console.Write(tetris.tetrisTitle);


        // Key buttons
        for (int y = 0; y < keyButtons.Length; ++y)
        {
            Console.SetCursorPosition(44, 30 + y);
            for (int x = 0; x < keyButtons[y].Length; ++x)
            {
                Console.Write(keyButtons[y][x]);
            }
        }
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


}
