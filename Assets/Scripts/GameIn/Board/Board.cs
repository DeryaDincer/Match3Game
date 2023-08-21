using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Board : BoardBase<Block>
{
    //public class BoardPointPair
    //{
    //    public int BlockType;
    //    public int PointID;
    //    public bool LockedPoint;
    //    public Block ConnectedBlock;
    //    public BoardPointPair(int blockType, int pointID, bool lockedPoint)
    //    {
    //        this.BlockType = blockType;
    //        this.PointID = pointID;
    //        this.LockedPoint = lockedPoint;
    //    }
    //}
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