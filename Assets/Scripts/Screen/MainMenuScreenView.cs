using UnityEngine;
using Zenject;

public class MainMenuScreenView : BaseScreen
{
    [SerializeField] Transform content;

    #region Injection
    private LevelController levelController;
    private MainMenuController mainMenuController;
    private SignalBus signalBus;

    [Inject]
    private void Construct(LevelController levelController, MainMenuController mainMenuController, SignalBus signalBus)
    {
        this.levelController = levelController;
        this.mainMenuController = mainMenuController;
        this.signalBus = signalBus;
    }
    #endregion

    public override void Initialize()
    {
        levelController.SetLevelDatas();
        signalBus.Subscribe<ScreenStateChangedSignal>(OnScreenStateChangedSignal);
    }

    public override void Dispose()
    {
        signalBus.Unsubscribe<ScreenStateChangedSignal>(OnScreenStateChangedSignal);
    }

    // Callback for the screen state change signal.
    private void OnScreenStateChangedSignal(ScreenStateChangedSignal signal)
    {
        if (signal.CurrentScreenState == ScreenState.MainMenu)
        {
            mainMenuController.CreateLevelButtons(content);
        }
    }
}
