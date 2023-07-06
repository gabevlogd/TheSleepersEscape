using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryManager : MonoBehaviour, IPuzzle
{
    public Transform CameraTriggerer;
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
    [HideInInspector]
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
        //ShuffleTile();
        if (WorldOrientation != null) SetPositionAndRotation(WorldOrientation);
    }

    private void Start()
    {
        GameManager.Instance.EventManager.Registrer(Enumerators.Events.StartPuzzle, StartGame);
        GameManager.Instance.EventManager.Registrer(Enumerators.Events.ResetPuzzle, ResetGame);
        GameManager.Instance.EventManager.Registrer(Enumerators.Events.PuzzleCompleted, EndGame);
    }

    private void OnDisable()
    {
        GameManager.Instance.EventManager.Unregistrer(Enumerators.Events.StartPuzzle, StartGame);
        GameManager.Instance.EventManager.Unregistrer(Enumerators.Events.ResetPuzzle, ResetGame);
        GameManager.Instance.EventManager.Unregistrer(Enumerators.Events.PuzzleCompleted, EndGame);
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

    private void ShuffleAnimation()
    {
        foreach (MemoryWorldTile worldTile in m_worldTiles)
        {
            worldTile.RotateCard(true);

        }
    }

    private IEnumerator Shuffle()
    {
        CanChackTwoPair = false;
        ShuffleAnimation();
        yield return new WaitForSeconds(2f);
        ShuffleTile();
        CanChackTwoPair = true;
    }


    private void ShuffleTile()
    {
        List<Vector3> positions = new List<Vector3>();

        foreach(MemoryWorldTile worldTile in m_worldTiles)
        {
            Vector3 newPosition = worldTile.transform.localPosition;
            positions.Add(newPosition);
            
        }

        foreach (MemoryWorldTile worldTile in m_worldTiles)
        {
            Vector3 newPosition = positions[Random.Range(0, positions.Count)];

           

            worldTile.NewLocalPosition(newPosition);

            worldTile.transform.localRotation = Quaternion.identity; // added by gabe
            //worldTile.RotateCard(180);
            
            worldTile.Paired = false; //added by gabe

            positions.Remove(newPosition);

        }

    }

    

    private void SetPositionAndRotation(Transform targetTransform)
    {
        transform.rotation = targetTransform.rotation;
        transform.position = targetTransform.position;
    }

    public void SetScore(int value)
    {
        Score += value;
        CheckLoseCondition();
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        foreach(MemoryWorldTile worldTile in m_worldTiles)
        {
            if (!worldTile.Paired) return;
        }

        Debug.Log("win");
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.PuzzleCompleted);
    }

    private void CheckLoseCondition()
    {
        if (Score <= 0) GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.ResetPuzzle);
    }

    //private void TurnFaceUp()
    //{
    //    foreach (MemoryWorldTile worldTile in m_worldTiles)
    //    {
    //        worldTile.transform.localRotation = Quaternion.identity;

    //    }
    //}
    private void TurnFaceUp()
    {
        foreach (MemoryWorldTile worldTile in m_worldTiles)
        {
            worldTile.RotateCard(false);

        }
    }

    public void StartGame() 
    {
        GameTriggered = true;
        Score = InitialScore;
        StartCoroutine(Shuffle());
    }

    public void EndGame()
    {
        GameTriggered = false;
        CameraTriggerer.gameObject.SetActive(false);
    }

    public void ResetGame()
    {
        GameTriggered = false;
        Score = InitialScore;
        TurnFaceUp();
    }


}
