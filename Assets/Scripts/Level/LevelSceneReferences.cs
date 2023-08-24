using Cysharp.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class LevelSceneReferences
{
    [Group] [SerializeField] private BoardSpawnControllerReferences boardSpawnControllerReferences;
    [Group] [SerializeField] private GameInCameraControllerReferences gameInCameraControllerReferences;
    [Group] [SerializeField] private BlockGoalControllerReferences blockGoalControllerReferences;
    [Group] [SerializeField] private GameInUIEffectControllerReferences gameInUIEffectControllerReferences;
    [Group] [SerializeField] private BlockMoveControllerReferences blockMoveControllerReferences;
    [Group] [SerializeField] private GameEndCanvasControllerReferences gameEndCanvasControllerReferences;

    public BoardSpawnControllerReferences BoardSpawnControllerReferences => boardSpawnControllerReferences;
    public GameInCameraControllerReferences GameInCameraControllerReferences => gameInCameraControllerReferences;
    public BlockGoalControllerReferences BlockGoalControllerReferences => blockGoalControllerReferences;
    public GameInUIEffectControllerReferences GameInUIEffectControllerReferences => gameInUIEffectControllerReferences;
    public BlockMoveControllerReferences BlockMoveControllerReferences => blockMoveControllerReferences;
    public GameEndCanvasControllerReferences GameEndCanvasControllerReferences => gameEndCanvasControllerReferences;

    public void SetLevelReferences(LevelSceneReferences levelSceneReferences)
    {
        boardSpawnControllerReferences = levelSceneReferences.BoardSpawnControllerReferences; 
        gameInCameraControllerReferences = levelSceneReferences.GameInCameraControllerReferences; 
        blockGoalControllerReferences = levelSceneReferences.BlockGoalControllerReferences; 
        gameInUIEffectControllerReferences = levelSceneReferences.GameInUIEffectControllerReferences;
        blockMoveControllerReferences = levelSceneReferences.BlockMoveControllerReferences; 
        gameEndCanvasControllerReferences = levelSceneReferences.GameEndCanvasControllerReferences;
    }
}