using System.Collections.Generic;

public partial class Block
{   // Manage all the shape of block
    // Data structure
    //BT_l 0
    //BT_L 1
    //BT_J 2
    //BT_S 3
    //BT_Z 4
    //BT_o 5
    //BT_T 6    7종
    // 4 * 7
    // List<List<string[][]>>

    //BD_T,
    //BD_R,
    //BD_B,
    //BD_L,

    private List<List<string[][]>> AllBLock = new List<List<string[][]>>();

    private void Datalnit()
    {
        // Create Block Container.
        for (int BT = 0; BT < (int)BLOCKTYPE.BT_MAX; ++BT)
        {
            AllBLock.Add(new List<string[][]>());
            for (int BD = 0; BD < (int)BLOCKDIR.BD_MAX; ++BD)
            {
                AllBLock[BT].Add(null);
            }
        }

        // Assign Block
        string s = "▦";
        var e = "□";
        #region l

        //l T
        AllBLock[(int)BLOCKTYPE.BT_l][(int)BLOCKDIR.BD_T] = new string[][]
        {
            new string[] {s},
            new string[] {s},
            new string[] {s},
            new string[] {s}
        };
        //l R
        AllBLock[(int)BLOCKTYPE.BT_l][(int)BLOCKDIR.BD_R] = new string[][]
        {
            new string[] {s, s, s, s}
        };
        //l B
        AllBLock[(int)BLOCKTYPE.BT_l][(int)BLOCKDIR.BD_B] = new string[][]
        {
            new string[] {s},
            new string[] {s},
            new string[] {s},
            new string[] {s}
        };
        // l L
        AllBLock[(int)BLOCKTYPE.BT_l][(int)BLOCKDIR.BD_L] = new string[][]
        {
            new string[] {s, s, s, s}
        };

        #endregion l

        #region T

        // T T
        
        AllBLock[(int)BLOCKTYPE.BT_T][(int)BLOCKDIR.BD_T] = new string[][]
        {
            new string[] {e, s},
            new string[] {s, s, s}
        };
        // T R
        AllBLock[(int)BLOCKTYPE.BT_T][(int)BLOCKDIR.BD_R] = new string[][]
         {
             new string[] {s},
             new string[] {s, s},
             new string[] {s}
        };
        // T B
        AllBLock[(int)BLOCKTYPE.BT_T][(int)BLOCKDIR.BD_B] = new string[][]
        {
            new string[] {s, s, s},
            new string[] {e, s}
        };
        // T L
        AllBLock[(int)BLOCKTYPE.BT_T][(int)BLOCKDIR.BD_L] = new string[][]
        {
            new string[] {e, s},
            new string[] {s, s},
            new string[] {e, s}
        };

        #endregion T

        #region J

        // J T
        AllBLock[(int)BLOCKTYPE.BT_J][(int)BLOCKDIR.BD_T] = new string[][]
        {
            new string[] {e, s},
            new string[] {e, s},
            new string[] {s, s}
        };
        // J R
        AllBLock[(int)BLOCKTYPE.BT_J][(int)BLOCKDIR.BD_R] = new string[][]
        {
            new string[] {s, e, e},
            new string[] {s, s, s}
        };
        // J B
        AllBLock[(int)BLOCKTYPE.BT_J][(int)BLOCKDIR.BD_B] = new string[][]
        {
            new string[] {s, s},
            new string[] {s},
            new string[] {s}
        };
        // J L
        AllBLock[(int)BLOCKTYPE.BT_J][(int)BLOCKDIR.BD_L] = new string[][]
        {
            new string[] {s, s, s},
            new string[] {e, e, s}
        };

        #endregion J

        #region L

        // L T
        AllBLock[(int)BLOCKTYPE.BT_L][(int)BLOCKDIR.BD_T] = new string[][]
        {
            new string[] {s},
            new string[] {s},
            new string[] {s, s}
        };
        // L R
        AllBLock[(int)BLOCKTYPE.BT_L][(int)BLOCKDIR.BD_R] = new string[][]
        {
            new string[] {s, s, s},
            new string[] {s}
        };
        // L B
        AllBLock[(int)BLOCKTYPE.BT_L][(int)BLOCKDIR.BD_B] = new string[][]
        {
            new string[] {s, s},
            new string[] {e, s},
            new string[] {e, s}
        };
        // L L
        AllBLock[(int)BLOCKTYPE.BT_L][(int)BLOCKDIR.BD_L] = new string[][]
        {
            new string[] {e, e, s},
            new string[] {s, s, s}
        };

        #endregion L

        #region S

        // S T
        AllBLock[(int)BLOCKTYPE.BT_S][(int)BLOCKDIR.BD_T] = new string[][]
        {
            new string[] {s, e},
            new string[] {s, s},
            new string[] {e, s}
        };
        // S R
        AllBLock[(int)BLOCKTYPE.BT_S][(int)BLOCKDIR.BD_R] = new string[][]
        {
            new string[] {e, s, s},
            new string[] {s, s, e}
        };
        // S B
        AllBLock[(int)BLOCKTYPE.BT_S][(int)BLOCKDIR.BD_B] = new string[][]
        {
            new string[] {s, e},
            new string[] {s, s},
            new string[] {e, s}
        };
        // S L
        AllBLock[(int)BLOCKTYPE.BT_S][(int)BLOCKDIR.BD_L] = new string[][]
        {
            new string[] {e, s, s},
            new string[] {s, s}
        };

        #endregion S

        #region z

        // Z T
        AllBLock[(int)BLOCKTYPE.BT_Z][(int)BLOCKDIR.BD_T] = new string[][]
        {
            new string[] {s, s, e},
            new string[] {e, s, s}
        };
        // Z R
        AllBLock[(int)BLOCKTYPE.BT_Z][(int)BLOCKDIR.BD_R] = new string[][]
        {
            new string[] {e, s},
            new string[] {s, s},
            new string[] {s, e}
        };
        // Z B
        AllBLock[(int)BLOCKTYPE.BT_Z][(int)BLOCKDIR.BD_B] = new string[][]
        {
            new string[] {s, s, e},
            new string[] {e, s, s}
        };
        // Z L
        AllBLock[(int)BLOCKTYPE.BT_Z][(int)BLOCKDIR.BD_L] = new string[][]
        {
            new string[] {e, s},
            new string[] {s, s},
            new string[] {s, e}
        };

        #endregion z

        #region O

        // O
        AllBLock[(int)BLOCKTYPE.BT_O][(int)BLOCKDIR.BD_T] = new string[][]
        {
            new string[] {s, s},
            new string[] {s, s}
        };
        AllBLock[(int)BLOCKTYPE.BT_O][(int)BLOCKDIR.BD_R] = new string[][]
        {
            new string[] {s, s},
            new string[] {s, s}
        };
        AllBLock[(int)BLOCKTYPE.BT_O][(int)BLOCKDIR.BD_B] = new string[][]
        {
            new string[] {s, s},
            new string[] {s, s}
        };
        AllBLock[(int)BLOCKTYPE.BT_O][(int)BLOCKDIR.BD_L] = new string[][]
        {
            new string[] {s, s},
            new string[] {s, s}
        };

        #endregion O
    }
}