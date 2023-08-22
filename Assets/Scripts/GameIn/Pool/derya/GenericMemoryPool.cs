using System;
using UnityEngine;
using Zenject;

public class GenericMemoryPool<T> : MonoMemoryPool<T>
    where T : Component, IPoolable
{
    protected override void OnCreated(T item)
    {
        // Yeni bir Block yaratıldığında burada yapılacak işlemler
    }

    protected override void OnSpawned(T item)
    {
        item.OnSpawned();
        // Block'un spawn edildiğinde burada yapılacak işlemler
    }

    protected override void OnDespawned(T item)
    {
        item.OnDespawned();
        // Block'un despawn edildiğinde burada yapılacak işlemler
    }
}
