using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BoardSpawnControllerReferences 
{
    [SerializeField] private SpriteRenderer backgroundBoardSprite;
    [SerializeField] private float backgroundBoardScaleOffset = .2f;
    public SpriteRenderer BackgroundBoardSprite => backgroundBoardSprite;
    public float BackgroundBoardScaleOffset => backgroundBoardScaleOffset;
}
