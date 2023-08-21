using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    // �lgili t�rler i�in PoolObject ve IGenericPoolInterfaces aray�zlerini saklamak i�in bir dictionary.
    private Dictionary<Type, Queue<PoolObject>> poolQueues = new Dictionary<Type, Queue<PoolObject>>();
    private Dictionary<Type, Type> poolObjectToInterfaceMap = new Dictionary<Type, Type>();

    // Pool olu�turma i�lemi i�in kullan�lacak yap�.
    [System.Serializable]
    internal struct Pool
    {
        internal Queue<PoolObject> pooledObjects;
        [SerializeField] internal PoolObject objectPrefab;
        [SerializeField] internal int poolSize;
    }

    // Olu�turulacak pool listesi.
    [SerializeField] private Pool[] pools;
    public bool HasLoading = false;
    public UniTaskCompletionSource<bool> IsLoading = new UniTaskCompletionSource<bool>();

    private void Start()
    {
        CreatePools();
    }

    private void CreatePools()
    {
        poolQueues = new Dictionary<Type, Queue<PoolObject>>();
        poolObjectToInterfaceMap = new Dictionary<Type, Type>();

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i].pooledObjects = new Queue<PoolObject>();

            for (int j = 0; j < pools[i].poolSize; j++)
            {
                PoolObject poolObject = Instantiate(pools[i].objectPrefab, transform);
                poolObject.OnCreated();
                pools[i].pooledObjects.Enqueue(poolObject);


                // T�m aray�zleri al
                Type[] interfaces = poolObject.GetType().GetInterfaces();
                foreach (Type interfaceType in interfaces)
                {
                    // Aray�z bir IGenericPoolInterfaces ise i�leme devam et
                    if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IGenericPoolInterfaces<>))
                    {
                        Type entityType = interfaceType.GetGenericArguments()[0]; // T t�r�n� al

                        Debug.Log("T type===>>>>>" + "--------" + entityType);
                        if (!poolQueues.ContainsKey(entityType))
                        {
                            poolQueues[entityType] = new Queue<PoolObject>(); // Kuyru�u olu�tur
                        }

                        poolQueues[entityType].Enqueue(poolObject); // T t�r�ne g�re poolObject'i kuyru�a ekle

                        // PoolObject t�r�ne g�re hangi IGenericPoolInterfaces<T> aray�z� ile ili�kili oldu�unu tutan map'i g�ncelle
                        if (!poolObjectToInterfaceMap.ContainsKey(poolObject.GetType()))
                        {
                            poolObjectToInterfaceMap[poolObject.GetType()] = entityType;
                            Debug.Log("PoolObject type" + "=>>>>" + poolObject.GetType() + "------------------" + "InterfaceType" + "=>>>>>>>>>>>>" + entityType);
                        }
                    }
                }

                poolObject.gameObject.SetActive(false);
            }
            IsLoading.TrySetResult(true);
        }


        // Olu�turulan e�le�tirmeleri kontrol etmek i�in Debug.Log ekleyin
        foreach (var kvp in poolObjectToInterfaceMap)
        {
            Debug.Log(kvp.Key + " -> " + kvp.Value);
        }

        foreach (var kvp in poolObjectToInterfaceMap)
        {
            Debug.Log(kvp.Key + " -> " + kvp.Value);
        }



        HasLoading = true;
    }



    public T GetObject<T>() where T : PoolObject
    {
        for (int i = 0; i < pools.Length; i++)
        {
            if (typeof(T) == pools[i].objectPrefab.GetType())
            {
                if (pools[i].pooledObjects.Count == 0)
                {
                    PoolObject _po = Instantiate(pools[i].objectPrefab, transform);
                    _po.OnCreated();
                    EnqueueObject(_po);
                    return GetObject<T>();
                }
                else
                {

                    T t = pools[i].pooledObjects.Dequeue() as T;
                    t.OnSpawn();

                    return t;
                }
            }

        }
        return default;
    }


    // T t�r�ndeki PoolObject'leri �ekmek i�in kullan�lan metot.
    public T Resolve<T>() where T : PoolObject
    {
        Type objectType = typeof(T);

        if (poolQueues.ContainsKey(objectType) && poolQueues[objectType].Count > 0)
        {
            T poolObject = poolQueues[objectType].Dequeue() as T;
            poolObject?.OnSpawn();
            return poolObject;
        }

        Debug.LogWarning("Pool is empty for object with type: " + objectType);
        return null;
    }

    // IGenericPoolInterfaces<T> t�r�ndeki PoolObject'leri �ekmek i�in kullan�lan metot.


    public T ResolveWithInterface<T>() where T : class, IPoolObject
    {
        // PoolObject t�r�ne g�re hangi IGenericPoolInterfaces<T> aray�z� ile ili�kili oldu�unu alal�m
        if (poolObjectToInterfaceMap.TryGetValue(typeof(T), out Type interfaceType))
        {
            Debug.Log(typeof(T) + "***" + interfaceType);
            // DicPool'da interface tipi i�in bir kuyruk var m� kontrol ediyoruz.
            if (poolQueues.TryGetValue(interfaceType, out Queue<PoolObject> queue) && queue.Count > 0)
            {
                T poolObject = queue.Dequeue() as T;
                poolObject?.OnSpawn();
                return poolObject;
            }
            else
            {
                Debug.LogWarning("Pool is empty for object with interface type: " + interfaceType);
            }
        }
        else
        {
            // T tipine kar��l�k gelen aray�z bulunamad�
            Debug.LogWarning("No interface mapping found for type: " + typeof(T));
        }

        return null;
    }


    // T t�r�ndeki PoolObject'leri yeniden kuyru�a eklemek i�in kullan�lan metot.
    public void EnqueueObject<T>(T po) where T : PoolObject
    {
        Type objectType = typeof(T);

        if (!poolQueues.ContainsKey(objectType))
        {
            Debug.LogWarning("No pool for object with type: " + objectType);
            return;
        }

        bool isFound = false;

        // Kuyruktan elemanlar� ��kar ve liste olarak sakla
        List<PoolObject> pooledObjectsList = new List<PoolObject>();
        while (poolQueues[objectType].Count > 0)
        {
            PoolObject pooledObject = poolQueues[objectType].Dequeue();
            if (pooledObject == po)
            {
                isFound = true;
            }
            pooledObjectsList.Add(pooledObject);
        }

        // Eleman� ekle
        if (!isFound)
        {
            pooledObjectsList.Add(po);
        }

        // Listeyi kuyru�a geri ekle
        foreach (var pooledObject in pooledObjectsList)
        {
            poolQueues[objectType].Enqueue(pooledObject);
        }
    }
}