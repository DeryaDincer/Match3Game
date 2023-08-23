using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// A script to handle going back to the main menu.
public class BackToMainMenuButton : MonoBehaviour
{
    public void BackToMainMenu(bool increaseLevel)
    {
        StartCoroutine(LoadMenuSceneAsync(increaseLevel));
    }

    public IEnumerator LoadMenuSceneAsync(bool increaseLevel)
    {
        // If the flag is set, increase the total level before going back to the menu.
        if (increaseLevel) SaveLoadManager.IncreaseTotalLevel();
      

        // Load the main menu scene asynchronously.
        var progress = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);

        // Wait until the loading progress is complete.
        while (!progress.isDone)
        {
            yield return null;
        }
    }
}
