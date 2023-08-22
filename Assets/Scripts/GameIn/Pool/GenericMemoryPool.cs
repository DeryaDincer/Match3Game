using System;
using UnityEngine;
using Zenject;

public class GenericMemoryPool<T> : MonoMemoryPool<T>
    where T : Component, IPoolable
{
    protected override void OnCreated(T item)
    {
        // Yeni bir Block yarat�ld���nda burada yap�lacak i�lemler
    }

    protected override void OnSpawned(T item)
    {
        item.OnSpawned();
        // Block'un spawn edildi�inde burada yap�lacak i�lemler
    }

    protected override void OnDespawned(T item)
    {
        item.OnDespawned();
        // Block'un despawn edildi�inde burada yap�lacak i�lemler
    }
}
