using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuController:IInitializable
{
    [SerializeField] private LevelButton levelButton;           // Reference to the LevelButton prefab.
    [SerializeField] private Transform levelButtonContent;      // Parent transform for level buttons.
    private List<LevelButton> levelButtons = new List<LevelButton>();   // List to store instantiated level buttons.

    #region Injection
    private LevelController levelController;
    private SaveLoadController saveLoadController;
    private LevelButton.Factory levelButtonFactory;

    [Inject]
    public void Construct(LevelController levelController, SaveLoadController saveLoadController, LevelButton.Factory levelButtonFactory)
    {
        this.levelController = levelController;
        this.saveLoadController = saveLoadController;
        this.levelButtonFactory = levelButtonFactory;
    }
    #endregion 

    // Method to start the game with a specific level ID.
    public void OnStartGame(int levelID)
    {
        saveLoadController.SetTotalLevel(levelID);   // Set the selected level as the total level.
    }

    // Method to create level buttons based on available levels.
    public void CreateLevelButtons(Transform parent)
    {
        LevelController.LevelData info = levelController.GetLevelInfo();
        int Count = info.Levels.Length;
        int maxLevelID = saveLoadController.GetMaxTotalLevel();

        for (int i = 0; i < Count; i++)
        {
            bool _isAvailable = false;
            int levelID = i;

            // Instantiate a new LevelButton and add it to the list.
            LevelButton levelBarElement = levelButtonFactory.Create();
           // LevelButton levelBarElement = Instantiate(levelButton, levelButtonContent);
            levelButtons.Add(levelBarElement);
            levelBarElement.SetParent(parent);
            // Check if the level is available.
            if (levelID <= maxLevelID)
            {
                _isAvailable = true;
            }
            // Initialize the LevelButton with appropriate parameters.
            levelBarElement.Init(() => OnStartGame(levelID), levelID, _isAvailable, info);
        }
    }

    public void Initialize()
    {
       
    }
}
