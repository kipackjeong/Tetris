using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Security.Policy;



public partial class Block
{
    #region field
    //block grid (x,y)
    private int _x = 5;
    private int _y = 0;
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
    private readonly StackScreen _stackScr;
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
        Datalnit();
        CreateRandBlock();
        SettingBlock(_curBlockType, _curBlockDir);
        _maxLength = GetMaxLength(_arr);
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

        SettingBlock(_curBlockType,_curBlockDir); // sets on the screen
        GetMaxLength(_arr); // get MaxLength for block and set it
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

        var nextBMaxLength = GetMaxLength(nextBlock);
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
    #endregion


    //////WORK ON THIS//////
    public int ScanPoint()
    {
        int endpoint = 0;
        int longest = 0;
        int longindex = 0;
        int goal = 0;
        for (int y = 0; y < _arr.Length; ++ y)
        {
            if (_arr[y].Length > longest)
            {
                longest = _arr[y].Length;
                longindex = y;
            }
        }

        for (int y = _y + _maxLength; y < _stackScr.ScrSizeY; y++)
        {

            for (int x = _x; x < _x + longest; ++x) 
            {
                if (_stackScr.BlockList[y][x] == "▦")
                {
                    if (_arr[_arr.Length - 1].Length == longest && _curBlockType != BLOCKTYPE.BT_O &&
                        _curBlockType == BLOCKTYPE.BT_Z)
                    {
                        goal = y - GetMaxHeight(_arr) + 1;
                    }
                    else
                    {
                        goal = y - GetMaxHeight(_arr);
                    }

                    return goal;
                }
                else if (y == _stackScr.ScrSizeY - 1)
                {
                    goal = y + 2 - GetMaxHeight(_arr);
                }
            }
        }

        return goal;
    }
    private int GetMaxHeight(string[][] Block)
    {
        return Block.Length;
    }
    private int GetMaxLength(string[][] Block)
    {
        int maxLength = 0;
        for (int y = 0; y < _arr.Length; y++)
        {
            if (_arr[y].Length > maxLength)
            {
                maxLength = _arr[y].Length;
            }
        }

        return maxLength;
    }
    public void Move(int i)
    {
        Console.SetCursorPosition(48, 32);
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
        _y = point;
    }

    public void IsDead(int y)
    {
        if (_y + y - 1 == 0) // if block's height exceeds the screen.
        {
            blockAlive = false;
        }
    }
}