using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockGoalControllerSettings 
{
    [SerializeField] private AudioClip goalCollectAudio;
    [SerializeField] private List<Goal> gridGoals;

    public AudioClip GoalCollectAudio => goalCollectAudio;
    public List<Goal> GridGoals => gridGoals;
}
