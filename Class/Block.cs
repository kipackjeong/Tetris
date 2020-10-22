using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Xml.Schema;

public struct Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}
 
public partial class Block
{
    #region field/ prop
    
    //block grid (x,y)
    private int _x = 5;
    private int _y = 0;
    public Point XY;


    // Blocks container.
    private string[][] _arr;
    // cur block Type/Dir
    private BLOCKTYPE _curBlockType;
    private BLOCKDIR _curBlockDir;

    private BLOCKTYPE _nextBLockType;
    private BLOCKDIR _nextBLOCKDIR;
    // cur block's dimensions
    private int _curBase;
    private int _maxLength;
    private int _maxHeight;
    // Compositions
    private readonly Random _rand = new Random();
    private readonly GameScreen _gameScr;
    private StackScreen _stackScr;
    // condition
    private bool _canItTurn = true;
    public bool blockAlive = true;
    
    #endregion



    #region Constructor
    public Block(GameScreen gameScr, StackScreen stackScr)
    {
        if (gameScr == null || stackScr == null)
        {
            return;
        }
        _gameScr = gameScr;
        _stackScr = stackScr;
        Datalnit(); // initializes the array that all the blocks are in.
        CreateRandBlock();
        SettingBlock(_curBlockType, _curBlockDir);
    }

    #endregion

    private void CreateRandBlock()
    {
        _curBlockType = (BLOCKTYPE)_rand.Next(0, 7);
        _curBlockDir = (BLOCKDIR)_rand.Next(0, 4);

    }

    private void SettingBlock(BLOCKTYPE type, BLOCKDIR dir)
    {
        _arr = AllBLock[(int)type][(int)dir]; 
        GetMaxHeight(_arr);
        GetMaxLength(_arr);
    }

    public void Stack()
    {
        for (int y = 0; y < _arr.Length; ++y)
        {
            for (int x = 0; x < _arr[y].Length; ++x)
            {
                if (_arr[y][x] == "▦")
                {
                    _stackScr.SetBlock(_y + y -1 , _x + x, "▦");
                    IsDead(y);
                }
               
            }
        }
    }

