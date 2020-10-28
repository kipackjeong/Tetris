using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Schema;

public class GameScreen
{
    //field/ prop
    private Point baseGrid= StaticScreen.BasicGrid;

    public List<List<string>> MainScreen = new List<List<string>>(); // this is the main screen.
    public Random randomNum = new Random();
    public int ScrSizeX;
    public int ScrSizeY;
    

    // Render
    public virtual void Render() // y , x render screen.
    {
        for (int y = 0; y < MainScreen.Count; ++y)
        {
            Console.SetCursorPosition(10 + baseGrid.X,  20 + y + baseGrid.Y);
            //var weirdway = string.Join("", MainScreen[y]);
            //Console.WriteLine(weirdway);
            for (int x = 0; x < MainScreen[y].Count; ++x)
            {

                if (MainScreen[y][x] == "▦")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(MainScreen[y][x]);
                    continue;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(MainScreen[y][x]);
            }
            Console.WriteLine();

        }
    }

    public void DeadRender()
    {
        for (int y = 0; y < MainScreen.Count; ++y)
        {
            Console.SetCursorPosition(10 + baseGrid.X, 20 + y + baseGrid.Y);
            //var weirdway = string.Join("", MainScreen[y]);
            //Console.WriteLine(weirdway);
            for (int x = 0; x < MainScreen[y].Count; ++x)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("▦");
                continue;
            }
            Console.WriteLine();
            // record users score.
            try
            {
                var file = File.Create($@"D:\TetrisUsers\{StaticScreen.PlayerName}.txt");
                StreamWriter writer = new StreamWriter(file);
                writer.WriteLine($@"Latest Score for this User : {StackScreen._score}");
                writer.Close();
            }
            catch (Exception e)
            {
            }
           

        }
    }

    //SetBlock
    public void SetBlock(int _y, int _x, string block) // Set's block
    {
        try
        {
            MainScreen[_y][_x] = block;
        }
        catch (Exception e)
        {
            
        }
    }
    public bool IsBlock(int _y, int _x, string block)
    {
        return MainScreen[_y][_x] == block;
    }

    // ClearBlock

    public void ClearBlock()
    {
        
        for (int y = 0; y < MainScreen.Count; ++y)
        {
            for (int x = 0; x < MainScreen[y].Count; x++)
            {
                if (y == 0 || y == MainScreen.Count - 1)
                {
                    MainScreen[y][x] = "▣";
                    continue;
                }

                MainScreen[y][x] = "□";
            }
        }
    }

    // ctor
    public GameScreen(int _x, int _y, bool TopAndBottomLine)
    {
        // if _x , _y == 0
        // defense
        for (int y = 0; y < _y; ++y) // for every y axis movement,
        {
            MainScreen.Add(new List<string>()); // create _y amount of list inside the list.

            for (int x = 0; x < _x; ++x) //  then add _x amount of elements in side the list[y] list.
            {
                if ((TopAndBottomLine && y == 0) || (TopAndBottomLine && y == _y - 1))
                {
                    MainScreen[y].Add("▣");
                }
                else
                {
                    MainScreen[y].Add("□");
                }
            }
        }
        ScrSizeY = _y;
        ScrSizeX = _x;
    }
}