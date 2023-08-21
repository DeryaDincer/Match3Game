using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class ObserverManager
{
    private static List<Type> Observers = new List<Type>();
    public static void Register<T, U>(Action<T> add) where T : SubjectData where U : IObserver
    {
        Type observer = typeof(U);
        Observers.Add(observer);
        ObserverService<T, U>.Add(add, default(U));
    }

    public static void UnRegister<T, U>(Action<T> add) where T : SubjectData where U : IObserver
    {
        ObserverService<T, U>.Remove(add, default(U));
    }

    public static void Send<T, U>(T handle) where T : SubjectData where U : IObserver
    {
        ObserverService<T, U>.Handle(handle);
    }

    public static async UniTask SendAsync<T, U>(T handle) where T : SubjectData where U : IObserver
    {
        await ObserverService<T, U>.HandleAsync(handle);
    }

    public static void SendGlobal<T>(T handle) where T : SubjectData
    {
        foreach (var observer in Observers)
        {
            var observerServiceType = typeof(ObserverService<,>).MakeGenericType(typeof(T), observer);
            var handleMethod = observerServiceType.GetMethod("Handle");
            handleMethod.Invoke(null, new object[] { handle });
        }
    }


    public partial class ObserverService<T, U> where T : SubjectData where U : IObserver
    {
        public class ObserverData
        {
            public Action<T> Message { get; set; }
            public U Observer { get; set; }

            public Type TargetType { get; set; }

            public ObserverData() : this(null, default(U), null)
            {

            }

            public ObserverData(Action<T> message, U observer, Type targetType)
            {
                this.Message = message;
                this.Observer = observer;
                this.TargetType = targetType;
            }
        }
    }


    public partial class ObserverService<T, U> where T : SubjectData where U : IObserver
    {
        public List<ObserverData> lsMessages;


        private static ObserverService<T, U> Instance;
        public static ObserverService<T, U> instance
        {
            get => Instance = Instance ?? new ObserverService<T, U>();

        }

        private ObserverService()
        {
            lsMessages = new();

        }

        public static void Add(Action<T> message, U observer)
        {
            ObserverData observerData = new(message, observer, typeof(U));
            instance.lsMessages.Add(observerData);
        }

        public static void Remove(Action<T> message, U observer)
        {
            ObserverData observerData = new(message, observer, typeof(U));
            instance.lsMessages.Remove(observerData);
        }

        public static void Handle(T Data)
        {
            if (instance.lsMessages == null)
            {
                return;
            }

            for (int i = instance.lsMessages.Count - 1; i >= 0; i--)
            {
                ObserverData observerData = instance.lsMessages[i];

                if (observerData.Observer == null || observerData.Observer.Equals(default(U)))
                {
                    observerData.Message(Data);
                }
            }
        }

        public static async UniTask HandleAsync(T Data)
        {
            if (instance.lsMessages == null)
            {
                return;
            }

            for (int i = instance.lsMessages.Count - 1; i >= 0; i--)
            {
                ObserverData observerData = instance.lsMessages[i];

                if (observerData.Observer == null || observerData.Observer.Equals(default(U)))
                {
                    observerData.Message(Data);
                }
            }

            await UniTask.CompletedTask;
        }



    }

}




