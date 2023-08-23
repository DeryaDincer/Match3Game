using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameEndCanvasController : IInitializable, IDisposable
{
    private GameEndCanvasControllerReferences references;
    private SignalBus signalBus;

    [Inject]
    public void Construct(LevelSceneReferences references, SignalBus signalBus)
    {
        this.references = references.GameEndCanvasControllerReferences;
        this.signalBus = signalBus;
    }

    public void Initialize()
    {
        signalBus.Subscribe<GameEndSignal>(OnGameEndSignal);
    }
    public void Dispose()
    {
        signalBus.Unsubscribe<GameEndSignal>(OnGameEndSignal);

    }
    private void OnGameEndSignal(GameEndSignal signal)
    {
        references.GameEndCanvas.gameObject.SetActive(true);
    }
}
