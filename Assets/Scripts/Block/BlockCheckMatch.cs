using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains methods to check for matches and perform operations on a game board.
/// </summary>
public class BlockCheckMatch
{
    /// <summary>
    /// Checks if a match exists after swapping two elements on the board.
    /// </summary>
    /// <param name="board">The game board.</param>
    /// <param name="sourceIndex">Index of the first element to swap.</param>
    /// <param name="targetIndex">Index of the second element to swap.</param>
    /// <returns>Returns true if a match exists after swapping; otherwise, false.</returns>
    public static bool MatchExists(Board board, int sourceIndex, int targetIndex)
    {
        // Swap the elements in the grid temporarily
        SwapElementsInGrid(board, sourceIndex, targetIndex);

        // Check if a match exists on the board after the swap
        bool matchExists = HasMatch(board);

        // Swap the elements back to revert the board to its original state
        SwapElementsInGrid(board, sourceIndex, targetIndex);

        return matchExists;
    }

    /// <summary>
    /// Swaps two elements in the game board.
    /// </summary>
    /// <param name="board">The game board.</param>
    /// <param name="index1">Index of the first element to swap.</param>
    /// <param name="index2">Index of the second element to swap.</param>
    private static void SwapElementsInGrid(Board board, int index1, int index2)
    {
        Block tempValue = board.ActiveBlocks[index1];
        board.ActiveBlocks[index1] = board.ActiveBlocks[index2];
        board.ActiveBlocks[index2] = tempValue;
    }

    /// <summary>
    /// Swaps two blocks in the game board and returns them.
    /// </summary>
    /// <param name="board">The game board.</param>
    /// <param name="index1">Index of the first block to swap.</param>
    /// <param name="index2">Index of the second block to swap.</param>
    /// <returns>List containing the swapped blocks.</returns>
    public static List<Block> SwapBlocksInBoard(Board board, int index1, int index2)
    {
        List<Block> swapBlocks = new List<Block>();

        Block tempValue = board.ActiveBlocks[index1];
        board.ActiveBlocks[index1] = board.ActiveBlocks[index2];
        board.ActiveBlocks[index2] = tempValue;

        swapBlocks.Add(board.ActiveBlocks[index1]);
        swapBlocks.Add(board.ActiveBlocks[index2]);
        return swapBlocks;
    }

