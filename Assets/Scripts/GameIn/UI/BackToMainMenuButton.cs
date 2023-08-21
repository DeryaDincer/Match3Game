using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenuButton : MonoBehaviour
{
    public void BackToMainMenu(bool on)
    {
        StartCoroutine(LoadMenuSceneAsync(on));
    }

    public IEnumerator LoadMenuSceneAsync(bool on)
    {
        int sceneIndex = on ? 0 : 1;
        var progress = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);

        while (!progress.isDone)
        {
            yield return null;
        }
    }
}
