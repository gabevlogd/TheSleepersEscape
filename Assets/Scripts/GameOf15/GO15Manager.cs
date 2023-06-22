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
        //if (Input.GetKeyDown(KeyCode.F)) CheckWinCondition();
    }

    private void GeneratePuzzle()
    {
        m_grid = new Grid<GO15Tile>(Width, Height, WorldTilePrefab.transform.lossyScale.x, transform.position, (int x, int y) => new GO15Tile(x, y));
        m_worldTiles = new();

        for (int y = m_grid.GetHeight() - 1; y >= 0; y--)
        {
            for (int x = 0; x < m_grid.GetWidth(); x++)
            {
                GO15WorldTile worldTile = Instantiate(WorldTilePrefab, m_grid.GetWorldPosition(x, y), Quaternion.identity, transform);
                worldTile.SetGridManager(m_grid);
                GO15Tile tileData = m_grid.GetGridObject(x, y);
                worldTile.InitTileData(ref tileData);

                if (x == 3 && y == 0)
                {
                    Destroy(worldTile.GetComponentInChildren<MeshRenderer>().gameObject);
                    Destroy(worldTile.GetComponentInChildren<TextMeshPro>().gameObject);
                    m_worldTiles.Add(worldTile);
                    continue;
                }

                m_worldTiles.Add(worldTile);
            }
        }

        //ShuffleTile();
    }

    public void ShuffleTile()
    {
        List<int> numbersPool = new List<int>();
        for (int i = 1; i < Width * Height; i++) numbersPool.Add(i);

        int[] numberConfiguration = new int[Width * Height];
        int index = 0;

        foreach (GO15WorldTile worldTile in m_worldTiles)
        {
            if (worldTile.GetComponentInChildren<MeshRenderer>() == null)
            {
                numberConfiguration[index] = 0;
                index++;
                continue;
            }

            int newNumber = numbersPool[Random.Range(0, numbersPool.Count)]; //randomly picks a new number for the current tile 
            numbersPool.Remove(newNumber);
            worldTile.SetTileNumber(newNumber);

            numberConfiguration[index] = newNumber;
            index++;
            //Debug.Log(newNumber);
        }

        if (GO15Solver.IsSolvable(numberConfiguration, 4)) Debug.Log("solvable");
        else ShuffleTile();
    }

    public static void SwapTiles()
    {
        //swaps positions in world
        Vector3 positionA = SelectedTiles[0].transform.position;
        SelectedTiles[0].transform.position = SelectedTiles[1].transform.position;
        SelectedTiles[1].transform.position = positionA;

        //swaps in array
        int index0 = m_instance.m_worldTiles.FindIndex(value => value == SelectedTiles[0]);
        int index1 = m_instance.m_worldTiles.FindIndex(value => value == SelectedTiles[1]);
        m_instance.m_worldTiles[index0] = SelectedTiles[1];
        m_instance.m_worldTiles[index1] = SelectedTiles[0];


        CheckWinCondition();
    }

    private static void CheckWinCondition()
    {
        int nextNumber = m_instance.m_worldTiles[1].GetTileNumber();
        foreach (GO15WorldTile worldTile in m_instance.m_worldTiles)
        {
            if (worldTile.GetTileNumber() == nextNumber - 1)
            {
                nextNumber++;
            }
            else
            {
                Debug.Log("not win");
                break;
            }

            if (nextNumber == 16)
            {
                Debug.Log("win");
                break;
            }
        }
    }
}
