using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryManager : Singleton<MemoryManager>
{
    public List<MemoryWorldTile> wordTilePrefabs = new List<MemoryWorldTile>();

    private int Width;
    private int Height;

    private Grid<MemoryTile> m_grid;
    private List<MemoryWorldTile> m_worldTiles;


    private void Awake()
    {
        GeneratePuzzle();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) ShuffleTile();
    }

    private MemoryTile ReturnMemoryTile(int x, int y)
    {
        return new MemoryTile(x, y);
    }


    private void GeneratePuzzle()
    {
        Width = wordTilePrefabs.Count/2;
        Height = wordTilePrefabs.Count/2;

        m_grid = new Grid<MemoryTile>(Width, Height, wordTilePrefabs[0].transform.lossyScale.x, transform.position, ReturnMemoryTile);
        m_worldTiles = new();

        int index = 0;

        for (int y = 0; y < m_grid.GetHeight(); y++)
        {
            for (int x = 0; x < m_grid.GetWidth(); x++)
            {

                MemoryWorldTile worldTile = Instantiate(wordTilePrefabs[index], m_grid.GetWorldPosition(x, y), Quaternion.identity, transform);

                worldTile.SetGridManager(m_grid);

                m_worldTiles.Add(worldTile);

                worldTile.SetTileData(m_grid.GetGridObject(x,y));

                worldTile.SetID(index);

                if (x % 2 != 0)
                {
                    index++;
                }

            }


        }
    }

    

    private void ShuffleTile()
    {
        List<Vector3> number = new List<Vector3>();
        for (int y = 0; y < m_grid.GetHeight(); y++)
        {
            for (int x = 0; x < m_grid.GetWidth(); x++)
            {
                Vector3 newPosition = new Vector3(x, y, 0);
                number.Add(newPosition);
                Debug.Log(number);
            }
        }


        foreach (MemoryWorldTile worldTile in m_worldTiles)
        {
            Vector3 newNumber = number[Random.Range(0, number.Count)]; //randomly picks a new number for the current tile 
            number.Remove(newNumber);
            
        }

    }

 


}
