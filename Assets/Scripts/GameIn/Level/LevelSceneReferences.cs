using UnityEngine;

[System.Serializable]
public class LevelSceneReferences
{
    [Group] [SerializeField] private BoardControllerReferences boardControllerReferences;
    [Group] [SerializeField] private BoardSpawnControllerReferences boardSpawnControllerReferences;
    [Group] [SerializeField] private GameInCameraControllerReferences gameInCameraControllerReferences;
    [Group] [SerializeField] private BlockGoalControllerReferences blockGoalControllerReferences;
    [Group] [SerializeField] private GameInUIEffectControllerReferences gameInUIEffectControllerReferences;
    [Group] [SerializeField] private BlockMoveControllerReferences blockMoveControllerReferences;

    public BoardControllerReferences BoardControllerReferences => boardControllerReferences;
    public BoardSpawnControllerReferences BoardSpawnControllerReferences => boardSpawnControllerReferences;
    public GameInCameraControllerReferences GameInCameraControllerReferences => gameInCameraControllerReferences;
    public BlockGoalControllerReferences BlockGoalControllerReferences => blockGoalControllerReferences;
    public GameInUIEffectControllerReferences GameInUIEffectControllerReferences => gameInUIEffectControllerReferences;
    public BlockMoveControllerReferences BlockMoveControllerReferences => blockMoveControllerReferences;

}