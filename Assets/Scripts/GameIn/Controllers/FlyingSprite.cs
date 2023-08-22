using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSprite : PoolObject
{
    public override void OnCreated()
    {
        OnDeactivate();
    }
    public override void OnDeactivate()
    {
        transform.SetParent(null);
        gameObject.SetActive(false);
    }
   
    public override void OnSpawn()
    {
        gameObject.SetActive(true);
    }
}
