using UnityEditor;

[CustomEditor(typeof(LevelSettings))]
public class LevelConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelSettings levelSettings = (LevelSettings)target;
        GridStartLayout gridStartLayout = levelSettings.BoardSpawnControllerSettings.gridStartLayout;

        if (gridStartLayout.Width != levelSettings.BoardSpawnControllerSettings.Width || gridStartLayout.Height != levelSettings.BoardSpawnControllerSettings.Height)
        {
           levelSettings.BoardSpawnControllerSettings.gridStartLayout =
                new GridStartLayout((int)levelSettings.BoardSpawnControllerSettings.Width, (int)levelSettings.BoardSpawnControllerSettings.Height);
        }
    }
}