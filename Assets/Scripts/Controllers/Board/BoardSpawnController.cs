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

    #region Injection
    private BoardController boardController;
    private Block.Factory blockFactory;
    private LevelController levelController;
    private LevelSceneReferences levelSceneReferences;

    [Inject]
    public void Construct(LevelSceneReferences references, BoardController boardController, Block.Factory blockFactory, LevelController levelController)
    {
        // Initialize references and settings
        this.levelSceneReferences = references; 
        this.boardController = boardController;
        this.blockFactory = blockFactory;
        this.levelController = levelController;
    }
    #endregion

    public void Initialize()
    {
        references = levelSceneReferences.BoardSpawnControllerReferences;
        SpawnRandomBoardAysnc();
    }
    public void Dispose()
    {
        Board = null;
    }

    // Spawn a random game board async func
    private async void SpawnRandomBoardAysnc()
    {
        await SpawnRandomBoard();
    }
    // Spawn a random game board
    private async UniTask SpawnRandomBoard()
    {
        this.settings = levelController.GetCurrentLevel().SettingsInfo.BoardSpawnControllerSettings;
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
        while (BlockCheckMatch.IsAnyMatchExistsInBoard(Board))
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
        if (references.BackgroundBoardSprite == null) Debug.LogError("null");

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
                Block block = blockFactory.Create();
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
            IBlockEntityTypeDefinition randomEntityType = blockEntityTypeDefinitions[Random.Range(0, blockEntityTypeDefinitions.Length)];
         
            Block block = blockFactory.Create();
         
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
