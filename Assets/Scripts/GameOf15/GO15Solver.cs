using System;
using UnityEngine;

//thanks chat gpt :)

public class GO15Solver
{
    // Funzione per contare le inversioni
    public static int CountInversions(int[] tiles)
    {
        int inversions = 0;
        int size = tiles.Length;

        for (int i = 0; i < size - 1; i++)
        {
            for (int j = i + 1; j < size; j++)
            {
                // Se l'elemento corrente è maggiore dell'elemento successivo, incrementa il contatore di inversioni
                if (tiles[i] > tiles[j] && tiles[i] != 0 && tiles[j] != 0)
                {
                    inversions++;
                }
            }
        }

        return inversions;
    }

    // Funzione per verificare la risolvibilità della configurazione
    public static bool IsSolvable(int[] tiles, int gridSize)
    {
        int inversions = CountInversions(tiles);
        int emptyRow = GetEmptyRow(tiles, gridSize);

        // Se il numero totale di inversioni più il numero di righe in cui si trova la tessera vuota è pari, la configurazione è risolvibile
        return (inversions + emptyRow) % 2 == 0;
    }

    // Funzione per ottenere la riga in cui si trova la tessera vuota
    public static int GetEmptyRow(int[] tiles, int gridSize)
    {
        int emptyIndex = Array.IndexOf(tiles, 0);
        int row = emptyIndex / gridSize;
        return row + 1;
    }
}

