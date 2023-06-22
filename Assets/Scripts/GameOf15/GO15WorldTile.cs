using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Gabevlogd.Patterns;

public class GO15WorldTile : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{


    private TextMeshPro m_tileNumber;
    private Grid<GO15Tile> m_grid;

    private void Awake() => m_tileNumber = GetComponentInChildren<TextMeshPro>();



    public void SetTileNumber(int value) => m_tileNumber.text = value.ToString();
    public int GetTileNumber() => int.Parse(m_tileNumber.text);
    public void SetGridManager(Grid<GO15Tile> g) => m_grid = g;



    public void OnPointerDown(PointerEventData eventData)
    {
        StoreThisTile();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Input.GetKey(KeyCode.Mouse0) || GetComponentInChildren<MeshRenderer>() != null) return;

        if (GO15Manager.SelectedTiles.Count != 1)
        {
            GO15Manager.SelectedTiles.Clear();
            return;
        }

        m_grid.GetXY(transform.position, out int currentX, out int currentY);
        m_grid.GetXY(GO15Manager.SelectedTiles[0].transform.position, out int lastX, out int lastY);

        


        if ((currentX == lastX && Mathf.Abs(currentY - lastY) == 1) || (currentY == lastY && Mathf.Abs(currentX - lastX) == 1))
        {
            GO15Manager.SelectedTiles.Add(this);
            GO15Manager.SwapTiles();
        }
        else GO15Manager.SelectedTiles.Clear();

    }

    private void StoreThisTile()
    {
        if (GO15Manager.SelectedTiles.Count != 0) GO15Manager.SelectedTiles.Clear();
        GO15Manager.SelectedTiles.Add(this);
    }

}
