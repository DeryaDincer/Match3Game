using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
using TMPro;
using Cysharp.Threading.Tasks;

public class GameScreen : BaseScreen
{
    [Group] [SerializeField] private LevelSceneReferences levelReferences;

    #region Injection
    private BoardSpawnController boardSpawnController;
    private BoardController boardController;
    private GameInCameraController gameInCameraController;
    private BlockGoalController blockGoalController;
    private BlockMoveController blockMoveController;
    private BlockAnimationController blockAnimationController;
    private GameInUIEffectController gameInUIEffectController;
    private GameEndCanvasController gameEndCanvasController;
    private SignalBus signalBus;
    private LevelSceneReferences references;

    [Inject]
    public void Construct(BoardSpawnController boardSpawnController,
        BoardController boardController,
        GameInCameraController gameInCameraController,
        BlockGoalController blockGoalController,
        BlockMoveController blockMoveController,
        BlockAnimationController blockAnimationController,
        GameInUIEffectController gameInUIEffectController,
        GameEndCanvasController gameEndCanvasController,
        SignalBus signalBus,
        LevelSceneReferences references)
    {
        this.boardSpawnController = boardSpawnController;
        this.boardController = boardController;
        this.gameInCameraController = gameInCameraController;
        this.blockGoalController = blockGoalController;
        this.blockMoveController = blockMoveController;
        this.blockAnimationController = blockAnimationController;
        this.gameInUIEffectController = gameInUIEffectController;
        this.gameEndCanvasController = gameEndCanvasController;
        this.signalBus = signalBus;
        this.references = references;
    }
    #endregion

    public override void Initialize()
    {
        signalBus.Subscribe<ScreenStateChangedSignal>(OnScreenStateChangedSignal);
    }

    public override void Dispose()
    {
        DeinitializeAllGameIn();
        signalBus.Unsubscribe<ScreenStateChangedSignal>(OnScreenStateChangedSignal);
    }

    // Callback for the screen state change signal.
    private void OnScreenStateChangedSignal(ScreenStateChangedSignal signal)
    {
        if (signal.CurrentScreenState == ScreenState.Game)
        {
            references.SetLevelReferences(levelReferences);
            InitializeAllGameIn();
        }
    }

    // Initialize all game-related components.
    private async void InitializeAllGameIn()
    {
        await UniTask.Yield();

        boardSpawnController.Initialize();
        boardController.Initialize();
        gameInCameraController.Initialize();
        blockGoalController.Initialize();
        blockMoveController.Initialize();
        blockAnimationController.Initialize();
        gameInUIEffectController.Initialize();
        gameEndCanvasController.Initialize();
    }

    // Deinitialize all game-related components.
    private void DeinitializeAllGameIn()
    {
        boardSpawnController.Dispose();
        boardController.Dispose();
        blockMoveController.Dispose();
        gameEndCanvasController.Dispose();
    }
}
