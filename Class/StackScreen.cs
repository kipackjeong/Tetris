using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Xml.Schema;

public class StackScreen : GameScreen
{
    // field/ prop
    public static int _score;
    private GameScreen _parent;
    // ctor
    public StackScreen(GameScreen _Parent) : base(_Parent.ScrSizeX, _Parent.ScrSizeY - 2, false)
    {
        _parent = _Parent; // get the screen from parent.
    }
    // methods
    public override void Render() // renders stacked blocks, and removes filled line. It will be stacked in GameScreen.
    {

        var emptyList = empty(); // brings filled rows, which is in list container.
        if (emptyList.Count > 0)
        {
            for (int i = 0; i < emptyList.Count; i++)
            {
                MainScreen.RemoveAt(emptyList[i]); // remove each rows
                MainScreen.Insert(0, new List<string>() { "□", "□", "□", "□", "□", "□", "□", "□", "□", "□"}); // insert new row at the top
            }
            upScore(emptyList.Count); // increase  score per deleted row
        }
        for (int y = 0; y < MainScreen.Count; ++y)
        {
            for (int x = 0; x < MainScreen[y].Count; ++x)
            {
                _parent.SetBlock(y + 1, x, MainScreen[y][x]); // set the block on GameScreen
            }
        }
        Console.SetCursorPosition(43 + StaticScreen.BasicGrid.X, 25 + StaticScreen.BasicGrid.Y);
        Console.WriteLine("score: " + _score); // update score
    }

    private List<int> empty()
    {
        List<int> row = new List<int>();
        for (int y = 0; y < ScrSizeY; ++y)
        {
            int count = 0;
            for (int x = 0; x < ScrSizeX; ++x)
            {
                if (MainScreen[y][x] == "▦")
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