using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BoardSpawnController
{
    public Board Board;
    private BoardSpawnControllerSettings settings;
    private BoardSpawnControllerReferences references;
    private IBlockEntityTypeDefinition[] blockEntityTypeDefinitions;
    private GridStartLayout startLayout;
    private BoardController boardController;
    private GenericMemoryPool<Block> memoryPool;
    [Inject]
    public void Construct(LevelSceneReferences references, BoardController boardController, GenericMemoryPool<Block> memoryPool)
    {
        // Initialize references and settings
        this.references = references.BoardSpawnControllerReferences;
        this.boardController = boardController;
        this.memoryPool = memoryPool;
    }

    // Spawn a random game board
    public async UniTask SpawnRandomBoard()
    {
        this.settings = LevelController.GetCurrentLevel().SettingsInfo.BoardSpawnControllerSettings;
        this.blockEntityTypeDefinitions = this.settings.EntityTypes;
        this.startLayout = this.settings.gridStartLayout;

        int width = (int)settings.Width;
        int height = (int)settings.Height;
        List<int> lockedIds = new List<int>();

        // Create a new game board
        Board = new Board(width, height, new Block[width * height], lockedIds);
        boardController.BoardInject(Board);
        CreateRandomBoard(Board);

        // Check for initial matches and shuffle if needed
        if (BlockCheckMatch.IsAnyMatchExistsInBoard(Board))
        {
            BlockCheckMatch.Shuffle(Board);
        }

        // Set IDs for all blocks and find locked blocks
        SetAllBlockId();
        FindLockedBlocks();
        SetBoardBackround();
        await UniTask.Yield();
    }

    // Find locked blocks and populate LockedIds list
    private void FindLockedBlocks()
    {
        for (int i = 0; i < Board.ActiveBlocks.Length; i++)
        {
            Block block = Board.ActiveBlocks[i];

            if (block.EntityType is LockedBlockTypeDefinition)
                SetLockedId(block);
        }
    }

    // Set Background Board
    private void SetBoardBackround()
    {
        Vector2 backgroundScale = new Vector3(Board.Width + references.BackgroundBoardScaleOffset, Board.Height + references.BackgroundBoardScaleOffset, 1);
        references.BackgroundBoardSprite.transform.localScale = backgroundScale;
    }
    // Set BlockID and target position for all blocks
    public void SetAllBlockId(bool setBlockPosition = true)
    {
        for (int i = 0; i < Board.ActiveBlocks.Length; i++)
        {
            if (Board.ActiveBlocks[i])
            {
                Board.ActiveBlocks[i].SetBlockBaseId(i);
                Board.ActiveBlocks[i].SetTargetPositionToID(Board);
                if (setBlockPosition) Board.ActiveBlocks[i].SetPosition();
            }
        }
    }

    // Spawn new blocks in the place of null blocks
    public List<Block> SpawnNullBlocks()
    {
        List<Block> spawnBlock = new List<Block>();

        for (int i = 0; i < Board.ActiveBlocks.Length; i++)
        {
            if (Board.ActiveBlocks[i] == null)
            {
                if (Board.LockedIds.Contains(i))
                    continue;

                // Choose a random entity type for the block
                IBlockEntityTypeDefinition randomEntityType = blockEntityTypeDefinitions[Random.Range(0, blockEntityTypeDefinitions.Length)];

                // Create and setup a new block
                Block block = memoryPool.Spawn();
               // Block block = PoolManager.Instance.GetObject<Block>();
                block.SetupEntity(randomEntityType);
                Board.ActiveBlocks[i] = block;
                spawnBlock.Add(block);
            }
        }

        // Set IDs for the newly spawned blocks
        SetAllBlockId(setBlockPosition: false);
        return spawnBlock;
    }

    // Create a random game board layout
    private Board CreateRandomBoard(Board board)
    {
        for (int i = 0; i < board.Width * board.Height; i++)
        {
            IBlockEntityTypeDefinition randomEntityType = null;
            randomEntityType = blockEntityTypeDefinitions[Random.Range(0, blockEntityTypeDefinitions.Length)];
            //Block block = PoolManager.Instance.GetObject<Block>();
         
           // Block block = blockFactory.Create();
            Block block = memoryPool.Spawn();
            // If the block layout specifies a block, use that type and set it as situated
            if (startLayout.BlockDatas.Blocks[i] != null)
            {
                randomEntityType = startLayout.BlockDatas.Blocks[i];
                block.SetSituated();
            }

            // Setup the entity for the block
            block.SetupEntity(randomEntityType);
            board.ActiveBlocks[i] = block;
        }

        return board;
    }

    // Set LockedIds based on the lockedBlock's position
    private void SetLockedId(Block lockedBlock)
    {
        Board.LockedIds.AddRange(BlockCheckMatch.GetBlocksBelowIds(Board, lockedBlock.BlockID));
    }
}
