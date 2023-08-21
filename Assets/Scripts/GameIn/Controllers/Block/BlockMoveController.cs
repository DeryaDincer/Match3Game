using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;
public class BlockMoveController 
{
    private TextMeshProUGUI moveText = null;
    [SerializeField] private int moveCount = 0;
    private GameInController GameInController = null;
   

    [Inject]
    public void Construct(LevelSettings settings, LevelSceneReferences references, GameInController gameInController)
    {
        GameInController = gameInController;
       // moveText = references.MoveText;
    
        //UpdateMoveText();
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
