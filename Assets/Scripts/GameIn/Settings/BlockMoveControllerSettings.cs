using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockMoveControllerSettings 
{
    [SerializeField] private int moveCount;
    public int MoveCount => moveCount;
}
