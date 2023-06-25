using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryManager : MonoBehaviour
{

    public Transform WorldOrientation;
    public List<MemoryWorldTile> WordTilePrefabs = new List<MemoryWorldTile>();

    [HideInInspector] public List<MemoryWorldTile> SelectedTiles = new List<MemoryWorldTile>();

    private int Width;
    private int Height;

    private Grid<MemoryTile> m_grid;
    private List<MemoryWorldTile> m_worldTiles;

    [HideInInspector] public bool CanChackTwoPair;
    public float VelocityAfterTurnTheRotationTo0;

    public int InitialScore;
    public int Score;

    /// <summary>
    /// figure out name to increase the score every time the combination is right
    /// </summary>
    public int IncreaseNumber;

    /// <summary>
    /// bool to set if the game is unlocked
    /// </summary>
    public bool GameTriggered;


    private void Awake()
    {
        GeneratePuzzle();
        ShuffleTile();
        if (WorldOrientation != null) SetPositionAndRotation(WorldOrientation);
    }


    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R)) ShuffleTile();
    //}

    private MemoryTile ReturnMemoryTile(int x, int y)
    {
        return new MemoryTile(x, y);
    }


    private void GeneratePuzzle()
    {
        Width = WordTilePrefabs.Count/2;
        Height = WordTilePrefabs.Count/2;

        m_grid = new Grid<MemoryTile>(Width, Height, WordTilePrefabs[0].transform.lossyScale.x, transform.position, ReturnMemoryTile);
        m_worldTiles = new();

        int index = 0;

        for (int y = 0; y < m_grid.GetHeight(); y++)
        {
            for (int x = 0; x < m_grid.GetWidth(); x++)
            {

                MemoryWorldTile worldTile = Instantiate(WordTilePrefabs[index], m_grid.GetWorldPosition(x, y), Quaternion.identity, transform);

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
        List<Vector3> positions = new List<Vector3>();

        //for (int y = 0; y < m_grid.GetHeight(); y++)
        //{
        //    for (int x = 0; x < m_grid.GetWidth(); x++)
        //    {
        //        Vector3 newPosition = m_grid.GetWorldPosition(x, y);

        //        positions.Add(newPosition);
        //    }
        //}

        foreach(MemoryWorldTile worldTile in m_worldTiles)
        {
            Vector3 newPosition = worldTile.transform.localPosition;
            positions.Add(newPosition);
        }

        foreach (MemoryWorldTile worldTile in m_worldTiles)
        {
            Vector3 newPosition = positions[Random.Range(0, positions.Count)];

            worldTile.NewLocalPosition(newPosition);

            worldTile.RotateCard(180);

            positions.Remove(newPosition);

        }

    }

    private void SetPositionAndRotation(Transform targetTransform)
    {
        transform.rotation = targetTransform.rotation;
        transform.position = targetTransform.position;
    }


}
