using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public partial class LevelController
{
    public struct LevelData
    {
        public Level[] Levels;
        public LevelData(Level[] levels)
        {
            Levels = levels;
        }
    }
    public struct Level
    {
        public Settings SettingsInfo;
    }
    public struct Settings
    {
        public BoardSpawnControllerSettings BoardSpawnControllerSettings;
        public BlockGoalControllerSettings BlockGoalControllerSettings;
        public BlockMoveControllerSettings BlockMoveControllerSettings;
    }
}
public partial class LevelController
{
    private LevelData levelData = new LevelData();
    public bool IsInitialized = false;
    public LevelData GetLevelInfo() => levelData;
    public Level GetCurrentLevel() => currentLevel;

    private Level currentLevel;
    public bool IslastLevelGame { get; set; }
    public int CurrentLevelID { get; set; }

    #region Injection
    private LevelSettings[] LevelSettings;
    private SaveLoadController saveLoadController;

    [Inject]
    public void Construct(ApplicationLevelSettings applicationLevelSettings, SaveLoadController saveLoadController)
    {
        this.LevelSettings = applicationLevelSettings.LevelSettings;
        this.saveLoadController = saveLoadController;
    }
    #endregion

    // Method to retrieve a level's data by its ID.
    private Level GetLevelByID(int levelID)
    {
        return levelData.Levels[levelID];
    }

    // Method to set level data based on the game manager's settings.
    public void SetLevelDatas()
    { 
        // Retrieve the level settings from the GameManager instance.
        int levelCount = LevelSettings.Length;

        // Create an array to store level data.
        levelData.Levels = new Level[levelCount];

        // Populate the level data array with settings from GameManager.
        for (int i = 0; i < levelData.Levels.Length; i++)
        {
            levelData.Levels[i] = new Level();
            levelData.Levels[i].SettingsInfo.BoardSpawnControllerSettings = LevelSettings[i].BoardSpawnControllerSettings;
            levelData.Levels[i].SettingsInfo.BlockGoalControllerSettings = LevelSettings[i].BlockGoalControllerSettings;
            levelData.Levels[i].SettingsInfo.BlockMoveControllerSettings = LevelSettings[i].BlockMoveControllerSettings;
        }

        // Mark level data as initialized.
        IsInitialized = true;
        SetLocalLevelIDs();
    }

    // Method to set local level IDs and determine if the last level is being played.
    public void SetLocalLevelIDs(bool isLastLevelGame = false)
    {
        IslastLevelGame = false;   // Reset the flag for playing the last level.

        int totalLevelID = saveLoadController.GetTotalLevel();
        int sumLevel = 0;
        bool isLevelFound = false;

        // Calculate the total number of levels.
        sumLevel += levelData.Levels.Length;

        // Check if the total level ID is within the range of available levels.
        if (totalLevelID < sumLevel && !isLevelFound)
        {
            // Calculate the current level ID and mark it as found.
            CurrentLevelID = totalLevelID - (sumLevel - levelData.Levels.Length);
            isLevelFound = true;

            // Check if the current level is the first level.
            if (CurrentLevelID == 0)
            {
                IslastLevelGame = true;   // Mark that the last level is being played.
            }

            // Check if the last level is being played.
            if (isLastLevelGame)
            {
                IslastLevelGame = true;   // Mark that the last level is being played.
            }
        }

        // If the level ID is not found, set it to the last available level ID.
        if (!isLevelFound)
        {
            saveLoadController.SetTotalLevel(sumLevel - 1);
            Debug.Log("Loaded" + "-------------------->" + (sumLevel - 1));
            SetLocalLevelIDs(true);
        }
        else
        {
            Debug.Log("Loaded" + "-------------------->" + (CurrentLevelID));
            currentLevel = GetLevelByID(CurrentLevelID);
        }
    }



}