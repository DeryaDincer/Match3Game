using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockGoalControllerSettings 
{
    [SerializeField] private AudioClip goalCollectAudio;
    [SerializeField] private List<Goal> blockGoals;

    public AudioClip GoalCollectAudio => goalCollectAudio;
    public List<Goal> BlockGoals => blockGoals;
}
