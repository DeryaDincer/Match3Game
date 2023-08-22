using System;
using Zenject;
using UnityEngine;
using Cysharp.Threading.Tasks;


public class GameInInstaller : MonoInstaller
{
    [Group] [SerializeField] private LevelSceneReferences LevelReferences;
    public UniTaskCompletionSource<bool> InitialSignal = new();
    public Block blockPrefab;
    public Transform parent;
    private void Awake()
    {
        InitialSignal.TrySetResult(true);
    }
    public override void InstallBindings()
    {
        //References
        Container.BindInstance(LevelReferences).AsSingle();
        //Controllers
        Container.Bind<BlockGoalController>().AsSingle();
        Container.Bind<BoardController>().AsSingle();
        Container.Bind<BoardSpawnController>().AsSingle();
        Container.Bind<BlockMoveController>().AsSingle();
        Container.Bind<BlockAnimationController>().AsSingle();
        Container.Bind<GameInCameraController>().AsSingle();


        Container.BindMemoryPool<Block, GenericMemoryPool<Block>>()
.WithInitialSize(10) 
.FromComponentInNewPrefab(blockPrefab)
.UnderTransform(parent);

        Container.Inject(this);

    }

}