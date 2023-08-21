using System;
using Zenject;
using UnityEngine;
using Cysharp.Threading.Tasks;


public class GameInInstaller : MonoInstaller
{

    [SerializeField] private LevelSettings LevelSettings;
    [Group] [SerializeField] private LevelSceneReferences LevelReferences;
    public UniTaskCompletionSource<bool> InitialSignal = new();

    private void Awake()
    {
        InitialSignal.TrySetResult(true);
    }
    public override void InstallBindings()
    {

        //Settings
        Container.BindInstance(LevelSettings).AsSingle();
        //References
        Container.BindInstance(LevelReferences).AsSingle();
        //Controllers

        Container.Bind<BoardController>().AsSingle();
        Container.Bind<BoardSpawnController>().AsSingle();
        Container.Bind<BlockGoalController>().AsSingle();
        Container.Bind<BlockMoveController>().AsSingle();
        Container.Bind<BlockAnimationController>().AsSingle();
        Container.Bind<GameInCameraController>().AsSingle();

        Container.Inject(this);


    }

}