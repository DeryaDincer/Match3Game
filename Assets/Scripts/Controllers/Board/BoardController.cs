using System.Collections.Generic;
using Zenject;
using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

// Controller responsible for managing the game board and block interactions.
public class BoardController : IInitializable, IObserver, IDisposable
{
    private Board Board;
    private bool explosionState;
    private bool gameEndState;
    private List<int> explosionBlocks = new List<int>();

    #region Injection
    private BoardSpawnController boardSpawnController;
    private BlockAnimationController blockAnimationController;
    private SignalBus signalBus;
    private ScreenController screenController;

    // Inject the associated board into the controller.
    public void BoardInject(Board board)
    {
        Board = board;
    }

    [Inject]
    public void Construct(BoardSpawnController boardSpawnController, BlockAnimationController blockAnimationController, SignalBus signalBus, ScreenController screenController)
    {
        this.boardSpawnController = boardSpawnController;
        this.blockAnimationController = blockAnimationController;
        this.signalBus = signalBus;
        this.screenController = screenController;
    }
    #endregion

    public void Initialize()
    {
        ObserverManager.Register<SwipeMessage, BoardController>(Message);
        signalBus.Subscribe<GameEndSignal>(OnGameEndSignal);
        signalBus.Subscribe<RequestBackToMainMenuSignal>(OnRequestBackToMainMenuSignal);
        gameEndState = false;
    }

    public void Dispose()
    {
        ObserverManager.UnRegister<SwipeMessage, BoardController>(Message);
        signalBus.Unsubscribe<GameEndSignal>(OnGameEndSignal);
        signalBus.Unsubscribe<RequestBackToMainMenuSignal>(OnRequestBackToMainMenuSignal);
    }

    // Handle the swipe message from the player.
    public void Message(SwipeMessage msg)
    {
        int index1 = msg.ClickBlock.BlockID;
        int index2 = msg.Direction[0] + (msg.Direction[1] * Board.Width);
        index2 += index1;
        if (Board == null) return;
        // Check if the move is valid and process it.
        bool isValid = BlockCheckMatch.IsValidAdjacentMove(Board, index1, index2);

        if (isValid && !explosionState && !gameEndState)
        {
            explosionState = true;
            // Start the block swapping and matching process.
            InitialController(index1, index2);
        }
    }

    // Initialize the block swapping and matching process.
    private async void InitialController(int index1, int index2)
    {
        await SwapBlocksInBoard(index1, index2);
        explosionBlocks = BlockCheckMatch.GetMatchingBlocks(Board.ActiveBlocks, Board.Width, Board.Height);
        if (explosionBlocks.Count == 0)
        {
            await SwapBlocksInBoard(index1, index2);
        }
        else
        {
            signalBus.Fire(new MoveMadeSignal());
            await InitialControllerAsync();

            while (true)
            {
                explosionBlocks = BlockCheckMatch.GetMatchingBlocks(Board.ActiveBlocks, Board.Width, Board.Height);
                if (explosionBlocks.Count != 0)
                {
                    await InitialControllerAsync();
                }
                else
                {
                    break;
                }
            }

        }
        explosionState = false;
    }

    // Asynchronously execute the entire block processing sequence.
    public async UniTask InitialControllerAsync()
    {
        await CheckExplosion();
        await ShiftBlocks();
        await SpawnNullBlocks();
    }

    // Swap two blocks on the board.
    private async UniTask SwapBlocksInBoard(int index1, int index2)
    {
        List<Block> swapBlocks = BlockCheckMatch.SwapBlocksInBoard(Board, index1, index2);
        boardSpawnController.SetAllBlockId(setBlockPosition: false);
        blockAnimationController.SetSwapAnimation(swapBlocks[0], setOrderInLayer: true);
        await blockAnimationController.SetSwapAnimation(swapBlocks[1]);
        await UniTask.Yield();
    }

    // Check and handle block explosions.
    private async UniTask CheckExplosion()
    {
        List<UniTask> taskList = new List<UniTask>();

        for (int i = 0; i < explosionBlocks.Count; i++)
            taskList.Add(blockAnimationController.SetDespawnAnimation(Board.ActiveBlocks[explosionBlocks[i]]));

        await UniTask.WhenAll(taskList);
        await UniTask.Yield();

        for (int i = 0; i < explosionBlocks.Count; i++)
        {
            Board.ActiveBlocks[explosionBlocks[i]].OnEntityDestroy();
            Board.ActiveBlocks[explosionBlocks[i]].Despawn();
            Board.ActiveBlocks[explosionBlocks[i]] = null;
        }

        await UniTask.Yield();
    }

    // Shift the remaining blocks after explosions.
    private async UniTask ShiftBlocks()
    {
        List<Block> shiftBlocks = BlockCheckMatch.ShiftBlocks(Board, Board.Width);
        boardSpawnController.SetAllBlockId(setBlockPosition: false);
        await SetBlocksMoveAnimation(shiftBlocks, 0);
    }

    // Spawn new blocks to fill empty spaces after shifts.
    private async UniTask SpawnNullBlocks()
    {
        List<Block> spawnBlocks = boardSpawnController.SpawnNullBlocks();
        spawnBlocks.Reverse();

        for (int i = 0; i < spawnBlocks.Count; i++)
            spawnBlocks[i].SetActiveRenderer(false);

        await SetBlocksMoveAnimation(spawnBlocks, 150, blockSetStartPosition: true);
    }

    // Set the move animation for blocks.
    private async UniTask SetBlocksMoveAnimation(List<Block> blocks, int delay, bool blockSetStartPosition = false)
    {
        List<UniTask> taskList = new List<UniTask>();

        for (int i = 0; i < blocks.Count; i++)
        {
            Block block = blocks[i];
            if (!block) continue;

            block.SetActiveRenderer(true);
            taskList.Add(blockAnimationController.SetTargetTransform(block, blockSetStartPosition));
            await UniTask.Delay(delay);
        }
        await UniTask.WhenAll(taskList);
    }

    // Handle the game end signal.
    private void OnGameEndSignal(GameEndSignal signal)
    {
        gameEndState = true;
    }

    // Handle the request to return to the main menu.
    public void OnRequestBackToMainMenuSignal(RequestBackToMainMenuSignal signal)
    {
        CheckExplosionState();
    }

    private async void CheckExplosionState()
    {
        while (explosionState) await UniTask.Yield();
        screenController.ChangeState(ScreenState.MainMenu);
    }
}
