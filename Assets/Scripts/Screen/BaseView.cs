using System;
using UnityEngine;
using Zenject;

public abstract class BaseView : MonoBehaviour, IInitializable, IDisposable
{

    [Inject]
    private void Construct()
    {
       
    }

    public abstract void Initialize();

    public virtual void Dispose()
    {

    }

    public void DestroyView(float delay = 0)
    {
        Dispose();
        Destroy(gameObject, delay);
    }

}