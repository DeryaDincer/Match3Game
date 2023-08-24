using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeMessage : SubjectData
{
    public Block ClickBlock { get; set; }
    public int[] Direction { get; set; }

    public static SwipeMessage Create(Block clickBlock, int[] direction)
    {
        SwipeMessage swipeMessage = new SwipeMessage();
        swipeMessage.ClickBlock = clickBlock;
        swipeMessage.Direction = direction;
        return swipeMessage;
    }
}
