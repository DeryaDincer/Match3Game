using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BlockGoalControllerSettings 
{
    [SerializeField] private List<Goal> blockGoals;
    public List<Goal> BlockGoals => blockGoals;
}