    public void Reset()
    {
        CreateRandBlock(); // generate random block Type and DIR
        // reset block's position
        _x = 5;
        _y = 0;

        SettingBlock(_curBlockType,_curBlockDir); // sets on the screen//
    }
    #region Side/Down Checks
    public bool DownCheck()
    {   // Can't go down further
        for (int y = 0; y < _arr.Length; ++y)
        {
            for (int x = 0; x < _arr[y].Length; ++x)
            {
                if (_arr[y][x] == "▦")
                {
                    //  if block is at bottom line         or       block present right below.
                    if (_stackScr.ScrSizeY == y + _y || _stackScr.IsBlock(y + _y, x + _x, "▦"))
                    {
                        Stack();
                        Reset();
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public bool RightSideCheck()
    {
        for (int y = 0; y < _arr.Length; ++y)
        {
            for (int x = 0; x < _arr[y].Length; ++x)
            {
                if (_arr[y][x] == "▦")
                {
                    if (_stackScr.ScrSizeX == x + _x + 1 || _stackScr.IsBlock(y + _y, x + _x + 1, "▦"))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public bool LeftSideCheck()
    {
        for (int y = 0; y < _arr.Length; ++y)
        {
            for (int x = 0; x < _arr[y].Length; ++x)
            {
                if (_arr[y][x] == "▦")
                {
                    if (0 == _x || _stackScr.IsBlock(y + _y, _x - 1, "▦"))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void TurnCheck()
    {
        string[][] nextBlock;
        if ((int)_curBlockDir < 3)
        {
            nextBlock = AllBLock[(int)_curBlockType][(int)_curBlockDir + 1];
        }
        else
        {
            nextBlock = AllBLock[(int)_curBlockType][0];
        }

        var nextBMaxLength = NextBlockLength(nextBlock);
        if (RightSideCheck() || nextBMaxLength > _maxLength)
        {
            if (_x + (nextBMaxLength - 1) >= 9)
            {
                _canItTurn = false;
            }
            else
            {
                _canItTurn = true;
            }
        }
        else
        {
            _canItTurn = true;
        }
    }

    public int NextBlockLength(string[][] block)
    {
        int maxLength = 0;
        for (int y = 0; y < block.Length; y++)
        {
            if (block[y].Length > maxLength)
            {
                maxLength = block[y].Length;
            }
        }
        return maxLength;
    }
    #endregion


    //////WORK ON THIS//////
    public Point ScanPoint()
    {
        int endpoint = 21;

        int blockBaseXIndex = 15;
        int blockBaseLength = 0;
        bool foundBlock = false;
        var alignsWBases = new HashSet<int>();

        // Scan block
        for (var x = 0; x < _arr[_maxHeight - 1].Length; ++x)
        {
            if (_arr[_maxHeight - 1][x] == "▦")
            {
                if (!foundBlock)
                {
                    foundBlock = true;
                    blockBaseXIndex = x;
                }
                blockBaseLength++;
                
            }
        }

        foundBlock = false;


        int count = 0;
        // Scan Ground
        for (var y = _y + _maxLength; y < _stackScr.ScrSizeY; ++y)
        {
            for (var x = _x; x < _x + _maxLength; ++x)
            {
                if (_stackScr.BlockList[y][x] == "▦") // found a stacked block below current block
                {
                    if (!foundBlock)
                    {
                        XY.X = x;
                        XY.Y = y;
                        foundBlock = true;
                    }

                    alignsWBases.Add(x);

                }
            }
            if (foundBlock)
                break;
            // check if x grid of highestgroundblock is same as the base of current block
        }
        if(!foundBlock)
        {
            XY.Y = _stackScr.ScrSizeY + 1 - _maxHeight;
        }
        else if (alignsWBases.Contains(blockBaseXIndex) || blockBaseXIndex ==  XY.X)
        {
            XY.Y = XY.Y - 1 - _maxHeight;
        }
        else
        {
            XY.Y = XY.Y - _maxHeight + 1;
        }
        return XY;
    }
    private void GetMaxHeight(string[][] Block)
    {
        _maxHeight =  Block.Length;
    }
    private void GetMaxLength(string[][] Block)
    {
        int maxLength = 0;
        for (int y = 0; y < _arr.Length; y++)
        {
            if (_arr[y].Length > maxLength)
            {
                maxLength = _arr[y].Length;
            }
        }

        _maxLength =  maxLength;
    }
    public void Move(int i)
    {
        Console.SetCursorPosition(48 + StaticScreen.BasicGrid.X, 32 + StaticScreen.BasicGrid.Y);
        if (i % 10 == 0)
        {
            Down();
        }
        Input();
        try
        {

            if (!_canItTurn)
            {
                _x = (_gameScr.ScrSizeX - (_maxLength));
                _canItTurn = true;
            }
            for (int y = 0; y < _arr.Length; ++y)
            {
                for (int x = 0; x < _arr[y].Length; ++x)
                {
                    _gameScr.SetBlock(_y + y, _x + x, _arr[y][x]);
                }
            }
            // locates block grid


        }
        catch (Exception e)
        {
            return;
        }
    }
    public void Input()
    {
        Console.CursorVisible = false;

        if (!Console.KeyAvailable)
        {
            return;
        }
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.A:
                if ((LeftSideCheck()))
                    break;
                _x -= 1;
                break;

            case ConsoleKey.D:
                if (RightSideCheck())
                    break;
                else
                    _x += 1;
                break;

            case ConsoleKey.S:
                Down();
                break;

            case ConsoleKey.W:
                TurnCheck();
                if ((int)_curBlockDir < 3)
                {
                    _curBlockDir += 1;
                    SettingBlock(_curBlockType, _curBlockDir);
                    GetMaxHeight(_arr);
                    break;
                }
                else if ((int)_curBlockDir == 3)
                {
                    _curBlockDir = 0;
                    SettingBlock(_curBlockType, _curBlockDir);
                    break;
                }

                break;
            case ConsoleKey.Spacebar:
                QuickDown();
                break;
        }
    }
    public void Down()
    {
        if (!DownCheck())
        {
            _y++;
        }
    }
    public void QuickDown()
    {
        var point = ScanPoint();

        //if (_stackScr.BlockList[point.Y][point.X] == "▦")
        //{
        //    _y = point.Y - 2;
        //}

        _y = point.Y;
    }

    public void IsDead(int y)
    {
        if (_y + y - 1 == 0) // if block's height exceeds the screen.
        {
            blockAlive = false;
        }
    }
}