using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Xml.Schema;

public class StackScreen : GameScreen
{
    private GameScreen Parent;
    private int Score;

    public StackScreen(GameScreen _Parent) : base(_Parent.ScrSizeX, _Parent.ScrSizeY - 2, false)
    // Use Parent's Constructor
    {
        Parent = _Parent;

    }

    public override void Render()
    {

        var emptyList = Empty();
        if (emptyList.Count > 0)
        {
            for (int i = 0; i < emptyList.Count; i++)
            {
                BlockList.RemoveAt(emptyList[i]);
                BlockList.Insert(0, new List<string>() { "□", "□", "□", "□", "□", "□", "□", "□", "□", "□"});
            }
            getScore(emptyList.Count);
        }
        for (int y = 0; y < BlockList.Count; ++y)
        {
            for (int x = 0; x < BlockList[y].Count; ++x)
            {
                Parent.SetBlock(y + 1, x, BlockList[y][x]);
            }
        }
        Console.SetCursorPosition(43, 25);
        Console.WriteLine("Score: " + Score);
    }

    public List<int> Empty()
    {
        List<int> row = new List<int>();
        for (int y = 0; y < ScrSizeY; ++y)
        {
            int count = 0;
            for (int x = 0; x < ScrSizeX; ++x)
            {
                if (BlockList[y][x] == "▦")
                {
                    count++;
                }
            }
            if (count == ScrSizeX)
            {
                row.Add(y);
            }
        }
        return row;
    }
    public void getScore(int rows)
    {
        Score += (100 * rows);
    }


}