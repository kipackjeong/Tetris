using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Xml.Schema;

public class StackScreen : GameScreen
{
    private GameScreen _parent;
    private int _score;

    public StackScreen(GameScreen _Parent) : base(_Parent.ScrSizeX, _Parent.ScrSizeY - 2, false)
    // Use _parent's Constructor
    {
        _parent = _Parent;

    } 
    public override void Render() // renders stacked blocks, and removes filled line.
    {

        var emptyList = empty(); // brings filled rows to empty.
        if (emptyList.Count > 0)
        {
            for (int i = 0; i < emptyList.Count; i++)
            {
                BlockList.RemoveAt(emptyList[i]);
                BlockList.Insert(0, new List<string>() { "□", "□", "□", "□", "□", "□", "□", "□", "□", "□"});
            }
            upScore(emptyList.Count);
        }
        for (int y = 0; y < BlockList.Count; ++y)
        {
            for (int x = 0; x < BlockList[y].Count; ++x)
            {
                _parent.SetBlock(y + 1, x, BlockList[y][x]);
            }
        }
        Console.SetCursorPosition(43 + StaticScreen.BasicGrid.X, 25 + StaticScreen.BasicGrid.Y);
        Console.WriteLine("score: " + _score);
    }

    private List<int> empty()
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
    private void upScore(int rows)
    {
        _score += (100 * rows);
    }


}