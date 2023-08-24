using UnityEngine;
using Zenject;

// A script to handle going back to the main menu.
public class BackToMainMenuButton:MonoBehaviour
{
    private bool clicked;

    #region Injection
    private SignalBus signalBus;
    private SaveLoadController saveLoadController;

    [Inject]
    public void Construct(SignalBus signalBus, SaveLoadController saveLoadController)
    {
        this.signalBus = signalBus;
        this.saveLoadController = saveLoadController;
    }
    #endregion

    public void Clicked(bool increaseLevel)
    {
        if (clicked) return;
        clicked = true;

        if (increaseLevel) saveLoadController.IncreaseTotalLevel();
        signalBus.Fire(new RequestBackToMainMenuSignal());
    }
}
