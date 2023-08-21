using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class LevelController
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
        public BoardControllerSettings BoardControllerSettings;
        public BoardSpawnControllerSettings BoardSpawnControllerSettings;
        public BlockAnimationControllerSettings BlockAnimationControllerSettings;
    }
}
public static partial class LevelController
{

    private static LevelData levelData = new LevelData();
    public static bool IsInitialized = false;

    public static LevelData GetLevelInfo() => levelData;
    public static Level GetCurrentLevel() => currentLevel;

    private static Level currentLevel;
    public static bool IslastLevelGame { get; set; }
    public static int CurrentLevelID { get; set; }

    private static LevelSettings[] LevelSettings;

    private static Level GetLevelByID(int levelID)
    {
        return levelData.Levels[levelID];
    }

    public static void SetLevelDatas()
    {
        LevelSettings = GameManager.I.levelSettings;
        int levelCount = LevelSettings.Length;
        levelData.Levels = new Level[levelCount];

        for (int i = 0; i < levelData.Levels.Length; i++)
        {
            //add fonksiyomu koy
            levelData.Levels[i] = new Level();
            levelData.Levels[i].SettingsInfo.BoardControllerSettings = LevelSettings[i].BoardControllerSettings;
            levelData.Levels[i].SettingsInfo.BoardSpawnControllerSettings = LevelSettings[i].BoardSpawnControllerSettings;
            levelData.Levels[i].SettingsInfo.BlockAnimationControllerSettings = LevelSettings[i].BlockAnimationControllerSettings;
        }

        //currentLevel = GetLevelByID(SaveLoadManager.GetTotalLevel());
        IsInitialized = true;
    }

    public static void SetLocalLevelIDs(bool isLastLevelGame = false)
    {
        IslastLevelGame = false;

        int totalLevelID = SaveLoadManager.GetTotalLevel();
        int sumLevel = 0;
        bool isLevelFound = false;


        sumLevel += levelData.Levels.Length;

        if (totalLevelID < sumLevel && !isLevelFound)
        {
            CurrentLevelID = totalLevelID - (sumLevel - levelData.Levels.Length);
            isLevelFound = true;

            if (CurrentLevelID == 0)
            {
                IslastLevelGame = true;
            }

            if (isLastLevelGame)
            {
                IslastLevelGame = true;
            }
        }


        if (!isLevelFound)
        {
            SaveLoadManager.SetTotalLevel(sumLevel - 1);
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