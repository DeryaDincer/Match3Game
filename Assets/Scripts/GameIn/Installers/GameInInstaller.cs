using System;
using Zenject;
using UnityEngine;
using Cysharp.Threading.Tasks;


public class GameInInstaller : MonoInstaller
{
    [Group] [SerializeField] private LevelSceneReferences LevelReferences;
    public UniTaskCompletionSource<bool> InitialSignal = new();

    #region values
    [SerializeField] private Block blockPrefab;
    [SerializeField] private BlockGoalUI blockGoalUI;
    [SerializeField] private FlyingSprite flyingSprite;
    [SerializeField] private Transform gridGoalUIParent;
    [SerializeField] private Transform flyingSpriteParent;
    #endregion

    private void Awake()
    {
        InitialSignal.TrySetResult(true);
    }
    public override void InstallBindings()
    {
        //References
        Container.BindInstance(LevelReferences).AsSingle();

        //Signal
        GameSignalsInstaller.Install(Container);

        //Controllers
        Container.BindInterfacesAndSelfTo<BoardSpawnController>().AsSingle();
        Container.BindInterfacesAndSelfTo<BoardController>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameInCameraController>().AsSingle();
        Container.BindInterfacesAndSelfTo<BlockGoalController>().AsSingle();
        Container.BindInterfacesAndSelfTo<BlockMoveController>().AsSingle();
        Container.BindInterfacesAndSelfTo<BlockAnimationController>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameInUIEffectController>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameEndCanvasController>().AsSingle();

        //Pooling
        Container.BindMemoryPool<Block, GenericMemoryPool<Block>>().FromComponentInNewPrefab(blockPrefab);
        Container.BindMemoryPool<BlockGoalUI, GenericMemoryPool<BlockGoalUI>>().FromComponentInNewPrefab(blockGoalUI).UnderTransform(gridGoalUIParent);
        Container.BindMemoryPool<FlyingSprite, GenericMemoryPool<FlyingSprite>>().FromComponentInNewPrefab(flyingSprite).UnderTransform(flyingSpriteParent);
    }
}