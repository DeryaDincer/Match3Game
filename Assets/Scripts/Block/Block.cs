using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public partial class Block : BlockBase, IPoolable<IMemoryPool>, IDisposable
{
    public class Factory : PlaceholderFactory<Block> { }

    public class Pool : MonoPoolableMemoryPool<IMemoryPool, Block> { }
}

public partial class Block : BlockBase,IPoolable<IMemoryPool>, IDisposable
{
    private BlockGoalController blockGoalController;
    private SignalBus signalBus;
    private IMemoryPool memoryPool;

    [Inject] 
    public void Construct(BlockGoalController blockGoalController, SignalBus signalBus)
    {
        this.blockGoalController = blockGoalController; 
        this.signalBus = signalBus; 
    }
    private void OnEnable()
    {
        OnEntityDestroyed.AddListener(blockGoalController.OnEntityDestroyed);
        signalBus.Subscribe<ScreenStateChangedSignal>(OnScreenStateChangedSignal);
    }
    private void OnDisable()
    {
         OnEntityDestroyed.RemoveListener(blockGoalController.OnEntityDestroyed);
        signalBus.Unsubscribe<ScreenStateChangedSignal>(OnScreenStateChangedSignal);
    }
    private void OnScreenStateChangedSignal(ScreenStateChangedSignal signal)
    {
        if (signal.CurrentScreenState == ScreenState.MainMenu)
        {
            Despawn();
        }
    }
    public void OnSpawned(IMemoryPool pool)
    {
        transform.localScale = Vector3.one;
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
