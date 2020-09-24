using System;
using System.Collections.Generic;
using System.Xml.Schema;

public class TETRISSCREEN
{
    //field
    public List<List<string>> BlockList = new List<List<string>>();
    public Random randomNum = new Random();
    public int ScrSizeX;
    public int ScrSizeY;


    // Render
    public virtual void Render() // y , x render screen.
    {
        for (int y = 0; y < BlockList.Count; ++y)
        {
            Console.SetCursorPosition(10,  20 + y);
            //var weirdway = string.Join("", BlockList[y]);
            //Console.WriteLine(weirdway);
            for (int x = 0; x < BlockList[y].Count; ++x)
            {

                if (BlockList[y][x] == "▦")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(BlockList[y][x]);
                    continue;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(BlockList[y][x]);
            }
            Console.WriteLine();

        }
    }

    public void DeadRender()
    {
        for (int y = 0; y < BlockList.Count; ++y)
        {
            Console.SetCursorPosition(10, 20 + y);
            //var weirdway = string.Join("", BlockList[y]);
            //Console.WriteLine(weirdway);
            for (int x = 0; x < BlockList[y].Count; ++x)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("▦");
                    continue;
            }
            Console.WriteLine();

        }
    }

    // SetBlock
    public void SetBlock(int _y, int _x, string block) // Set's block
    {
        BlockList[_y][_x] = block;

    }
    public bool IsBlock(int _y, int _x, string block) // Set's block
    {
        return BlockList[_y][_x] == block;

    }

    // ClearBlock

    public void ClearBlock()
    {
        
        for (int y = 0; y < BlockList.Count; ++y)
        {
            for (int x = 0; x < BlockList[y].Count; x++)
            {
                if (y == 0 || y == BlockList.Count - 1)
                {
                    BlockList[y][x] = "▣";
                    continue;
                }

                BlockList[y][x] = "□";
            }
        }
    }

    // ctor
    public TETRISSCREEN(int _x, int _y, bool TopAndBottomLine)
    {
        // if _x , _y == 0
        // defense
        for (int y = 0; y < _y; ++y) // for every y axis movement,
        {
            BlockList.Add(new List<string>()); // create _y amount of list inside the list.

            for (int x = 0; x < _x; ++x) //  then add _x amount of elements in side the list[y] list.
            {
                if ((TopAndBottomLine && y == 0) || (TopAndBottomLine && y == _y - 1))
                {
                    BlockList[y].Add("▣");
                }
                else
                {
                    BlockList[y].Add("□");
                }
            }
        }
        ScrSizeY = _y;
        ScrSizeX = _x;
    }
}