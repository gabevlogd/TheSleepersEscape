using System;


/// <summary>
/// deals with verifying whether a 15 game configuration is solvable or not
/// </summary>
public class GO15Solver
{
    /// <summary>
    /// Returns the number of inversions of the configuration
    /// </summary
    public static int CountInversions(int[] tiles)
    {
        int inversions = 0;
        int size = tiles.Length;

        for (int i = 0; i < size - 1; i++)
        {
            for (int j = i + 1; j < size; j++)
            {
                // If the current element is greater than the next element, increment the flip counter
                if (tiles[i] > tiles[j] && tiles[i] != 0 && tiles[j] != 0)
                {
                    inversions++;
                }
            }
        }

        return inversions;
    }

    /// <summary>
    /// Checks if the configuration is solvable
    /// </summary>
    public static bool IsSolvable(int[] tiles, int gridSize)
    {
        int inversions = CountInversions(tiles);
        int emptyRow = GetEmptyRow(tiles, gridSize);

        // If the total number of inverions plus the number of rows where the blank tile is located is even, the configuration is solvable
        return (inversions + emptyRow) % 2 == 0;
    }

    /// <summary>
    /// Retruns the row of the empty tile
    /// </summary>
    public static int GetEmptyRow(int[] tiles, int gridSize)
    {
        int emptyIndex = Array.IndexOf(tiles, 0);
        int row = emptyIndex / gridSize;
        return row + 1;
    }
}

