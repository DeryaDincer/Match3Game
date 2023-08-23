using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenuButton : MonoBehaviour
{
    public void BackToMainMenu(bool increaseLevel)
    {
        StartCoroutine(LoadMenuSceneAsync(increaseLevel));
    }

    public IEnumerator LoadMenuSceneAsync(bool increaseLevel)
    {
        if (increaseLevel)
        {
            SaveLoadManager.IncreaseTotalLevel();

        }

        var progress = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);

        while (!progress.isDone)
        {
            yield return null;
        }
    }
}
