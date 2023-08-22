using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;
public class BlockMoveController 
{
   
   
    public static readonly int MinGroupSizeForExplosion = 2;
    public int MovesLeft { get; private set; }
    private TextMeshProUGUI movesLeftText;

    [Inject]
    public void Construct(LevelSceneReferences references)
    {
        this.movesLeftText = references.BlockMoveControllerReferences.MovesLeftText;
      
       // moveText = references.MoveText;
        //UpdateMoveText();
    }


    public async UniTask Initialized()
    {
        MovesLeft = LevelController.GetCurrentLevel().SettingsInfo.BlockMoveControllerSettings.MoveCount;
        UpdateMovesLeftUiText();
        await UniTask.Yield();
    }
   

    public bool TryMakeMatchMove(Block blockEntity)
    {
        if (MovesLeft == 0) return false;

        //count
        //if (blockEntity.CurrentMatchGroup.Count < MinGroupSizeForExplosion) return false;

        MovesLeft--;
        UpdateMovesLeftUiText();
        return true;
    }

    public void ClickedPowerUp()
    {
        MovesLeft--;
        UpdateMovesLeftUiText();
    }
   

    private void OnGridReadyForNextMove()
    {
        if (MovesLeft != 0) return;
       //Failed
    }
    private void UpdateMovesLeftUiText()
    {
        movesLeftText.text = MovesLeft.ToString();
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
