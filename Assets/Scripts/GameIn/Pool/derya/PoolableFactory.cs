using UnityEngine;
using Zenject;

public class PoolableFactory<T> : PlaceholderFactory<T> where T : Component, IPoolable
{
    public override T Create()
    {
        T instance = base.Create();
        instance.OnSpawned();
        return instance;
    }
}
