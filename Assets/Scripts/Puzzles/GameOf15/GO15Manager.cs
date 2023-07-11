using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GO15Manager : MonoBehaviour, IPuzzle
{
    public static List<GO15WorldTile> SelectedTiles = new();

    public Transform CameraTriggerer;
    public GO15WorldTile WorldTilePrefab;
    public Transform WorldOrientation;
    public float DefaultTimer;
    [HideInInspector]
    public bool GameTriggered;

    private int m_width = 4;
    private int m_height = 4;
    private float m_timer;
    private bool m_outOfTime;


    private Grid<GO15Tile> m_grid;
    private List<GO15WorldTile> m_worldTiles;

    private static GO15Manager m_instance;

    private TextMeshPro m_tile06;



    private void Awake()
    {
        m_instance = this;
        m_timer = 0f;
        m_outOfTime = true;

        GeneratePuzzle();
        ShuffleTile();
        if (WorldOrientation != null) SetPositionAndRotation(WorldOrientation);
    }

    private void Start()
    {
        GameManager.Instance.EventManager.Register(Enumerators.Events.StartPuzzle, StartGame);
        GameManager.Instance.EventManager.Register(Enumerators.Events.ResetPuzzle, ResetGame);
        GameManager.Instance.EventManager.Register(Enumerators.Events.PuzzleCompleted, EndGame);
    }

    private void OnDisable()
    {
        GameManager.Instance.EventManager.Unregister(Enumerators.Events.StartPuzzle, StartGame);
        GameManager.Instance.EventManager.Unregister(Enumerators.Events.ResetPuzzle, ResetGame);
        GameManager.Instance.EventManager.Unregister(Enumerators.Events.PuzzleCompleted, EndGame);
    }

    private void Update() => UpdateTimer();

    private void GeneratePuzzle()
    {
        m_grid = new Grid<GO15Tile>(m_width, m_height, WorldTilePrefab.transform.lossyScale.x, transform.position, (int x, int y) => new GO15Tile(x, y));
        m_worldTiles = new();

        for (int y = m_grid.GetHeight() - 1; y >= 0; y--)
        {
            for (int x = 0; x < m_grid.GetWidth(); x++)
            {
                GO15WorldTile worldTile = Instantiate(WorldTilePrefab, m_grid.GetWorldPosition(x, y), Quaternion.identity, transform);
                GO15Tile tileData = m_grid.GetGridObject(x, y);

                worldTile.SetGO15Manager(this);
                worldTile.SetGridManager(ref m_grid);
                worldTile.InitTileData(ref tileData);

                if (x == 3 && y == 0)
                {
                    worldTile.EmptyTile = true;
                    Destroy(worldTile.GetComponentInChildren<MeshRenderer>().gameObject); //makes the tile invissible
                    Destroy(worldTile.GetComponentInChildren<TextMeshPro>().gameObject);
                    m_worldTiles.Add(worldTile);
                    continue;
                }

                m_worldTiles.Add(worldTile);
            }
        }
    }

    public void ShuffleTile()
    {
        List<int> numbersPool = new List<int>();
        for (int i = 1; i < m_width * m_height; i++) numbersPool.Add(i);

        int[] numberConfiguration = new int[m_width * m_height];
        int index = 0;

        foreach (GO15WorldTile worldTile in m_worldTiles)
        {

            if (worldTile.EmptyTile)
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
        }

        if (GO15Solver.IsSolvable(numberConfiguration, m_grid.GetWidth())) Debug.Log("solvable");
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

        //da capire gabri
        GameManager.Instance.SoundEventManager.TriggerEvent(Enumerators.MusicEvents.PlaySoundEnv, GameManager.Instance.SoundManager.GO15SwitchTile);

        CheckWinCondition();
    }


    private static void CheckWinCondition()
    {
        int nextNumber = m_instance.m_worldTiles[1].GetTileNumber();

        foreach (GO15WorldTile worldTile in m_instance.m_worldTiles)
        {
            if (worldTile.GetTileNumber() == 6) m_instance.m_tile06 = worldTile.GetTMProTileNumber();
            if (worldTile.GetTileNumber() == nextNumber - 1)
            {
                nextNumber++;
                if (nextNumber == 17)
                {
                    Debug.Log("win");
                    m_instance.StartCoroutine(m_instance.WinEvent());
                    break;
                }
            }
            else
            {
                Debug.Log("not win");
                break;
            }
        }
    }

    private IEnumerator WinEvent()
    {
        GameTriggered = false;
        m_tile06.color = Color.green;
        yield return new WaitForSeconds(2f);
        GameManager.Instance.SoundEventManager.TriggerEvent(Enumerators.MusicEvents.PlaySoundEnv, GameManager.Instance.SoundManager.PenSound);
        yield return new WaitForSeconds(1f);
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.PuzzleCompleted); //win condition
    }

    private void SetPositionAndRotation(Transform targetTransform)
    {
        transform.rotation = targetTransform.rotation;
        transform.position = targetTransform.position;
    }

    private void UpdateTimer()
    {
        if (!m_outOfTime)
        {
            //Debug.Log(m_timer);
            if (m_timer >= 0f) m_timer -= Time.deltaTime;
            else GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.ResetPuzzle); //lose condition
        }
    }

    /// <summary>
    /// Start game event
    /// </summary>
    public void StartGame()
    {
        //da capire
        //GameManager.Instance.SoundEventManager.TriggerEvent(Enumerators.MusicEvents.PlaySoundEnv, GameManager.Instance.SoundManager.Watch);
        GameTriggered = true;
        m_timer = DefaultTimer;
        m_outOfTime = false;
        //maybe other stuff to implement...
    }

    /// <summary>
    /// End game event
    /// </summary>
    public void EndGame()
    {
        GameTriggered = false;
        m_timer = 0;
        m_outOfTime = true;
        CameraTriggerer.gameObject.SetActive(false);
        //maybe other stuff to implement...
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.EnableRadio);
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.PickUpNote);
    }

    /// <summary>
    /// Reset game event
    /// </summary>
    public void ResetGame()
    {
        GameTriggered = false;
        m_timer = 0;
        m_outOfTime = true;
        ShuffleTile();
        //maybe other stuff to implement...
    }
}
