using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Level Settings")]
public class LevelSettings : ScriptableObject
{
    [Group] [SerializeField] private BoardControllerSettings boardControllerSettings;
    [Group] [SerializeField] private BoardSpawnControllerSettings boardSpawnControllerSettings;
    [Group] [SerializeField] private BlockAnimationControllerSettings blockAnimationControllerSettings;
    [Group] [SerializeField] private BlockGoalControllerSettings blockGoalControllerSettings;
    [Group] [SerializeField] private BlockMoveControllerSettings blockMoveControllerSettings;
    

    public BoardControllerSettings BoardControllerSettings => boardControllerSettings;
    public BoardSpawnControllerSettings BoardSpawnControllerSettings => boardSpawnControllerSettings;
    public BlockAnimationControllerSettings BlockAnimationControllerSettings => blockAnimationControllerSettings;
    public BlockMoveControllerSettings BlockMoveControllerSettings => blockMoveControllerSettings;
    public BlockGoalControllerSettings BlockGoalControllerSettings => blockGoalControllerSettings;
}
