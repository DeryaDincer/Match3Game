using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameEndCanvasController : IInitializable, IDisposable
{
    #region Injection
    private LevelSceneReferences levelSceneReferences;
    private GameEndCanvasControllerReferences references;
    private SignalBus signalBus;
    private GameObject gameEndCanvas;

    [Inject]
    public void Construct(LevelSceneReferences levelSceneReferences, SignalBus signalBus)
    {
        this.levelSceneReferences = levelSceneReferences;
        this.signalBus = signalBus;
    }
    #endregion

    public void Initialize()
    {
        references = levelSceneReferences.GameEndCanvasControllerReferences;
        gameEndCanvas = references.GameEndCanvas.gameObject;

        signalBus.Subscribe<GameEndSignal>(OnGameEndSignal);
    }
    public void Dispose()
    {
        signalBus.Unsubscribe<GameEndSignal>(OnGameEndSignal);
    }

    // Called when the GameEndSignal is fired.
    private void OnGameEndSignal(GameEndSignal signal)
    {
        // Activate the game end canvas to display the end of the game.
        gameEndCanvas.gameObject.SetActive(true);
    }
}
