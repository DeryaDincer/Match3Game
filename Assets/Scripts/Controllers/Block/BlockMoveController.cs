using System;
using TMPro;
using UnityEngine;
using Zenject;
public class BlockMoveController : IDisposable
{
    private int moveCount { get; set; }
    private TextMeshProUGUI movesLeftText;

    #region Injection
    private SignalBus signalBus;
    private LevelController levelController;
    private LevelSceneReferences levelSceneReferences;
    private BlockMoveControllerReferences references;

    [Inject]
    public void Construct(LevelSceneReferences levelSceneReferences, SignalBus signalBus, LevelController levelController)
    {
        this.levelSceneReferences = levelSceneReferences;
        this.signalBus = signalBus;
        this.levelController = levelController;
    }
    #endregion

    public void Initialize()
    {
        references = levelSceneReferences.BlockMoveControllerReferences;
        movesLeftText = references.MovesLeftText;

        moveCount = levelController.GetCurrentLevel().SettingsInfo.BlockMoveControllerSettings.MoveCount;
        UpdateMovesLeftUiText();

        signalBus.Subscribe<MoveMadeSignal>(OnMoveMadeSignal);
    }

    public void Dispose()
    {
        signalBus.Unsubscribe<MoveMadeSignal>(OnMoveMadeSignal);
    }

    // Callback function for the MoveMadeSignal.
    private void OnMoveMadeSignal(MoveMadeSignal signal)
    {
        MoveMade();
        if (!IsReadyForNextMove())
        {
            signalBus.Fire(new GameEndSignal());
        }
    }

    // Decrease the move count when a move is made.
    public void MoveMade()
    {
        moveCount--;
        UpdateMovesLeftUiText();
    }

    // Update the UI text displaying remaining moves.
    private void UpdateMovesLeftUiText()
    {
        movesLeftText.text = moveCount.ToString();
    }

    // Check if the player is ready for the next move based on the remaining move count.
    private bool IsReadyForNextMove()
    {
        return moveCount != 0;
    }

    // Set the TextMeshProUGUI element to display remaining moves.
    public void SetMoveText(TextMeshProUGUI text)
    {
        movesLeftText = text;
    }
}
