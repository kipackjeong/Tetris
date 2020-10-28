using System.Collections.Generic;
public enum BLOCKDIR // Block Directions
{
    BD_T,
    BD_R,
    BD_B,
    BD_L,
    BD_MAX
}

public enum BLOCKTYPE // Block Types
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

    private List<List<string[][]>> AllBlock = new List<List<string[][]>>(); // container to keep all the blocks/ each type has 4 directions

    private void Datalnit() // store all the blocks in AllBlock List
    {
        // Create Block Container.
        for (int BT = 0; BT < (int)BLOCKTYPE.BT_MAX; ++BT)
        {
            AllBlock.Add(new List<string[][]>());
            for (int BD = 0; BD < (int)BLOCKDIR.BD_MAX; ++BD)
            {
                AllBlock[BT].Add(null);
            }
        }

        // Block components
        string s = "▦";
        var e = "□";

        // Blocks By BLOCKTYPE
        #region l

        //l T
        AllBlock[(int)BLOCKTYPE.BT_l][(int)BLOCKDIR.BD_T] = new string[][]
        {
            new string[] {s},
            new string[] {s},
            new string[] {s},
            new string[] {s}
        };
        //l R
        AllBlock[(int)BLOCKTYPE.BT_l][(int)BLOCKDIR.BD_R] = new string[][]
        {
            new string[] {s, s, s, s}
        };
        //l B
        AllBlock[(int)BLOCKTYPE.BT_l][(int)BLOCKDIR.BD_B] = new string[][]
        {
            new string[] {s},
            new string[] {s},
            new string[] {s},
            new string[] {s}
        };
        // l L
        AllBlock[(int)BLOCKTYPE.BT_l][(int)BLOCKDIR.BD_L] = new string[][]
        {
            new string[] {s, s, s, s}
        };

        #endregion l

        #region T

        // T T
        
        AllBlock[(int)BLOCKTYPE.BT_T][(int)BLOCKDIR.BD_T] = new string[][]
        {
            new string[] {e, s},
            new string[] {s, s, s}
        };
        // T R
        AllBlock[(int)BLOCKTYPE.BT_T][(int)BLOCKDIR.BD_R] = new string[][]
         {
             new string[] {s},
             new string[] {s, s},
             new string[] {s}
        };
        // T B
        AllBlock[(int)BLOCKTYPE.BT_T][(int)BLOCKDIR.BD_B] = new string[][]
        {
            new string[] {s, s, s},
            new string[] {e, s}
        };
        // T L
        AllBlock[(int)BLOCKTYPE.BT_T][(int)BLOCKDIR.BD_L] = new string[][]
        {
            new string[] {e, s},
            new string[] {s, s},
            new string[] {e, s}
        };

        #endregion T

        #region J

        // J T
        AllBlock[(int)BLOCKTYPE.BT_J][(int)BLOCKDIR.BD_T] = new string[][]
        {
            new string[] {e, s},
            new string[] {e, s},
            new string[] {s, s}
        };
        // J R
        AllBlock[(int)BLOCKTYPE.BT_J][(int)BLOCKDIR.BD_R] = new string[][]
        {
            new string[] {s, e, e},
            new string[] {s, s, s}
        };
        // J B
        AllBlock[(int)BLOCKTYPE.BT_J][(int)BLOCKDIR.BD_B] = new string[][]
        {
            new string[] {s, s},
            new string[] {s},
            new string[] {s}
        };
        // J L
        AllBlock[(int)BLOCKTYPE.BT_J][(int)BLOCKDIR.BD_L] = new string[][]
        {
            new string[] {s, s, s},
            new string[] {e, e, s}
        };

        #endregion J

        #region L

        // L T
        AllBlock[(int)BLOCKTYPE.BT_L][(int)BLOCKDIR.BD_T] = new string[][]
        {
            new string[] {s},
            new string[] {s},
            new string[] {s, s}
        };
        // L R
        AllBlock[(int)BLOCKTYPE.BT_L][(int)BLOCKDIR.BD_R] = new string[][]
        {
            new string[] {s, s, s},
            new string[] {s}
        };
        // L B
        AllBlock[(int)BLOCKTYPE.BT_L][(int)BLOCKDIR.BD_B] = new string[][]
        {
            new string[] {s, s},
            new string[] {e, s},
            new string[] {e, s}
        };
        // L L
        AllBlock[(int)BLOCKTYPE.BT_L][(int)BLOCKDIR.BD_L] = new string[][]
        {
            new string[] {e, e, s},
            new string[] {s, s, s}
        };

        #endregion L

        #region S

        // S T
        AllBlock[(int)BLOCKTYPE.BT_S][(int)BLOCKDIR.BD_T] = new string[][]
        {
            new string[] {s, e},
            new string[] {s, s},
            new string[] {e, s}
        };
        // S R
        AllBlock[(int)BLOCKTYPE.BT_S][(int)BLOCKDIR.BD_R] = new string[][]
        {
            new string[] {e, s, s},
            new string[] {s, s, e}
        };
        // S B
        AllBlock[(int)BLOCKTYPE.BT_S][(int)BLOCKDIR.BD_B] = new string[][]
        {
            new string[] {s, e},
            new string[] {s, s},
            new string[] {e, s}
        };
        // S L
        AllBlock[(int)BLOCKTYPE.BT_S][(int)BLOCKDIR.BD_L] = new string[][]
        {
            new string[] {e, s, s},
            new string[] {s, s}
        };

        #endregion S

        #region z

        // Z T
        AllBlock[(int)BLOCKTYPE.BT_Z][(int)BLOCKDIR.BD_T] = new string[][]
        {
            new string[] {s, s, e},
            new string[] {e, s, s}
        };
        // Z R
        AllBlock[(int)BLOCKTYPE.BT_Z][(int)BLOCKDIR.BD_R] = new string[][]
        {
            new string[] {e, s},
            new string[] {s, s},
            new string[] {s, e}
        };
        // Z B
        AllBlock[(int)BLOCKTYPE.BT_Z][(int)BLOCKDIR.BD_B] = new string[][]
        {
            new string[] {s, s, e},
            new string[] {e, s, s}
        };
        // Z L
        AllBlock[(int)BLOCKTYPE.BT_Z][(int)BLOCKDIR.BD_L] = new string[][]
        {
            new string[] {e, s},
            new string[] {s, s},
            new string[] {s, e}
        };

        #endregion z

        #region O

        // O
        AllBlock[(int)BLOCKTYPE.BT_O][(int)BLOCKDIR.BD_T] = new string[][]
        {
            new string[] {s, s},
            new string[] {s, s}
        };
        AllBlock[(int)BLOCKTYPE.BT_O][(int)BLOCKDIR.BD_R] = new string[][]
        {
            new string[] {s, s},
            new string[] {s, s}
        };
        AllBlock[(int)BLOCKTYPE.BT_O][(int)BLOCKDIR.BD_B] = new string[][]
        {
            new string[] {s, s},
            new string[] {s, s}
        };
        AllBlock[(int)BLOCKTYPE.BT_O][(int)BLOCKDIR.BD_L] = new string[][]
        {
            new string[] {s, s},
            new string[] {s, s}
        };

        #endregion O
    }
}