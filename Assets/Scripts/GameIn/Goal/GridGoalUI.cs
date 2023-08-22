using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridGoalUI : MonoBehaviour, IPoolable
{

    [SerializeField] private ParticleSystem psGridGoal;
    [SerializeField] private TMP_Text goalAmountLeftText;
    [SerializeField] private Image goalImage;
    [SerializeField] private Image goalCompletedImage;

    public Goal Goal { get; private set; }

    public void SetupGoalUI(Goal goal)
    {
        Goal = goal;
        goalImage.sprite = goal.entityType.DefaultEntitySprite;
        SetGoalAmount(goal.GoalLeft, false);
    }

    public void SetGoalAmount(int goalAmount, bool playParticles = true)
    {
        if (playParticles)
            psGridGoal.Play();
        if (goalAmount == 0)
        {
            goalCompletedImage.enabled = true;
            goalAmountLeftText.text = "";
        }
        else
        {
            goalCompletedImage.enabled = false;
            goalAmountLeftText.text = goalAmount.ToString();
        }
    }
    public void OnSpawned()
    {
        gameObject.SetActive(true);
    }

    public void OnDespawned()
    {
        transform.SetParent(null);
        gameObject.SetActive(false);
    }
}
