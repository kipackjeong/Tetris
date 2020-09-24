using System;
using System.Collections.Generic;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Security.Policy;

public enum BLOCKDIR
{
    BD_T,
    BD_R,
    BD_B,
    BD_L,
    BD_MAX
}

public enum BLOCKTYPE
{
    BT_l,
    BT_T,
    BT_J,
    BT_L,
    BT_S,
    BT_Z,
    BT_O,
    BT_MAX
}

public partial class Block
{
    public int _x = 5;
    public int _y = 0;
    public int curBase;
    protected BLOCKTYPE CurBLockType;
    protected BLOCKDIR CurBLOCKDIR;
    protected Random Rand = new Random();
    protected TETRISSCREEN BlockScr; //스크린 밖에있는 스크린을 불러와야한다
    protected ACCClass BlockAccScreen;
    public string[][] Arr;
    public bool Dead = false;
    public bool canItTurn = true;
    public bool pressedW;
    public int MaxLength;
    public int MaxHeight;

    public Block(TETRISSCREEN _BlockScr, ACCClass _BlockAccScreen) // Constructor
    {
        if (_BlockScr == null || _BlockAccScreen == null)
        {
            return;
        }
        BlockScr = _BlockScr;
        BlockAccScreen = _BlockAccScreen;
        Datalnit();
        CreateRandBlock();
        SettingBlock(CurBLockType, CurBLOCKDIR);
        AssignMaxLength(GetMaxLength(Arr));
    }

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
        CurBLockType = (BLOCKTYPE)Rand.Next(0, 7);
        CurBLOCKDIR = (BLOCKDIR)Rand.Next(0, 4);
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
                    
                    BlockAccScreen.SetBlock(_y + y -1 , _x + x, "▦");
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
        SettingBlock(CurBLockType,CurBLOCKDIR);
        AssignMaxLength(GetMaxLength(Arr));
    }
    public bool DownCheck()
    {   // Can't go down further
        for (int y = 0; y < Arr.Length; ++y)
        {
            for (int x = 0; x < Arr[y].Length; ++x)
            {
                if (Arr[y][x] == "▦")
                {
                    if (BlockAccScreen.ScrSizeY == y + _y || BlockAccScreen.IsBlock(y+_y , x+_x, "▦"))
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
                    if (BlockAccScreen.ScrSizeX == x + _x + 1|| BlockAccScreen.IsBlock(y + _y, x + _x + 1, "▦"))
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
                    if (0 == _x || BlockAccScreen.IsBlock(y + _y, _x -1, "▦"))
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
        if ((int) CurBLOCKDIR < 3)
        {
            nextBlock = AllBLock[(int)CurBLockType][(int)CurBLOCKDIR + 1];
        } else
        {
            nextBlock = AllBLock[(int)CurBLockType][0];
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
    public void Down()
    {
        if (!DownCheck())
        {
            _y++;
        }
    }

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

        for (int y = _y + GetMaxLength(Arr); y < BlockAccScreen.ScrSizeY; y++)
        {
            for (int x = _x; x < _x + longest; ++x)
            { 
                if (BlockAccScreen.BlockList[y][x] == "▦")
                {
                   
                    if (Arr[Arr.Length - 1].Length == longest && CurBLockType != BLOCKTYPE.BT_O && CurBLockType == BLOCKTYPE.BT_Z)
                    {
                        goal = y - GetMaxHeight(Arr) + 1;
                    }
                    else
                    {
                        goal = y - GetMaxHeight(Arr);
                    }
                    return goal;
                } else if (y == BlockAccScreen.ScrSizeY - 1)
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
                if ((int)CurBLOCKDIR < 3)
                {
                    CurBLOCKDIR += 1;
                    SettingBlock(CurBLockType, CurBLOCKDIR);
                    break;
                }
                else if ((int)CurBLOCKDIR == 3)
                {
                    CurBLOCKDIR = 0;
                    SettingBlock(CurBLockType, CurBLOCKDIR);
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
        return AllBLock[(int) CurBLockType][(int) CurBLOCKDIR - 1];
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
                _x = (BlockScr.ScrSizeX - (MaxLength)); 
                canItTurn = true;
            }
            for (int y = 0; y < Arr.Length; ++y)
            {
                for (int x = 0; x < Arr[y].Length; ++x)
                {
                        BlockScr.SetBlock(_y + y, _x + x, Arr[y][x]);
                }
            }
            // 블록을 찍는역활


        }
        catch (Exception e)
        {
            return;
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