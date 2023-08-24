using UnityEngine;
using Zenject;

public class SaveLoadController
{
    #region Injection
    private LevelController levelController;

    [Inject]
    public void Construct(LevelController levelController)
    {
        this.levelController = levelController;
    }

    #endregion

    #region OVERALL LEVEL

    // Key for storing and retrieving the total level progress.
    private const string KEY_TOTAL_LEVEL = "jgh311asd";

    // Retrieves the current total level progress.
    public int GetTotalLevel() => PlayerPrefs.GetInt(KEY_TOTAL_LEVEL, 0);

    // Sets the total level progress.
    public void SetTotalLevel(int id)
    {
        PlayerPrefs.SetInt(KEY_TOTAL_LEVEL, id);
        levelController.SetLocalLevelIDs();
    }

    // Increases the total level progress by 1.
    public void IncreaseTotalLevel()
    {
        int id = GetTotalLevel() + 1;

        if (id > GetMaxTotalLevel())
        {
            SetMaxTotalLevel(id);
        }

        SetTotalLevel(id);
    }

    // Key for storing and retrieving the maximum total level reached.
    private const string KEY_MAX_TOTAL_LEVEL = "nasd213aa";

    // Retrieves the maximum total level reached.
    public int GetMaxTotalLevel() => PlayerPrefs.GetInt(KEY_MAX_TOTAL_LEVEL, 0);

    // Sets the maximum total level reached.
    public void SetMaxTotalLevel(int id) => PlayerPrefs.SetInt(KEY_MAX_TOTAL_LEVEL, id);

    #endregion
}
