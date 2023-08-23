using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private LevelButton levelButton;           // Reference to the LevelButton prefab.
    [SerializeField] private Transform levelButtonContent;      // Parent transform for level buttons.
    private List<LevelButton> levelButtons = new List<LevelButton>();   // List to store instantiated level buttons.
    private Coroutine activeCoroutine = null;                    // Reference to the active coroutine.

    private IEnumerator Start()
    {
        // Wait until the game manager is running.
        yield return new WaitUntil(() => GameManager.Instance.IsRunning);

        // Create level buttons once the game manager is running.
        CreateLevelButtons();
    }

    // Method to start the game with a specific level ID.
    public void OnStartGame(int levelID)
    {
        activeCoroutine = null;
        SaveLoadManager.SetTotalLevel(levelID);   // Set the selected level as the total level.
        activeCoroutine = StartCoroutine(LoadSceneAsync());   // Start loading the game scene asynchronously.
    }

    // Coroutine to load the game scene asynchronously.
    public IEnumerator LoadSceneAsync()
    {
        var progress = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

        while (!progress.isDone)
        {
            yield return null;
        }
    }

    // Method to create level buttons based on available levels.
    private void CreateLevelButtons()
    {
        LevelController.LevelData info = LevelController.GetLevelInfo();
        int Count = info.Levels.Length;
        int maxLevelID = SaveLoadManager.GetMaxTotalLevel();

        for (int i = 0; i < Count; i++)
        {
            bool _isAvailable = false;
            int levelID = i;

            // Instantiate a new LevelButton and add it to the list.
            LevelButton levelBarElement = Instantiate(levelButton, levelButtonContent);
            levelButtons.Add(levelBarElement);

            // Check if the level is available.
            if (levelID <= maxLevelID)
            {
                _isAvailable = true;
            }

            // Initialize the LevelButton with appropriate parameters.
            levelBarElement.Init(() => OnStartGame(levelID), levelID, _isAvailable, info);
        }
    }
}
