using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSprite : MonoBehaviour, IPoolable
{
  
    public void OnSpawned()
    {
        gameObject.SetActive(true);
    }

    public void OnDespawned()
    {
        transform.SetParent(null);
        gameObject.SetActive(false);
    }
}
