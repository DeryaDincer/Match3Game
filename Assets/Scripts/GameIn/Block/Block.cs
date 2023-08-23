using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Block : BlockBase, IPoolable
{
     [Inject] private BlockGoalController blockGoalController;

    private void OnEnable()
    {
         OnEntityDestroyed.AddListener(blockGoalController.OnEntityDestroyed);
    }

    private void OnDisable()
    {
         OnEntityDestroyed.RemoveListener(blockGoalController.OnEntityDestroyed);
    }

    public void OnSpawned() { }

    public void OnDespawned()
    {
        OnEntityDestroy();
    }
}
