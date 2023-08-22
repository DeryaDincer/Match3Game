using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockGoalControllerReferences
{
    [SerializeField] private RectTransform goalObjectsParent;
    public RectTransform GoalObjectsParent => goalObjectsParent;
   
}
