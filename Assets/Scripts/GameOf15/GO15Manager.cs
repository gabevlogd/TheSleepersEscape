using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GO15Manager : MonoBehaviour
{
    public static List<GO15WorldTile> SelectedTiles = new();

    public GO15WorldTile WorldTilePrefab;

    public int Width;
    public int Height;
    //public float CellSize;
    //public Vector3 GridOrigin;

    private Grid<GO15Tile> m_grid;
    private List<GO15WorldTile> m_worldTiles;

    private static GO15Manager m_instance;



    private void Awake()
    {
        m_instance = this;
        GeneratePuzzle();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) ShuffleTile();
        if (Input.GetKeyDown(KeyCode.F)) CheckWinCondition();
    }

    //private GO15Tile pippo(int x,int y)
    //{
    //    return new GO15Tile(x, y);
    //}

    private void GeneratePuzzle()
    {
        m_grid = new Grid<GO15Tile>(Width, Height, WorldTilePrefab.transform.lossyScale.x, transform.position,(int x, int y) => new GO15Tile(x, y));
        m_worldTiles = new();

        for (int y = 0; y < m_grid.GetHeight(); y++)
        {
            for (int x = 0; x < m_grid.GetWidth(); x++)
            {
                GO15WorldTile worldTile = Instantiate(WorldTilePrefab, m_grid.GetWorldPosition(x, y), Quaternion.identity, transform);
                worldTile.SetGridManager(m_grid);

                if (x == 3 && y == 0)
                {
                    Destroy(worldTile.GetComponentInChildren<MeshRenderer>().gameObject);
                    Destroy(worldTile.GetComponentInChildren<TextMeshPro>().gameObject);
                    continue;
                }

                m_worldTiles.Add(worldTile);
            }
        }
    }

    public void ShuffleTile()
    {
        List<int> numbersPool = new List<int>();
        for (int i = 1; i < Width * Height; i++) numbersPool.Add(i);

        foreach (GO15WorldTile worldTile in m_worldTiles)
        {
            int newNumber = numbersPool[Random.Range(0, numbersPool.Count)]; //randomly picks a new number for the current tile 
            numbersPool.Remove(newNumber);
            m_grid.GetGridObject(worldTile.transform.position).TileNumber = newNumber;
            worldTile.SetTileNumber(newNumber); //update the world tile number (TextMeshPro);
            //Debug.Log(newNumber);

        }
    }

    public static void SwapTiles()
    {
        //swaps positions in world
        Vector3 positionA = SelectedTiles[0].transform.position;
        SelectedTiles[0].transform.position = SelectedTiles[1].transform.position;
        SelectedTiles[1].transform.position = positionA;

        //swaps datas
        m_instance.m_grid.GetGridObject(SelectedTiles[0].transform.position).TileNumber = SelectedTiles[0].GetTileNumber();
        m_instance.m_grid.GetGridObject(SelectedTiles[1].transform.position).TileNumber = SelectedTiles[1].GetTileNumber();

        //CheckWinCondition();
    }

    private static void CheckWinCondition()
    {
        for (int y = 0; y < m_instance.m_grid.GetHeight(); y++)
        {
            for (int x = 0; x < m_instance.m_grid.GetWidth(); x++)
            {
                Debug.Log(m_instance.m_grid.GetGridObject(x, y).TileNumber);
            }
        }

        //foreach (GO15WorldTile worldTile in m_instance.m_worldTiles)
        //{

        //    Debug.Log(m_instance.m_grid.GetGridObject(worldTile.transform.position).TileNumber);
        //}
    }
}
