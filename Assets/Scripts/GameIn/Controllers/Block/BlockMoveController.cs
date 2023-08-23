using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;
public class BlockMoveController :IInitializable, IDisposable
{
    public static readonly int MinGroupSizeForExplosion = 2;
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
    private void OnMoveMadeSignal( MoveMadeSignal signal)
    {
        MoveMade();
        if (!IsReadyForNextMove())
        {
            signalBus.Fire(new GameEndSignal());
        }
    }

    public void MoveMade()
    {
        moveCount--;
        UpdateMovesLeftUiText();
    }
    private void UpdateMovesLeftUiText()
    {
        movesLeftText.text = moveCount.ToString();
    }
    private bool IsReadyForNextMove()
    {
        return moveCount != 0;
    }
   // private void 

  



}
