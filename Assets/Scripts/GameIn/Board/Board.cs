using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Board : BoardBase<Block>
{
  
}

public partial class Board : BoardBase<Block>
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    public List<int> LockedIds { get; private set; } = new List<int>();

    public Board(int width, int height, Block[] blocks, List<int> lockedIds)
    {
        Width = width;
        Height = height;
        ActiveBlocks = blocks;
        LockedIds = lockedIds;
    }
}