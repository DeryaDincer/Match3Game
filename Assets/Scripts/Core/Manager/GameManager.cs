using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] public LevelSettings[] levelSettings;
    public bool IsRunning => isRunning;
    private bool isRunning = false;

    private void Start()
    {
        StartCoroutine(InitRoutine());
    }

    private IEnumerator InitRoutine()
    {
        if (!LevelController.IsInitialized)
        {
            LevelController.SetLevelDatas();

            yield return new WaitUntil(() => LevelController.IsInitialized);
            isRunning = true;
        }
    }
}