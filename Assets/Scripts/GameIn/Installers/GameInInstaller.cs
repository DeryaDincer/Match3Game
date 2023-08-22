using System;
using Zenject;
using UnityEngine;
using Cysharp.Threading.Tasks;


public class GameInInstaller : MonoInstaller
{
    [Group] [SerializeField] private LevelSceneReferences LevelReferences;
    public UniTaskCompletionSource<bool> InitialSignal = new();

    [SerializeField] private Block blockPrefab;
    [SerializeField] private GridGoalUI gridGoalUI;
    [SerializeField] private FlyingSprite flyingSprite;

    [SerializeField] private Transform gridGoalUIParent;
    [SerializeField] private Transform flyingSpriteParent;
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
.WithInitialSize(1) 
.FromComponentInNewPrefab(blockPrefab)
.UnderTransform(parent);


        Container.BindMemoryPool<GridGoalUI, GenericMemoryPool<GridGoalUI>>()
.WithInitialSize(1)
.FromComponentInNewPrefab(gridGoalUI)
.UnderTransform(gridGoalUIParent);


//        Container.BindMemoryPool<FlyingSprite, GenericMemoryPool<FlyingSprite>>()
//.WithInitialSize(1)
//.FromComponentInNewPrefab(flyingSprite)
//.UnderTransform(flyingSpriteParent);



        Container.Inject(this);

    }

}