using Zenject;

// Enum to represent different states of the screen.
public enum ScreenState
{
    MainMenu,
    Game
}

// Controller responsible for managing the active screen state.
public class ScreenController : BaseController
{
    private MainMenuScreenView mainMenuScreen;
    private GameScreen gameScreen;

    #region Injection
    private ApplicationControllerSettings applicationControllerSettings;
    private BaseScreen.Factory screenFactory;

    [Inject]
    public void Construct(ApplicationControllerSettings applicationControllerSettings, BaseScreen.Factory screenFactory)
    {
        this.applicationControllerSettings = applicationControllerSettings;
        this.screenFactory = screenFactory;
    }
    #endregion

    private ScreenState currentState;

    // Changes the active screen state and creates the corresponding screen.
    public void ChangeState(ScreenState state)
    {
        CreateState(state);
    }

    // Creates the appropriate screen based on the current state.
    private void CreateState(ScreenState state)
    {
        ClearScreens();
        currentState = state;

        switch (currentState)
        {
            case ScreenState.MainMenu:
                CreateMainMenuScreen();
                break;
            case ScreenState.Game:
                CreateGameScreen();
                break;
        }

        // Notifies listeners about the change in screen state.
        signalBus.Fire(new ScreenStateChangedSignal
        {
            CurrentScreenState = currentState
        });
    }

    // Creates the main menu screen.
    private void CreateMainMenuScreen()
    {
        mainMenuScreen = (MainMenuScreenView)screenFactory.Create(applicationControllerSettings.MainMenuScreenPrefab);
        mainMenuScreen.Initialize();
    }

    // Creates the game screen.
    private void CreateGameScreen()
    {
        gameScreen = (GameScreen)screenFactory.Create(applicationControllerSettings.GameScreenPrefab);
        gameScreen.Initialize();
    }

    // Clears the currently active screens.
    public void ClearScreens()
    {
        if (gameScreen)
        {
            gameScreen.DestroyView();
            gameScreen = null;
        }
        if (mainMenuScreen)
        {
            mainMenuScreen.DestroyView();
            mainMenuScreen = null;
        }
    }

    protected override void OnInitialize()
    {
        // Implementation specific to the derived class can be added here.
    }

    protected override void OnApplicationReadyToStart()
    {
        // Implementation specific to the derived class can be added here.
    }

    protected override void OnDispose()
    {
        // Implementation specific to the derived class can be added here.
    }
}
