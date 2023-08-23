using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public readonly struct MoveMadeSignal
{
    public readonly bool Click;
    public MoveMadeSignal(bool click)
    { 
        Click = click;
    }
}