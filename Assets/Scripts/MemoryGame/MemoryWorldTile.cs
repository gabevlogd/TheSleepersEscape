using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;


public class MemoryWorldTile : MonoBehaviour, IPointerDownHandler
{
    
    private Grid<MemoryTile> m_grid;

    MemoryTile m_data;

    MemoryManager m_memoryManager;
    bool m_paired;
    

    private void Awake()
    {
        m_memoryManager = GameManager.instance.memoryManager;
        m_memoryManager.CanChackTwoPair = true;
    }

    public void SetTileData(MemoryTile memoryTile) => m_data = memoryTile;
    
    //public int GetId() => data.ID;

    public int SetID(int value) => m_data.ID = value;

    public void SetGridManager(Grid<MemoryTile> g) => m_grid = g;

    public void NewPosition(Vector2 vector2) => transform.position = vector2;

    public void RotateCard(float rotatioValue) => transform.rotation = Quaternion.Euler(0, rotatioValue, 0);


    public void OnPointerDown(PointerEventData eventData)
    {
        if (m_paired) return;

        if (!m_memoryManager.GameTriggered) return;

        if (m_memoryManager.CanChackTwoPair)
        {
            StoreThisTile();
            if (m_memoryManager.SelectedTiles.Count == 1) m_memoryManager.SelectedTiles[0].RotateCard(0);
            else m_memoryManager.SelectedTiles[1].RotateCard(0);

            if (m_memoryManager.SelectedTiles.Count == 2)
            {
                if (m_memoryManager.SelectedTiles[0].m_data.ID == m_memoryManager.SelectedTiles[1].m_data.ID)
                {
                    m_memoryManager.SelectedTiles[0].m_paired = true;
                    m_memoryManager.SelectedTiles[1].m_paired = true;
                    Debug.Log("uguali");
                }
                else
                {
                    //add delay
                    StartCoroutine(RotateCard(m_memoryManager.SelectedTiles, 1));
                    Debug.Log("diversi");
                }
            }
        }
    }

    private void StoreThisTile()
    {
        if(m_memoryManager.SelectedTiles.Count == 2) m_memoryManager.SelectedTiles.Clear();
        m_memoryManager.SelectedTiles.Add(this);
    }

    private IEnumerator RotateCard(List<MemoryWorldTile> memoryWorldTiles, int index)
    {
        m_memoryManager.CanChackTwoPair = false;
        Debug.Log(m_memoryManager.CanChackTwoPair);
        yield return new WaitForSeconds(m_memoryManager.VelocityAfterTurnTheRotationTo0);
        memoryWorldTiles[index].RotateCard(180);
        memoryWorldTiles[index - 1].RotateCard(180);
        m_memoryManager.CanChackTwoPair = true;
        Debug.Log(m_memoryManager.CanChackTwoPair);
    }
}
