using UnityEngine;
using System.Collections.Generic;
using Zenject;
using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Linq;

public class BoardController : IObserver, IDisposable
{
    private BoardControllerSettings settings;
    private BoardControllerReferences references;
    private BoardSpawnController boardSpawnController;
    private BlockAnimationController blockAnimationController;
    private Board Board;
    private bool explosionState;
    private List<int> popBlocks = new List<int>();

    public void BoardInject(Board board)
    {
        Board = board;
    }

    [Inject]
    public void Construct(LevelSettings settings, LevelSceneReferences references, BoardSpawnController boardSpawnController, BlockAnimationController blockAnimationController)
    {
        this.settings = settings.BoardControllerSettings;
        this.references = references.BoardControllerReferences;
        this.boardSpawnController = boardSpawnController;
        this.blockAnimationController = blockAnimationController;
    }
    public void AddRegister()
    {
        ObserverManager.Register<SwipeMessage, BoardController>(Message);
    }
    public void Dispose()
    {
        ObserverManager.UnRegister<SwipeMessage, BoardController>(Message);
    }
    public void Message(SwipeMessage msg)
    {
        int index1 = msg.ClickBlock.BlockID;
        int index2 = msg.Direction[0] + (msg.Direction[1]  * Board.Width);
        index2 += index1;

        bool isValid = BlockCheckMatch.IsValidAdjacentMove(Board, index1, index2);
        if (isValid && !explosionState)
        {
            explosionState = true;
            InitialController(index1, index2);
        }
    }
    private async void InitialController(int index1,int index2)
    {
        await SwapBlocksInBoard(index1, index2);
        popBlocks = BlockCheckMatch.GetMatchingBlocks(Board.ActiveBlocks, Board.Width);
        if(popBlocks.Count == 0)
        {
            await SwapBlocksInBoard(index2, index1);
        }
        else
        {
            await InitialControllerAsync();
        }
      
        explosionState = false;

    }
    public async UniTask InitialControllerAsync()
    {
        using (CancellationTokenSource cts = new CancellationTokenSource())
        {
            var token = cts.Token;
            UniTask Task1 = CheckExplosion(cts);
            UniTask Task2 = ShiftBlocks(cts);
            UniTask Task3 = SpawnNullBlocks(cts);

            await Task1;
            token.ThrowIfCancellationRequested();

            await UniTask.WhenAll(ShiftBlocks(cts), SpawnNullBlocks(cts));

            if (Task3.Status == UniTaskStatus.Succeeded)
            {
                await InitialControllerAsync();
            }

        }
    }

    private async UniTask SwapBlocksInBoard(int index1, int index2)
    {
        List<Block> swapBlocks = BlockCheckMatch.SwapBlocksInBoard(Board, index1, index2);
        boardSpawnController.SetAllBlockId(setBlockPosition: false);
        blockAnimationController.SetSwapAnimation(swapBlocks[0], setOrderInLayer:true);
        await blockAnimationController.SetSwapAnimation(swapBlocks[1]);
        await UniTask.Yield();
    }
  
    private async UniTask CheckExplosion(CancellationTokenSource cts)
    {
        popBlocks = BlockCheckMatch.GetMatchingBlocks(Board.ActiveBlocks, Board.Width);

        if (popBlocks.Count == 0)
        {
            explosionState = false;
            cts.Cancel();
        }
        List<UniTask> taskList = new List<UniTask>();
      
        for (int i = 0; i < popBlocks.Count; i++)
             taskList.Add(blockAnimationController.SetDespawnAnimation(Board.ActiveBlocks[popBlocks[i]]));
      
        await UniTask.WhenAll(taskList);

        for (int i = 0; i < popBlocks.Count; i++)
        {
            Board.ActiveBlocks[popBlocks[i]].OnDeactivate();
            Board.ActiveBlocks[popBlocks[i]] = null;
        }

        await UniTask.Yield();
    }

    private async UniTask ShiftBlocks(CancellationTokenSource cts)
    {
        List<Block> shiftBlocks = BlockCheckMatch.ShiftBlocks(Board, Board.Width);
        boardSpawnController.SetAllBlockId(setBlockPosition:false);
        await SetBlocksMoveAnimation(shiftBlocks,0);
    }

    private async UniTask SpawnNullBlocks(CancellationTokenSource cts)
    {
        List<Block> spawnBlocks = boardSpawnController.SpawnNullBlocks();
        spawnBlocks.Reverse();

        for (int i = 0; i < spawnBlocks.Count; i++)
            spawnBlocks[i].SetActiveRenderer(false);
       
        await SetBlocksMoveAnimation(spawnBlocks, 100,blockSetStartPosition : true);
    }
    
    private async UniTask SetBlocksMoveAnimation(List<Block> blocks,int delay, bool blockSetStartPosition = false)
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
   
}
