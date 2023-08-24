using UnityEngine;

[CreateAssetMenu(fileName = nameof(GamePoolSettings), menuName = AssetMenuName.SETTINGS + nameof(GamePoolSettings))]
public class GamePoolSettings : ScriptableObject
{
    public LevelButton LevelButtonPrefab;
    public int LevelButtonPoolSize;
    public string LevelButtonPoolName;

    [Space(20)]

    public Block BlockPrefab;
    public int BlockPoolSize;
    public string BlockPoolName;

    [Space(20)]

    public FlyingSprite FlyingSpritePrefab;
    public int FlyingSpritePoolSize;
    public string FlyingSpritePoolName;

    [Space(20)]

    public BlockGoalUI BlockGoalUIPrefab;
    public int BlockGoalUIPoolSize;
    public string BlockGoalUIPoolName;
}