    /// <summary>
    /// Checks if any match exists on the game board.
    /// </summary>
    /// <param name="board">The game board.</param>
    /// <returns>Returns true if any match exists; otherwise, false.</returns>
    private static bool HasMatch(Board board)
    {
        int width = board.Width;
        int height = board.Height;

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                // Check if there is a match at the current position
                if (CheckForMatch(board, row, col))
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Checks if a match exists at the specified position on the game board.
    /// </summary>
    /// <param name="board">The game board.</param>
    /// <param name="row">Row index of the position.</param>
    /// <param name="col">Column index of the position.</param>
    /// <returns>Returns true if a match exists at the specified position; otherwise, false.</returns>
    private static bool CheckForMatch(Board board, int row, int col)
    {
        int width = board.Width;
        IBlockEntityTypeDefinition targetType = board.ActiveBlocks[row * width + col].EntityType;

        if (targetType is LockedBlockTypeDefinition) return false;

        int[] dx = { -1, 1, 0, 0 }; // Horizontal directions (left and right)
        int[] dy = { 0, 0, -1, 1 }; // Vertical directions (up and down)

        // Check for matches in horizontal and vertical directions
        for (int direction = 0; direction < 4; direction++)
        {
            int matchCount = 1;
            int newRow = row + dy[direction];
            int newCol = col + dx[direction];

            while (IsInsideBoard(board, newRow, newCol) && board.ActiveBlocks[newRow * width + newCol].EntityType == targetType)
            {
                matchCount++;
                newRow += dy[direction];
                newCol += dx[direction];
            }

            if (matchCount >= 3)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Checks if the specified position is inside the game board boundaries.
    /// </summary>
    /// <param name="board">The game board.</param>
    /// <param name="row">Row index of the position.</param>
    /// <param name="col">Column index of the position.</param>
    /// <returns>Returns true if the position is inside the game board; otherwise, false.</returns>
    private static bool IsInsideBoard(Board board, int row, int col)
    {
        return row >= 0 && row < board.Height && col >= 0 && col < board.Width;
    }

    /// <summary>
    /// Gets a list of all possible matches on the game board.
    /// </summary>
    /// <param name="board">The game board.</param>
    /// <returns>List of Tuple pairs representing possible matches.</returns>
    public static List<Tuple<int, int>> GetAllPossibleMatches(Board board)
    {
        List<Tuple<int, int>> possibleMatches = new List<Tuple<int, int>>();

        int width = board.Width;
        int height = board.Height;
        int gridCount = width * height;

        for (int index1 = 0; index1 < gridCount; index1++)
        {
            for (int index2 = index1 + 1; index2 < gridCount; index2++)
            {
                if (IsValidAdjacentMove(board, index1, index2) && MatchExists(board, index1, index2))
                {
                    Tuple<int, int> matchTuple = GetSortedTuple(index1, index2);

                    if (!possibleMatches.Contains(matchTuple))
                    {
                        possibleMatches.Add(matchTuple);
                    }
                }
            }
        }

        return possibleMatches;
    }

    /// <summary>
    /// Checks if two elements are adjacent on the game board.
    /// </summary>
    /// <param name="board">The game board.</param>
    /// <param name="sourceIndex">Index of the first element.</param>
    /// <param name="targetIndex">Index of the second element.</param>
    /// <returns>Returns true if the two elements are adjacent; otherwise, false.</returns>
    public static bool IsValidAdjacentMove(Board board, int sourceIndex, int targetIndex)
    {
        int width = board.Width;
        int row1 = sourceIndex / width;
        int col1 = sourceIndex % width;
        int row2 = targetIndex / width;
        int col2 = targetIndex % width;

       if (board.ActiveBlocks[sourceIndex] == null || board.ActiveBlocks[targetIndex] == null)
        {
            return false;
        }

        if (board.ActiveBlocks[sourceIndex].Situated || board.ActiveBlocks[targetIndex].Situated)
        {
            return false;
        }

        return Math.Abs(row1 - row2) + Math.Abs(col1 - col2) == 1; // Only adjacent elements are valid moves
    }

    /// <summary>
    /// Returns a sorted tuple of two indices.
    /// </summary>
    /// <param name="index1">The first index.</param>
    /// <param name="index2">The second index.</param>
    /// <returns>Sorted tuple of the two indices.</returns>
    private static Tuple<int, int> GetSortedTuple(int index1, int index2)
    {
        return index1 < index2 ? new Tuple<int, int>(index1, index2) : new Tuple<int, int>(index2, index1);
    }

    /// <summary>
    /// Shuffles the blocks on the game board.
    /// </summary>
    /// <param name="board">The game board.</param>
    public static void Shuffle(Board board)
    {
        int width = board.Width;
        int height = board.Height;
        int gridCount = width * height;
        List<int> lockedIds = board.LockedIds;
        int maxShuffleAttempts = 400; // You can adjust this value as needed

        for (int attempt = 0; attempt < maxShuffleAttempts; attempt++)
        {
            Block[] shuffledGrid = new Block[gridCount];
            Array.Copy(board.ActiveBlocks, shuffledGrid, gridCount); // Make a copy of the board.Grid

            System.Random rand = new System.Random();

            for (int i = gridCount - 1; i > 0; i--)
            {
                int randomIndex = rand.Next(i + 1);
                if (shuffledGrid[i].Situated) continue;

                while (shuffledGrid[randomIndex].Situated)
                {
                    randomIndex = rand.Next(i + 1);
                }

                Block temp = shuffledGrid[i];
                shuffledGrid[i] = shuffledGrid[randomIndex];
                shuffledGrid[randomIndex] = temp;
            }

            Board shuffledBoard = new Board(width, height, shuffledGrid, lockedIds);

            if (IsValidShuffledBoard(board, shuffledBoard))
            {
                Array.Copy(shuffledGrid, board.ActiveBlocks, gridCount); // Copy back the shuffledGrid to board.Grid
                return;
            }
        }
    }

    /// <summary>
    /// Checks if a shuffled board is valid (not the same as the original board).
    /// </summary>
    /// <param name="originalBoard">The original game board.</param>
    /// <param name="shuffledBoard">The shuffled game board.</param>
    /// <returns>Returns true if the shuffled board is valid; otherwise, false.</returns>
    private static bool IsValidShuffledBoard(Board originalBoard, Board shuffledBoard)
    {
        return HasAnyPossibleMatch(shuffledBoard) && !CheckArrayEquality(originalBoard.ActiveBlocks, shuffledBoard.ActiveBlocks) && !HasMatch(shuffledBoard);
    }
   
    /// <summary>
    /// Checks if there is any match on the game board.
    /// </summary>
    /// <param name="board">The reference to the game board.</param>
    /// <returns>Returns true if any match exists on the board; otherwise, returns false.</returns>
    public static bool IsAnyMatchExistsInBoard(Board board)
    {
        int width = board.Width;
        int height = board.Height;

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                // Check if there is a match at the current position
                if (CheckForMatch(board, row, col))
                {
                    return true; // If a match is found, return true
                }
            }
        }

        return false; // If no match is found, return false
    }

   
    /// <summary>
    /// Checks if there are any possible matches on the game board.
    /// </summary>
    /// <param name="board">The reference to the game board.</param>
    /// <returns>Returns true if there are any possible matches; otherwise, returns false.</returns>
    public static bool HasAnyPossibleMatch(Board board)
    {
        return GetAllPossibleMatches(board).Count > 0;
    }

    /// <summary>
    /// Checks if two arrays of blocks are equal.
    /// </summary>
    /// <param name="originalGrid">The original block array.</param>
    /// <param name="shuffledGrid">The shuffled block array.</param>
    /// <returns>Returns true if the arrays are equal; otherwise, returns false.</returns>
    private static bool CheckArrayEquality(Block[] originalGrid, Block[] shuffledGrid)
    {
        for (int i = 0; i < originalGrid.Length; i++)
        {
            if (originalGrid[i] != shuffledGrid[i])
                return false;
        }

        return true;
    }

    /// <summary>
    /// Gets the list of block indices below the locked block.
    /// </summary>
    /// <param name="board">The game board.</param>
    /// <param name="lockedId">Index of the locked block.</param>
    /// <returns>List of block indices below the locked block.</returns>
    public static List<int> GetBlocksBelowIds(Board board, int lockedId)
    {
        List<int> lockedIds = new List<int>();

        int width = board.Width;
        int height = board.Height;

        for (int i = board.ActiveBlocks.Length - 1; i >= lockedId; i--)
        {
            if ((i % width) == (lockedId % width))
            {
                lockedIds.Add(i);
            }
        }
        return lockedIds;
    }

    /// <summary>
    /// Finds and returns the indices of blocks that are part of matches on the game board.
    /// </summary>
    /// <param name="blocks">The array of blocks on the board.</param>
    /// <param name="boardWidth">The width of the game board.</param>
    /// <returns>List of block indices that are part of matches.</returns>
    public static List<int> GetMatchingBlocks(Block[] blocks, int boardWidth, int boardHeight)
    {
        List<int> matchingBlockIds = new List<int>();
        int maxMatchCount = 0;

        for (int i = 0; i < blocks.Length; i++)
        {
            int x = i % boardWidth;
            int y = i / boardWidth;

            if (!blocks[i]) continue;
            if (blocks[i].EntityType is LockedBlockTypeDefinition) continue;

            IBlockEntityTypeDefinition currentBlockType = blocks[i].EntityType;

            List<int> horizontalMatches = CheckHorizontalMatches(blocks, boardWidth, x, y, currentBlockType);
            List<int> verticalMatches = CheckVerticalMatches(blocks, boardWidth, boardHeight, x, y, currentBlockType);

            int currentMatchCount = horizontalMatches.Count + verticalMatches.Count;

            if (currentMatchCount >= 3 && currentMatchCount >= maxMatchCount)
            {
                if (currentMatchCount > maxMatchCount)
                {
                    matchingBlockIds.Clear();
                    maxMatchCount = currentMatchCount;
                }
                for (int j = 0; j < horizontalMatches.Count; j++)
                {
                    if (!matchingBlockIds.Contains(horizontalMatches[j]))
                    {
                        matchingBlockIds.Add(horizontalMatches[j]);
                    }
                }
                for (int j = 0; j < verticalMatches.Count; j++)
                {
                    if (!matchingBlockIds.Contains(verticalMatches[j]))
                    {
                        matchingBlockIds.Add(verticalMatches[j]);
                    }
                }
            }
        }

        return matchingBlockIds;
    }

    /// <summary>
    /// Checks for horizontal matches starting from a specific position.
    /// </summary>
    /// <param name="blocks">The array of blocks on the board.</param>
    /// <param name="boardWidth">The width of the game board.</param>
    /// <param name="startX">The starting x-coordinate.</param>
    /// <param name="y">The y-coordinate.</param>
    /// <param name="targetType">The target block type to match.</param>
    /// <returns>List of indices of blocks that are part of horizontal matches.</returns>
    private static List<int> CheckHorizontalMatches(Block[] blocks, int boardWidth, int startX, int y, IBlockEntityTypeDefinition targetType)
    {
        List<int> matchingBlockIds = new List<int>();

        int count = 0;
        for (int x = startX; x < boardWidth; x++)
        {
            if (!blocks[x + y * boardWidth]) break;

            IBlockEntityTypeDefinition currentBlockType = blocks[x + y * boardWidth].EntityType;

            if (currentBlockType == targetType && !matchingBlockIds.Contains(x + y * boardWidth))
            {
                matchingBlockIds.Add(x + y * boardWidth);
                count++;
            }
            else
            {
                break;
            }
        }

        return count >= 3 ? matchingBlockIds : new List<int>();
    }


    /// <summary>
    /// Checks for vertical matches starting from a specific position.
    /// </summary>
    /// <param name="blocks">The array of blocks on the board.</param>
    /// <param name="boardWidth">The width of the game board.</param>
    /// <param name="x">The x-coordinate.</param>
    /// <param name="startY">The starting y-coordinate.</param>
    /// <param name="targetType">The target block type to match.</param>
    /// <returns>List of indices of blocks that are part of vertical matches.</returns>
    private static List<int> CheckVerticalMatches(Block[] blocks, int boardWidth, int boardHeight, int x, int startY, IBlockEntityTypeDefinition targetType)
    {
        List<int> matchingBlockIds = new List<int>();

        int count = 0;
        for (int y = startY; y < boardHeight; y++) // Use boardHeight here
        {
            if (!blocks[x + y * boardWidth]) break;

            IBlockEntityTypeDefinition currentBlockType = blocks[x + y * boardWidth].EntityType;

            if (currentBlockType == targetType && !matchingBlockIds.Contains(x + y * boardWidth))
            {
                matchingBlockIds.Add(x + y * boardWidth);
                count++;
            }
            else
            {
                break;
            }
        }

        return count >= 3 ? matchingBlockIds : new List<int>();
    }


    /// <summary>
    /// Shifts the non-situated blocks on the game board downwards to fill empty spaces.
    /// </summary>
    /// <param name="board">The game board.</param>
    /// <param name="boardWidth">The width of the game board.</param>
    /// <returns>List of blocks that have been shifted.</returns>
    public static List<Block> ShiftBlocks(Board board, int boardWidth)
    {
        List<Block> shiftBlocks = new List<Block>();

        for (int x = 0; x < boardWidth; x++)
        {
            for (int y = board.ActiveBlocks.Length / boardWidth - 1; y >= 0; y--)
            {
                int currentIndex = x + y * boardWidth;
                Block currentBlock = board.ActiveBlocks[currentIndex];

                if (currentBlock != null && !currentBlock.Situated)
                {
                    int emptyIndex = currentIndex;

                    // Check if there is a situated block above the current block
                    while (emptyIndex + boardWidth < board.ActiveBlocks.Length &&
                           (board.ActiveBlocks[emptyIndex + boardWidth] == null ||
                           board.ActiveBlocks[emptyIndex + boardWidth].Situated))
                    {
                        if (board.ActiveBlocks[emptyIndex + boardWidth] != null && board.ActiveBlocks[emptyIndex + boardWidth].Situated)
                        {
                            break;
                        }
                        emptyIndex += boardWidth;
                    }

                    if (emptyIndex != currentIndex)
                    {
                        board.ActiveBlocks[emptyIndex] = currentBlock;
                        board.ActiveBlocks[currentIndex] = null;
                        shiftBlocks.Add(board.ActiveBlocks[emptyIndex]);
                    }
                }
            }
        }

        return shiftBlocks;
    }
}
