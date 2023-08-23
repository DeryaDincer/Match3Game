using System;
using UnityEngine;
using Zenject;

// A generic memory pool for components implementing IPoolable.
public class GenericMemoryPool<T> : MonoMemoryPool<T>
    where T : Component, IPoolable
{
    // This method is called when a new instance of 'item' is created.
    protected override void OnCreated(T item)
    {
        // Currently empty; you can add custom logic if needed when an item is created.
    }

    // This method is called when an instance of 'item' is taken from the memory pool.
    protected override void OnSpawned(T item)
    {
        item.OnSpawned();  // Call the OnSpawned method of the IPoolable component.
    }

    // This method is called when an instance of 'item' is returned to the memory pool.
    protected override void OnDespawned(T item)
    {
        item.OnDespawned();  // Call the OnDespawned method of the IPoolable component.
    }
}
