using System;
using System.Collections.Generic;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Security.Policy;



public partial class Block
{
    #region field
    //block starting grid (x,y)
    private int _x = 5;
    private int _y = 0;
    // Blocks container.
    private string[][] Arr;
    // cur block Type/Dir
    private BLOCKTYPE curBLockType;
    private BLOCKDIR curBLOCKDIR;

    private BLOCKTYPE nextBLockType;
    private BLOCKDIR nextBLOCKDIR;
    // cur block's dimensions
    private int curBase;
    private int MaxLength;
    private int MaxHeight;
    // Compositions
    private Random Rand = new Random();
    private GameScreen _blockScr;
    private StackScreen _blockAccScreen;
    // conditions
    public bool Dead = false;
    private bool canItTurn = true;

    #endregion



    #region Constructor
    public Block(GameScreen blockScr, StackScreen _blockAccScreen)
    {
        if (blockScr == null || _blockAccScreen == null)
        {
            return;
        }
        _blockScr = blockScr;
        this._blockAccScreen = _blockAccScreen;
        Datalnit();
        CreateRandBlock();
        SettingBlock(curBLockType, curBLOCKDIR);
        AssignMaxLength(GetMaxLength(Arr));
    }

    #endregion
    private int GetMaxHeight(string[][] Block)
    {
        return Block.Length;
    }
    private int GetMaxLength(string[][] Block)
    {
        int maxLength = 0;
        for (int y = 0; y < Arr.Length; y++)
        {
            if (Arr[y].Length > maxLength)
            {
                maxLength = Arr[y].Length;
            }
        }

        return maxLength;


    }

    private void AssignMaxLength(int length)
    {
        MaxLength = length; 
    }
    private void CreateRandBlock()
    {
        curBLockType = (BLOCKTYPE)Rand.Next(0, 7);
        curBLOCKDIR = (BLOCKDIR)Rand.Next(0, 4);
    }

    private void SettingBlock(BLOCKTYPE type, BLOCKDIR dir)
    {
        Arr = AllBLock[(int)type][(int)dir];
    }

    public void Stack()
    {
        for (int y = 0; y < Arr.Length; ++y)
        {
            for (int x = 0; x < Arr[y].Length; ++x)
            {
                if (Arr[y][x] == "▦")
                {
                    
                    _blockAccScreen.SetBlock(_y + y -1 , _x + x, "▦");
                    IsDead(y);
                }
               
            }
        }
    }

    public void Reset()
    {
        CreateRandBlock();
        _x = 5;
        _y = 1;
        SettingBlock(curBLockType,curBLOCKDIR);
        AssignMaxLength(GetMaxLength(Arr));
    }
    #region Side/Down Checks
    public bool DownCheck()
    {   // Can't go down further
        for (int y = 0; y < Arr.Length; ++y)
        {
            for (int x = 0; x < Arr[y].Length; ++x)
            {
                if (Arr[y][x] == "▦")
                {
                    if (_blockAccScreen.ScrSizeY == y + _y || _blockAccScreen.IsBlock(y + _y, x + _x, "▦"))
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
        for (int y = 0; y < Arr.Length; ++y)
        {
            for (int x = 0; x < Arr[y].Length; ++x)
            {
                if (Arr[y][x] == "▦")
                {
                    if (_blockAccScreen.ScrSizeX == x + _x + 1 || _blockAccScreen.IsBlock(y + _y, x + _x + 1, "▦"))
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
        for (int y = 0; y < Arr.Length; ++y)
        {
            for (int x = 0; x < Arr[y].Length; ++x)
            {
                if (Arr[y][x] == "▦")
                {
                    if (0 == _x || _blockAccScreen.IsBlock(y + _y, _x - 1, "▦"))
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
        if ((int)curBLOCKDIR < 3)
        {
            nextBlock = AllBLock[(int)curBLockType][(int)curBLOCKDIR + 1];
        }
        else
        {
            nextBlock = AllBLock[(int)curBLockType][0];
        }

        if (RightSideCheck() || GetMaxLength(nextBlock) > GetMaxLength(Arr))
        {
            if (_x + (GetMaxLength(nextBlock) - 1) >= 9)
            {
                canItTurn = false;
            }
            else
            {
                canItTurn = true;
            }
        }
        else
        {
            canItTurn = true;
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
        for (int y = 0; y < Arr.Length; ++ y)
        {
            if (Arr[y].Length > longest)
            {
                longest = Arr[y].Length;
                longindex = y;
            }
        }

        for (int y = _y + GetMaxLength(Arr); y < _blockAccScreen.ScrSizeY; y++)
        {
            for (int x = _x; x < _x + longest; ++x)
            { 
                if (_blockAccScreen.BlockList[y][x] == "▦")
                {
                   
                    if (Arr[Arr.Length - 1].Length == longest && curBLockType != BLOCKTYPE.BT_O && curBLockType == BLOCKTYPE.BT_Z)
                    {
                        goal = y - GetMaxHeight(Arr) + 1;
                    }
                    else
                    {
                        goal = y - GetMaxHeight(Arr);
                    }
                    return goal;
                } else if (y == _blockAccScreen.ScrSizeY - 1)
                {
                    goal = y + 2 - GetMaxHeight(Arr);
                }
            }
        }

        return goal;


        //if (longest != Arr[Arr.Length - 1].Length)
        //{
        //    return goal - 1;
        //}

    }
    public void QuickDown()
    {
        var point = ScanPoint();
        _y = point;
    }

    public void Input()
    {
        Console.CursorVisible = false;

        if (!Console.KeyAvailable)
        {
            return;
        }
        switch (Console.ReadKey().Key)
        { case ConsoleKey.A:
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
                if ((int)curBLOCKDIR < 3)
                {
                    curBLOCKDIR += 1;
                    SettingBlock(curBLockType, curBLOCKDIR);
                    break;
                }
                else if ((int)curBLOCKDIR == 3)
                {
                    curBLOCKDIR = 0;
                    SettingBlock(curBLockType, curBLOCKDIR);
                    break;
                }
        
                break;
            case ConsoleKey.Spacebar:
                QuickDown();
                break;
        }

        AssignMaxLength(GetMaxLength(Arr));
    }

    public string[][] beforeBlock()
    {
        return AllBLock[(int) curBLockType][(int) curBLOCKDIR - 1];
    }

    public void Move(int i)
    {
        Console.SetCursorPosition(48, 32);
        if (i % 10 == 0)
        {
            Down();
        }
        Input();
        AssignMaxLength(GetMaxLength(Arr));

        try
        {

            if (!canItTurn)
            {
                _x = (_blockScr.ScrSizeX - (MaxLength));
                canItTurn = true;
            }
            for (int y = 0; y < Arr.Length; ++y)
            {
                for (int x = 0; x < Arr[y].Length; ++x)
                {
                    _blockScr.SetBlock(_y + y, _x + x, Arr[y][x]);
                }
            }
            // 블록을 찍는역활


        }
        catch (Exception e)
        {
            return;
        }
    }
    public void Down()
    {
        if (!DownCheck())
        {
            _y++;
        }
    }

    public void IsDead(int y)
    {
        if (_y + y - 1 == 0)
        {
            this.Dead = true;
        }

    }
}