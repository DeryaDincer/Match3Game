using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public partial class FlyingSprite : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
{
    public class Factory : PlaceholderFactory<FlyingSprite> { }

    public class Pool : MonoPoolableMemoryPool<IMemoryPool, FlyingSprite> { }
}

public partial class FlyingSprite : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
{
    private IMemoryPool memoryPool;
   
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
