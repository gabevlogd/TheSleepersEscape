using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    public static List<GO15Tile> SelectedTiles = new();

    public GO15Tile WorldTilePrefab;

    public int Width;
    public int Height;
    public float CellSize;
    public Vector3 GridOrigin;

    private Grid<Tile> m_grid;
    private List<GO15Tile> m_worldTiles;
    


    private void Awake()
    {
        GeneratePuzzle();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) ShuffleTile();
    }

    private void GeneratePuzzle()
    {
        m_grid = new Grid<Tile>(Width, Height, WorldTilePrefab.transform.lossyScale.x, transform.position, (int x, int y) => new Tile(x, y));
        m_worldTiles = new();

        for(int y = 0; y < m_grid.GetHeight(); y++)
        {
            for(int x = 0; x < m_grid.GetWidth(); x++)
            {
                GO15Tile worldTile = Instantiate(WorldTilePrefab, m_grid.GetWorldPosition(x, y), Quaternion.identity, transform);
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

        foreach (GO15Tile worldTile in m_worldTiles)
        {
            int newNumber = numbersPool[Random.Range(0, numbersPool.Count)]; //randomly picks a new number for the current tile 
            numbersPool.Remove(newNumber);
            m_grid.GetGridObject(worldTile.transform.position).TileNumber = newNumber;
            worldTile.SetTileNumber(newNumber); //update the world tile number (TextMeshPro);

        }
    }

    public static void SwapTiles()
    {
        Vector3 positionA = SelectedTiles[0].transform.position;
        SelectedTiles[0].transform.position = SelectedTiles[1].transform.position;
        SelectedTiles[1].transform.position = positionA;
        //CheckWinCondition();
    }

    //private static void CheckWinCondition()
    //{
    //    for (int y = 0; y < m_grid.GetHeight(); y++)
    //    {
    //        for (int x = 0; x < m_grid.GetWidth(); x++)
    //        {

    //        }

    
}
