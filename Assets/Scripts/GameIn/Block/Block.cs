using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : BlockBase
{
    public override void OnDeactivate()
    {
        gameObject.SetActive(false);
    }

    public override void OnSpawn()
    {
        gameObject.SetActive(true);
    }

    public override void OnCreated()
    {
        OnDeactivate();
    }
}
