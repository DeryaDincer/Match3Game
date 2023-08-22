using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BlockGoalController
{
    public List<Goal> GridGoals { get; private set; }
    private List<GridGoalUI> GridGoalUiElements { get; set; }

    private AudioClip goalCollectAudio;
    private RectTransform gridUiElementsParent;
    private BlockGoalControllerReferences References;
    private BlockGoalControllerSettings Settings;

    [Inject]
    public void Construct(LevelSceneReferences references)
    {
        this.References = references.BlockGoalControllerReferences;
        this.gridUiElementsParent = References.GoalObjectsParent;
    }
  

    public async UniTask Initialized()
    {
        this.Settings = LevelController.GetCurrentLevel().SettingsInfo.BlockGoalControllerSettings;
        this.goalCollectAudio = Settings.GoalCollectAudio;
        GridGoals = new List<Goal>(Settings.GridGoals);
        GridGoalUiElements = new List<GridGoalUI>();
        StartAllGoals();
        SpawnUiElements();
        await UniTask.Yield();
    }

    //public void OnEntityDestroyed(IBlockEntity entity)
    //{
    //    entity.
    //    for (int i = 0; i < GridGoals.Count; i++)
    //    {
    //        Goal goal = GridGoals[i];
    //        GridGoalUI goalUI = GridGoalUiElements[i];

    //        if (goal.IsCompleted) continue;
    //        if (goal.entityType == entity.E)
    //        {
    //            goal.DecreaseGoal();
    //            CreateFlyingSpriteToGoal(entity, goalUI);
    //            if (goal.IsCompleted) CheckAllGoalsCompleted();
    //        }
    //    }
    //}

    //public void CreateFlyingSpriteToGoal(Ibloc entity, GridGoalUI goalUI)
    //{
    //    int goalAmount = goalUI.Goal.GoalLeft;
    //    UIEffectManager.I.CreateCurvyFlyingSprite(
    //        entity.EntityType.DefaultEntitySprite,
    //        entity.EntityTransform.GetComponent<RectTransform>().sizeDelta * 1.25f, // create bigger flying image for better visual representation
    //        entity.EntityTransform.position,
    //        goalUI.transform.position,
    //        UIEffectManager.CanvasLayer.OverEverything,
    //        () => OnFlyingSpriteReachGoal(goalAmount, goalUI));
    //}

    private void OnFlyingSpriteReachGoal(int goalAmount, GridGoalUI goalUI)
    {
        //AudioManager.Instance.PlayAudio(goalCollectAudio, AudioManager.PlayMode.Single, 1);
        goalUI.SetGoalAmount(goalAmount);
    }

    private void StartAllGoals()
    {
        foreach (Goal goal in GridGoals) goal.StartGoal();
    }
   

    private void SpawnUiElements()
    {
        int idx = 0;
        foreach (Goal goal in GridGoals)
        {
            GameObject newGo = PoolManager.Instance.GetObject<GridGoalUI>().gameObject;
            idx++;
            newGo.name = "cengiz" + "-----" + idx;
            newGo.transform.position = gridUiElementsParent.position;
            newGo.transform.SetParent(gridUiElementsParent);
            GridGoalUI goalUi = newGo.GetComponent<GridGoalUI>();
            goalUi.transform.localScale = Vector3.one;
            goalUi.SetupGoalUI(goal);
            GridGoalUiElements.Add(goalUi);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(gridUiElementsParent);
    }

    private void CheckAllGoalsCompleted()
    {
        foreach (Goal goal in GridGoals) if (!goal.IsCompleted) return;
       //bitir
    }
}
