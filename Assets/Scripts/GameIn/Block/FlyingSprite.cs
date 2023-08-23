using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSprite : MonoBehaviour, IPoolable
{
    public void OnSpawned() { }
    public void OnDespawned()
    {
        gameObject.SetActive(false);
    }
}
