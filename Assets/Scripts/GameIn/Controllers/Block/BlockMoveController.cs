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
            Debug.LogError("bitti");
        }
    }

    public void MoveMade()
    {
        moveCount--;
        UpdateMovesLeftUiText();
    }
   

    private bool IsReadyForNextMove()
    {
        return moveCount != 0;
    }
    private void UpdateMovesLeftUiText()
    {
        movesLeftText.text = moveCount.ToString();
    }

  



    //public void SetClickCell(EmptyCell EmptyCell)
    //{
    //    if (!EmptyCell)
    //    {
    //        return;
    //    }
    //    if (EmptyCell.ConnectedObject)
    //    {
    //        return;
    //    }
    //}


    //private void FinishMoveCount()
    //{
    //    if (moveCount != 0) return;

    //    InputManager.I.DisableInput();
    //    GameInController.GameInUIController.ShowFailedRoutine();
    //}
    //private void UpdateMoveText()
    //{
    //    moveText.text = moveCount.ToString();
    //}
    //private bool AreAllWallsDestroyed(List<Bomb> bombs, LevelManager.Level info)
    //{
    //    foreach (var bomb in bombs)
    //    {
    //        if (!AreSurroundingWallsDestroyed(bomb, info))
    //        {
    //            return false;
    //        }
    //    }

    //    return true;
    //}

    //private bool AreSurroundingWallsDestroyed(Bomb bomb, LevelManager.Level info)
    //{
    //    int rowCount = info.BoardInfo.RowCount;
    //    int columnCount = info.BoardInfo.ColumnCount;

    //    List<Wall> explodingWalls = new List<Wall>();

    //    for (int i = 0; i < bombs.Count; i++)
    //    {
    //        List<Wall> bombInteractWalls = GetWallNeighbors(rowCount, columnCount, bombs[i].ConnectedCell);

    //        for (int j = 0; j < bombInteractWalls.Count; j++)
    //        {
    //            if (!walls.Contains(bombInteractWalls[j]) && !explodingWalls.Contains(bombInteractWalls[j]))
    //            {
    //                explodingWalls.Add(bombInteractWalls[j]);
    //            }
    //        }
    //    }
    //    if (walls.Count == explodingWalls.Count)
    //    {
    //        return true;
    //    }
    //    return false;
    //}
}
