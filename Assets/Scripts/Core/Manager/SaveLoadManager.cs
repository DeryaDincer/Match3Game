using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLoadManager
{
    #region OVERALL LEVEL

    const string KEY_TOTAL_LEVEL = "jgh311asd";

    public static int GetTotalLevel() => PlayerPrefs.GetInt(KEY_TOTAL_LEVEL, 0);
    public static void SetTotalLevel(int id)
    {
        PlayerPrefs.SetInt(KEY_TOTAL_LEVEL, id);
        LevelController.SetLocalLevelIDs();
    }
    public static void IncreaseTotalLevel()
    {
        int id = GetTotalLevel() + 1;

        if (id > GetMaxTotalLevel())
        {
            SetMaxTotalLevel(id);
        }

        SetTotalLevel(id);
    }

    const string KEY_MAX_TOTAL_LEVEL = "nasd213aa";

    public static int GetMaxTotalLevel() => PlayerPrefs.GetInt(KEY_MAX_TOTAL_LEVEL, 0);
    public static void SetMaxTotalLevel(int id) => PlayerPrefs.SetInt(KEY_MAX_TOTAL_LEVEL, id);


    #endregion
}