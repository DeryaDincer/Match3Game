using Zenject;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GameInInstaller : MonoInstaller
{
    [Group] public LevelSceneReferences LevelReferences;
    public UniTaskCompletionSource<bool> InitialSignal = new();

    #region Injection
    private GamePoolSettings gamePoolSettings;

    [Inject]
    public void Construct(GamePoolSettings gamePoolSettings)
    {
        this.gamePoolSettings = gamePoolSettings;
    }
    #endregion

    // Called when the GameObject this script is attached to is awakened.
    private void Awake()
    {
        InitialSignal.TrySetResult(true);
    }

    // Override method for installing Zenject bindings.
    public override void InstallBindings()
    {
        Container.Bind<SaveLoadController>().AsSingle();

        Container.BindFactory<Object, BaseScreen, BaseScreen.Factory>().FromFactory<PrefabFactory<BaseScreen>>();
        Container.BindInterfacesAndSelfTo<ApplicationController>().AsSingle();

        //Controllers
        Container.Bind<ScreenController>().AsSingle();

        //References

        //Controllers
        Container.BindInstance(LevelReferences).AsSingle();
        Container.Bind<LevelController>().AsSingle();
        Container.BindInterfacesAndSelfTo<MainMenuController>().AsSingle();

        //Signal
        GameSignalsInstaller.Install(Container);

        //Controllers
        Container.Bind<BoardSpawnController>().AsSingle();
        Container.Bind<BoardController>().AsSingle();
        Container.Bind<GameInCameraController>().AsSingle();
        Container.Bind<BlockGoalController>().AsSingle();
        Container.Bind<BlockMoveController>().AsSingle();
        Container.Bind<BlockAnimationController>().AsSingle();
        Container.Bind<GameInUIEffectController>().AsSingle();
        Container.Bind<GameEndCanvasController>().AsSingle();

        //Install Pool
        InstallCreatorGridItemView();
    }

    // Install memory pools for various types.
    private void InstallCreatorGridItemView()
    {
        Container.BindFactory<LevelButton, LevelButton.Factory>()
            .FromPoolableMemoryPool<LevelButton, LevelButton.Pool>(poolBinder => poolBinder
                .WithInitialSize(gamePoolSettings.LevelButtonPoolSize)
                .FromComponentInNewPrefab(gamePoolSettings.LevelButtonPrefab)
                .UnderTransformGroup(gamePoolSettings.LevelButtonPoolName));

        Container.BindFactory<Block, Block.Factory>()
           .FromPoolableMemoryPool<Block, Block.Pool>(poolBinder => poolBinder
               .WithInitialSize(gamePoolSettings.BlockPoolSize)
               .FromComponentInNewPrefab(gamePoolSettings.BlockPrefab)
               .UnderTransformGroup(gamePoolSettings.BlockPoolName));

        Container.BindFactory<FlyingSprite, FlyingSprite.Factory>()
      .FromPoolableMemoryPool<FlyingSprite, FlyingSprite.Pool>(poolBinder => poolBinder
          .WithInitialSize(gamePoolSettings.FlyingSpritePoolSize)
          .FromComponentInNewPrefab(gamePoolSettings.FlyingSpritePrefab)
          .UnderTransformGroup(gamePoolSettings.FlyingSpritePoolName));

        Container.BindFactory<BlockGoalUI, BlockGoalUI.Factory>()
  .FromPoolableMemoryPool<BlockGoalUI, BlockGoalUI.Pool>(poolBinder => poolBinder
      .WithInitialSize(gamePoolSettings.BlockGoalUIPoolSize)
      .FromComponentInNewPrefab(gamePoolSettings.BlockGoalUIPrefab)
      .UnderTransformGroup(gamePoolSettings.BlockGoalUIPoolName));
    }
}