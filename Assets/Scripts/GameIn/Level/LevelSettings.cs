using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Level Settings")]
public class LevelSettings : ScriptableObject
{
    [Group] [SerializeField] private BoardSpawnControllerSettings boardSpawnControllerSettings;
    [Group] [SerializeField] private BlockGoalControllerSettings blockGoalControllerSettings;
    [Group] [SerializeField] private BlockMoveControllerSettings blockMoveControllerSettings;
    
    public BoardSpawnControllerSettings BoardSpawnControllerSettings => boardSpawnControllerSettings;
    public BlockMoveControllerSettings BlockMoveControllerSettings => blockMoveControllerSettings;
    public BlockGoalControllerSettings BlockGoalControllerSettings => blockGoalControllerSettings;
}
