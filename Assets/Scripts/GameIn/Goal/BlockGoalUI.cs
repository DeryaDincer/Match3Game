using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlockGoalUI : MonoBehaviour, IPoolable
{
    [SerializeField] private ParticleSystem goalParticle;
    [SerializeField] private TMP_Text goalAmountLeftText;
    [SerializeField] private Image goalImage;
    [SerializeField] private Image goalCompletedImage;

    public Goal Goal { get; private set; }

    public void SetupGoalUI(Goal goal)
    {
        Goal = goal;

        Sprite entitySprite = goal.entityType.EntitySpriteAtlas.GetSprite(goal.entityType.DefaultEntitySpriteName);
        goalImage.sprite = entitySprite;
        SetGoalAmount(goal.GoalLeft, false);
    }

    public void SetGoalAmount(int goalAmount, bool playParticles = true)
    {
        if (playParticles) goalParticle.Play();


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
    public void OnSpawned() { }

    public void OnDespawned() { }
}

