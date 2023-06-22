using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class MemoryWorldTile : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    
    private Grid<MemoryTile> m_grid;

    MemoryTile data;

    public void SetTileData(MemoryTile memoryTile) => data = memoryTile;
    
    //public int GetId() => data.ID;

    public int SetID(int value) => data.ID = value;

    public void SetGridManager(Grid<MemoryTile> g) => m_grid = g;



    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("premuto");

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       
    }



}
