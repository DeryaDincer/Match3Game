using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


public partial class LevelButton : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
{
    public class Factory : PlaceholderFactory<LevelButton> { }

    public class Pool : MonoPoolableMemoryPool<IMemoryPool, LevelButton> { }
}

public partial class LevelButton : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
{
    [SerializeField] private Button button;                   // Reference to the Button component.
    [SerializeField] private TextMeshProUGUI buttonText;      // Reference to the TextMeshProUGUI component for button text.
    [SerializeField] private TextMeshProUGUI text;            // Reference to the TextMeshProUGUI component for header text.
    [SerializeField] private Sprite playSprite;               // Sprite for the play button state.
    [SerializeField] private Sprite lockedSprite;             // Sprite for the locked button state.
    private bool disablePlayButton;                          // Flag to disable play button functionality.
    private IMemoryPool memoryPool;
    private const string playButtonString = "PLAY";          // Text for the play button.
    private const string lockedButtonString = "LOCKED";      // Text for the locked button.

    #region Injection
    private ScreenController screenController;
    private SignalBus signalBus;

    [Inject]
    public void Construct(ScreenController screenController,SignalBus signalBus)
    {
        this.screenController = screenController;
        this.signalBus = signalBus;
    }
    #endregion

    private void OnEnable()
    {
        signalBus.Subscribe<ScreenStateChangedSignal>(OnScreenStateChangedSignal);
    }

    // Remove all listeners when the button is disabled.
    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();

        signalBus.Unsubscribe<ScreenStateChangedSignal>(OnScreenStateChangedSignal);
    }
    private void OnScreenStateChangedSignal(ScreenStateChangedSignal signal)
    {
        if (signal.CurrentScreenState == ScreenState.MainMenu)
        {
            Despawn();
        }
    }

    // Initialize the LevelButton.
    public void Init(Action onClick, int LevelID, bool isInteractable, LevelController.LevelData info)
    {
        string headerText = $"LEVEL {LevelID + 1}";
        text.text = headerText;

        if (isInteractable && !disablePlayButton)
        {
            // Add a listener to the button's onClick event.
            button.onClick.AddListener(() => onClick?.Invoke());
            SetButtonProps(playSprite, playButtonString);
        }
        else
        {
            SetButtonProps(lockedSprite, lockedButtonString);
        }

    }

    // Set the properties of the button.
    private void SetButtonProps(Sprite sprite, string text)
    {
        button.image.sprite = sprite;
        buttonText.text = text;
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }
    public void Clicked()
    {
        screenController.ChangeState(ScreenState.Game);
    }
    public void OnSpawned(IMemoryPool pool)
    {
        memoryPool = pool;
    }
    public void OnDespawned()
    {
        Dispose();
    }
    public void Despawn()
    {
        memoryPool.Despawn(this);
    }
    public void Dispose()
    {
       
    }
}
