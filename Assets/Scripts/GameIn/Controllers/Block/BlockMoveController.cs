using System;
using TMPro;
using Zenject;

public class BlockMoveController : IInitializable, IDisposable
{
    private int moveCount { get; set; } 
    private TextMeshProUGUI movesLeftText; 
    private SignalBus signalBus; 

    [Inject]
    public void Construct(LevelSceneReferences references, SignalBus signalBus)
    {
        this.movesLeftText = references.BlockMoveControllerReferences.MovesLeftText;
        this.signalBus = signalBus;
    }

    public void Initialize()
    {
        moveCount = LevelController.GetCurrentLevel().SettingsInfo.BlockMoveControllerSettings.MoveCount;
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
}
