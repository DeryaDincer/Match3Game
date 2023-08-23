using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DespawnAnimationSignal
{
    public bool Click;
    public DespawnAnimationSignal(bool click)
    {
        Click = click;
    }
}