using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BlockGoalController
{
    private List<Goal> blockGoals { get;  set; }
    private List<BlockGoalUI> goalUIList { get; set; }
    private RectTransform blockUiElementsParent;
    private BlockGoalControllerReferences references;
    private BlockGoalControllerSettings settings;
    private GenericMemoryPool<BlockGoalUI> memoryPool;
    private GameInUIEffectController gameInUIEffectController;

    [Inject]
    public void Construct(LevelSceneReferences references, GenericMemoryPool<BlockGoalUI> memoryPool, GameInUIEffectController gameInUIEffectController)
    {
        this.references = references.BlockGoalControllerReferences;
        this.blockUiElementsParent = this.references.GoalObjectsParent;
        this.memoryPool = memoryPool;
        this.gameInUIEffectController = gameInUIEffectController;
    }

    public async UniTask Initialized()
    {
        this.settings = LevelController.GetCurrentLevel().SettingsInfo.BlockGoalControllerSettings;
        blockGoals = new List<Goal>(settings.BlockGoals);
        goalUIList = new List<BlockGoalUI>();
        StartAllGoals();
        SpawnUiElements();
        await UniTask.Yield();
    }

    public void OnEntityDestroyed(IBlockEntityTypeDefinition entityType)
    {
        for (int i = 0; i < blockGoals.Count; i++)
        {
            Goal goal = blockGoals[i];
            BlockGoalUI goalUI = goalUIList[i];

            if (goal.IsCompleted) continue;
            if (goal.entityType == entityType)
            {
                goal.DecreaseGoal();
                CreateFlyingSpriteToGoal(entityType, goalUI);

                if (goal.IsCompleted)
                {
                    CheckAllGoalsCompleted();
                }
            }
        }
    }


    public void CreateFlyingSpriteToGoal(IBlockEntityTypeDefinition entity, BlockGoalUI goalUI)
    {
        int goalAmount = goalUI.Goal.GoalLeft;
        Sprite entitySprite = entity.EntitySpriteAtlas.GetSprite(entity.DefaultEntitySpriteName);

        gameInUIEffectController.CreateCurvyFlyingSprite(
           entitySprite,
            goalUI.transform.position,
            () => OnFlyingSpriteReachGoal(goalAmount, goalUI));
    }
    private void OnFlyingSpriteReachGoal(int goalAmount, BlockGoalUI goalUI)
    {
        //AudioManager.Instance.PlayAudio(goalCollectAudio, AudioManager.PlayMode.Single, 1);
        goalUI.SetGoalAmount(goalAmount);
    }

    private void StartAllGoals()
    {
        foreach (Goal goal in blockGoals) goal.StartGoal();
    }
   

    private void SpawnUiElements()
    {
        int idx = 0;
        foreach (Goal goal in blockGoals)
        {
            BlockGoalUI blockGoal = memoryPool.Spawn();
            idx++;
            blockGoal.gameObject.name = "-----" + idx;
            blockGoal.transform.position = blockUiElementsParent.position;
            blockGoal.transform.SetParent(blockUiElementsParent);
            blockGoal.transform.localScale = Vector3.one;
            blockGoal.SetupGoalUI(goal);
            goalUIList.Add(blockGoal);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(blockUiElementsParent);
    }

    private void CheckAllGoalsCompleted()
    {
        foreach (Goal goal in blockGoals) if (!goal.IsCompleted) return;
    }
}
