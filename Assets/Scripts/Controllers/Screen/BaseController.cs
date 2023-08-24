using System;
using Zenject;

public abstract class BaseController : IInitializable, IDisposable
{
    #region Injection
    protected SignalBus signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }
    #endregion

    // Initializes the BaseController by subscribing to the ApplicationReadyToStartSignal.
    public void Initialize()
    {
        OnInitialize();
    }

    // Invoked during the Initialize method.
    protected abstract void OnInitialize();

    // Invoked when the ApplicationReadyToStartSignal is received.
    protected abstract void OnApplicationReadyToStart();

    // Disposes of the BaseController by unsubscribing from the ApplicationReadyToStartSignal.
    public void Dispose()
    {
        OnDispose();
    }

    // Invoked during the Dispose method.
    protected abstract void OnDispose();
}
