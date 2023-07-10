using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class GO15WorldTile : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    [HideInInspector]
    public bool EmptyTile;

    private GO15Tile m_data; 
    private TextMeshPro m_tileNumber;
    private Grid<GO15Tile> m_grid;
    private GO15Manager m_go15Manager;

    private void Awake() => m_tileNumber = GetComponentInChildren<TextMeshPro>();

    public void OnPointerDown(PointerEventData eventData) => StoreThisTile();

    public void OnPointerEnter(PointerEventData eventData) => SwapWithLastSelectedTile();


    public void SetGridManager(ref Grid<GO15Tile> grid) => m_grid = grid;
    public void SetGO15Manager(GO15Manager manager) => m_go15Manager = manager;

    public int GetTileNumber() => m_data.TileNumber;
    public TextMeshPro GetTMProTileNumber() => m_tileNumber;

    public void SetTileNumber(int value)
    {
        m_data.TileNumber = value;
        if (value < 10) m_tileNumber.text = "0" + m_data.TileNumber.ToString();
        else m_tileNumber.text = m_data.TileNumber.ToString();
    }


    public void InitTileData(ref GO15Tile data)
    {
        m_data = data;
        m_tileNumber.text = m_data.TileNumber.ToString();
    }



    private void StoreThisTile()
    {
        //Debug.Log("StoreThisTile");
        if (GameManager.Instance.Player.PlayerStateMachine.CurrentState.StateID != Enumerators.PlayerState.RunningPuzzle) return;
        if (!m_go15Manager.GameTriggered) return;

        if (GO15Manager.SelectedTiles.Count != 0) GO15Manager.SelectedTiles.Clear();
        GO15Manager.SelectedTiles.Add(this);
    }

    private void SwapWithLastSelectedTile()
    {
        //Debug.Log("SwapWithLastSelectedTile");
        if (GameManager.Instance.Player.PlayerStateMachine.CurrentState.StateID != Enumerators.PlayerState.RunningPuzzle) return;
        if (!m_go15Manager.GameTriggered) return;

        if (!Input.GetKey(KeyCode.Mouse0) || !EmptyTile) return;

        if (GO15Manager.SelectedTiles.Count != 1)
        {
            GO15Manager.SelectedTiles.Clear();
            return;
        }

        float distance = Vector3.Distance(transform.position, GO15Manager.SelectedTiles[0].transform.position);

        if (Mathf.Abs(distance - m_grid.GetCellSize()) <= m_grid.GetCellSize() / 10f)
        {
            GO15Manager.SelectedTiles.Add(this);
            GO15Manager.SwapTiles();
        }
        else GO15Manager.SelectedTiles.Clear();
    }

}
