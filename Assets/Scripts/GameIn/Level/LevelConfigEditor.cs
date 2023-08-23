using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(LevelSettings))]
public class LevelConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Get the target LevelSettings scriptable object.
        LevelSettings levelSettings = (LevelSettings)target;

        // Get the grid start layout from the BoardSpawnControllerSettings.
        GridStartLayout gridStartLayout = levelSettings.BoardSpawnControllerSettings.gridStartLayout;

        // Check if the width or height of the grid start layout needs to be updated.
        if (gridStartLayout.Width != levelSettings.BoardSpawnControllerSettings.Width ||
            gridStartLayout.Height != levelSettings.BoardSpawnControllerSettings.Height)
        {
            // Update the grid start layout with new dimensions based on BoardSpawnControllerSettings.
            levelSettings.BoardSpawnControllerSettings.gridStartLayout =
                new GridStartLayout((int)levelSettings.BoardSpawnControllerSettings.Width,
                                    (int)levelSettings.BoardSpawnControllerSettings.Height);
        }
    }
}
#endif
