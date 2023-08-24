using System;
using UnityEngine;

[Serializable]
public class BlockMoveControllerSettings 
{
    [SerializeField] private int moveCount;
    public int MoveCount => moveCount;
}
