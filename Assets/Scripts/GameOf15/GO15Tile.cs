using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Gabevlogd.Patterns;

public class GO15Tile : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    private TextMeshPro m_tileNumber;
    private Grid<Tile> m_grid;

    private void Awake() => m_tileNumber = GetComponentInChildren<TextMeshPro>();

    public void SetTileNumber(int value) => m_tileNumber.text = value.ToString();
    public void SetGridManager(Grid<Tile> g) => m_grid = g;

    public void OnPointerDown(PointerEventData eventData)
    {
        StoreThisTile();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Input.GetKey(KeyCode.Mouse0) || GetComponentInChildren<MeshRenderer>() != null) return;

        if (PuzzleManager.SelectedTiles.Count != 1)
        {
            PuzzleManager.SelectedTiles.Clear();
            return;
        }

        m_grid.GetXY(transform.position, out int currentX, out int currentY);
        m_grid.GetXY(PuzzleManager.SelectedTiles[0].transform.position, out int lastX, out int lastY);


        if ((currentX == lastX && Mathf.Abs(currentY - lastY) == 1) || (currentY == lastY && Mathf.Abs(currentX - lastX) == 1))
        {
            PuzzleManager.SelectedTiles.Add(this);
            PuzzleManager.SwapTiles();
        }
        else PuzzleManager.SelectedTiles.Clear();

    }

    private void StoreThisTile()
    {
        if (PuzzleManager.SelectedTiles.Count != 0) PuzzleManager.SelectedTiles.Clear();
        PuzzleManager.SelectedTiles.Add(this);
    }

}
