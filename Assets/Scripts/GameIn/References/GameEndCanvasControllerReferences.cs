using System;
using UnityEngine;

[Serializable]
public class GameEndCanvasControllerReferences
{
    [SerializeField] private Canvas gameEndCanvas;
    public Canvas GameEndCanvas => gameEndCanvas;
}
