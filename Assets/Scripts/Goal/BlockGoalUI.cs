using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public partial class BlockGoalUI : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
{
    public class Factory : PlaceholderFactory<BlockGoalUI> { }

    public class Pool : MonoPoolableMemoryPool<IMemoryPool, BlockGoalUI> { }
}

public partial class BlockGoalUI : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
{
    [SerializeField] private ParticleSystem goalParticle;
    [SerializeField] private TMP_Text goalAmountLeftText;
    [SerializeField] private Image goalImage;
    [SerializeField] private Image goalCompletedImage;
    private IMemoryPool memoryPool;
    public Goal Goal { get; private set; }
    private SignalBus signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }

    private void OnEnable()
    {
        signalBus.Subscribe<ScreenStateChangedSignal>(OnScreenStateChangedSignal);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<ScreenStateChangedSignal>(OnScreenStateChangedSignal);
    }

    // Callback for the screen state change signal.
    private void OnScreenStateChangedSignal(ScreenStateChangedSignal signal)
    {
        if (signal.CurrentScreenState == ScreenState.MainMenu)
        {
            Despawn();
        }
    }

    // Set up the UI for a specific goal.
    public void SetupGoalUI(Goal goal)
    {
        Goal = goal;

        Sprite entitySprite = goal.entityType.EntitySpriteAtlas.GetSprite(goal.entityType.DefaultEntitySpriteName);
        goalImage.sprite = entitySprite;
        SetGoalAmount(goal.GoalLeft, false);
    }

    // Set the goal amount and optionally play particles.
    public void SetGoalAmount(int goalAmount, bool playParticles = true)
    {
        if (playParticles) goalParticle.Play();

        if (goalAmount == 0)
        {
            goalCompletedImage.enabled = true;
            goalAmountLeftText.text = "";
        }
        else
        {
            goalCompletedImage.enabled = false;
            goalAmountLeftText.text = goalAmount.ToString();
        }
    }

    // Callback when the object is spawned.
    public void OnSpawned(IMemoryPool pool)
    {
        memoryPool = pool;
    }

    // Callback when the object is despawned.
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
