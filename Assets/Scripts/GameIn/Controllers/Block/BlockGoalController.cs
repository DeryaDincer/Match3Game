using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BlockGoalController : IInitializable
{
    private List<Goal> blockGoals { get; set; }
    private List<BlockGoalUI> goalUIList { get; set; }
    private RectTransform blockUiElementsParent;
    private BlockGoalControllerReferences references;
    private BlockGoalControllerSettings settings;
    private GenericMemoryPool<BlockGoalUI> memoryPool;
    private GameInUIEffectController gameInUIEffectController;
    private SignalBus signalBus;

    [Inject]
    public void Construct(LevelSceneReferences references, GenericMemoryPool<BlockGoalUI> memoryPool, GameInUIEffectController gameInUIEffectController, SignalBus signalBus)
    {
        this.references = references.BlockGoalControllerReferences;
        this.blockUiElementsParent = this.references.GoalObjectsParent;
        this.memoryPool = memoryPool;
        this.gameInUIEffectController = gameInUIEffectController;
        this.signalBus = signalBus;
    }

    public void Initialize()
    {
        this.settings = LevelController.GetCurrentLevel().SettingsInfo.BlockGoalControllerSettings;
        blockGoals = new List<Goal>(settings.BlockGoals);
        goalUIList = new List<BlockGoalUI>();
        // Start all block goals.
        StartAllGoals();
        // Spawn UI elements for each block goal.
        SpawnUiElements();
    }

    // Handle the event when an entity is destroyed.
    public void OnEntityDestroyed(IBlockEntityTypeDefinition entityType)
    {
        // Loop through all block goals to check and update their completion status.
        for (int i = 0; i < blockGoals.Count; i++)
        {
            Goal goal = blockGoals[i];
            BlockGoalUI goalUI = goalUIList[i];

            if (goal.IsCompleted) continue;
            if (goal.entityType == entityType)
            {
                goal.DecreaseGoal();
                // Create a flying sprite effect towards the goal.
                CreateFlyingSpriteToGoal(entityType, goalUI);
            }
        }

        // Check if all block goals are completed, and signal game end if true.
        if (CheckAllGoalsCompleted())
        {
            signalBus.Fire(new GameEndSignal());
        }
    }

    // Create a flying sprite effect towards a goal.
    public void CreateFlyingSpriteToGoal(IBlockEntityTypeDefinition entity, BlockGoalUI goalUI)
    {
        int goalAmount = goalUI.Goal.GoalLeft;
        Sprite entitySprite = entity.EntitySpriteAtlas.GetSprite(entity.DefaultEntitySpriteName);

        gameInUIEffectController.CreateCurvyFlyingSprite(
            entitySprite,
            goalUI.transform.position,
            () => OnFlyingSpriteReachGoal(goalAmount, goalUI));
    }

    // Handle the event when a flying sprite reaches a goal.
    private void OnFlyingSpriteReachGoal(int goalAmount, BlockGoalUI goalUI)
    {
        goalUI.SetGoalAmount(goalAmount);
    }

    // Start all block goals.
    private void StartAllGoals()
    {
        foreach (Goal goal in blockGoals) goal.StartGoal();
    }

    // Spawn UI elements for each block goal.
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

    // Check if all block goals are completed.
    private bool CheckAllGoalsCompleted()
    {
        foreach (Goal goal in blockGoals) if (!goal.IsCompleted) return false;
        return true;
    }
}
