using UnityEngine;

[System.Serializable]
public class LevelSceneReferences
{
    [Group] [SerializeField] private BoardControllerReferences boardControllerReferences;
    [Group] [SerializeField] private BoardSpawnControllerReferences boardSpawnControllerReferences;
    [Group] [SerializeField] private GameInCameraControllerReferences gameInCameraControllerReferences;

    public BoardControllerReferences BoardControllerReferences => boardControllerReferences;
    public BoardSpawnControllerReferences BoardSpawnControllerReferences => boardSpawnControllerReferences;
    public GameInCameraControllerReferences GameInCameraControllerReferences => gameInCameraControllerReferences;

}