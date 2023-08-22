using System;
using UnityEngine;
using Zenject;

public class GenericMemoryPool<T> : MonoMemoryPool<T>
    where T : Component, IPoolable
{
    protected override void OnCreated(T item)
    {
        // Yeni bir Block yaratýldýðýnda burada yapýlacak iþlemler
    }

    protected override void OnSpawned(T item)
    {
        item.OnSpawned();
        // Block'un spawn edildiðinde burada yapýlacak iþlemler
    }

    protected override void OnDespawned(T item)
    {
        item.OnDespawned();
        // Block'un despawn edildiðinde burada yapýlacak iþlemler
    }
}
