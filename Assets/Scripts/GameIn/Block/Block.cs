using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Block : BlockBase, IPoolable
{
    public void OnSpawned()
    {
        gameObject.SetActive(true);
       // OnDespawned();
    }

    public void OnDespawned()
    {
        //gameObject.SetActive(false);
    }
}
