using System;
using UnityEngine;

[Serializable]
public class BlockGoalControllerReferences
{
    [SerializeField] private RectTransform goalObjectsParent;
    public RectTransform GoalObjectsParent => goalObjectsParent;
}
