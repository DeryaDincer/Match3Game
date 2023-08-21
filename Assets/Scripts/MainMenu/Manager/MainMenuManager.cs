using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private LevelButton levelButton;
    [SerializeField] private Transform levelButtonContent;
    private List<LevelButton> levelButtons = new List<LevelButton>();
    private Coroutine activeCoroutine = null;


    private IEnumerator Start()
    {
        yield return new WaitUntil(() => GameManager.I.IsRunning);
        CreateLevelButtons();
    }
    public void OnStartGame(int levelID)
    {
        activeCoroutine = null;
        SaveLoadManager.SetTotalLevel(levelID);
        //if (activeCoroutine) StopCoroutine(activeCoroutine);

        activeCoroutine = StartCoroutine(LoadSceneAsync());

    }

    public IEnumerator LoadSceneAsync()
    {
        var progress = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

        while (!progress.isDone)
        {
            yield return null;
        }
    }

    private void CreateLevelButtons()
    {
        LevelController.LevelData Info = LevelController.GetLevelInfo();
        int Count = Info.Levels.Length;
        int maxLevelID = SaveLoadManager.GetMaxTotalLevel();

        for (int i = 0; i < Count; i++)
        {
            bool _isAvailable = false;
            int levelID = i;


            LevelButton levelBarElement = Instantiate(levelButton, levelButtonContent);
            levelButtons.Add(levelBarElement);
            if (levelID <= maxLevelID)
            {
                _isAvailable = true;
            }

            levelBarElement.Init(() => OnStartGame(levelID), levelID, _isAvailable, Info);


        }
    }
}