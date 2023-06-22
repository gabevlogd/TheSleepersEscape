using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Gabevlogd.Patterns;

public class GO15WorldTile : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{

    private GO15Tile m_data; 
    private TextMeshPro m_tileNumber;
    private Grid<GO15Tile> m_grid;

    private void Awake() => m_tileNumber = GetComponentInChildren<TextMeshPro>();




    public void SetGridManager(Grid<GO15Tile> g) => m_grid = g;


    public int GetTileNumber() => m_data.TileNumber;

    //public void SetCoordinate(int x, int y)
    //{
    //    m_data.x = x;
    //    m_data.y = y;
    //}

    //public void GetCoordinate(out int x, out int y)
    //{
    //    x = m_data.x;
    //    y = m_data.y;
    //}


    public void SetTileNumber(int value)
    {
        m_data.TileNumber = value;
        m_tileNumber.text = m_data.TileNumber.ToString();
    }


    public void InitTileData(ref GO15Tile data)
    {
        m_data = data;
        m_tileNumber.text = m_data.TileNumber.ToString();
    }



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

        float distance = Vector3.Distance(transform.position, GO15Manager.SelectedTiles[0].transform.position);

        if (Mathf.Abs(distance - m_grid.GetCellSize()) <= 0.1f)
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
