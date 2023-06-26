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

    [HideInInspector]
    public bool Paired;
    

    private void Awake()
    {
        m_memoryManager = GameManager.Instance.MemoryManager;
        m_memoryManager.CanChackTwoPair = true;
    }

    public void SetTileData(MemoryTile memoryTile) => m_data = memoryTile;
    
    //public int GetId() => data.ID;

    public int SetID(int value) => m_data.ID = value;

    public void SetGridManager(Grid<MemoryTile> g) => m_grid = g;

    //public void NewPosition(Vector2 vector2) => transform.position = vector2;
    public void NewLocalPosition(Vector3 position) => transform.localPosition = position;

    //public void RotateCard(float rotatioValue) => transform.rotation = Quaternion.Euler(0, rotatioValue, 0);
    public void RotateCard(float rotatioValue) => transform.Rotate(0f, rotatioValue, 0f, Space.Self);


    public void OnPointerDown(PointerEventData eventData)
    {
        //if (!m_memoryManager.GameTriggered) return;

        //if (Paired) return;

        //if (m_memoryManager.CanChackTwoPair)
        //{
        //    StoreThisTile();
        //    if (m_memoryManager.SelectedTiles.Count == 1) m_memoryManager.SelectedTiles[0].RotateCard(180);
        //    else m_memoryManager.SelectedTiles[1].RotateCard(180);

        //    if (m_memoryManager.SelectedTiles.Count == 2)
        //    {
        //        if (m_memoryManager.SelectedTiles[0].m_data.ID == m_memoryManager.SelectedTiles[1].m_data.ID)
        //        {
        //            m_memoryManager.SelectedTiles[0].Paired = true;
        //            m_memoryManager.SelectedTiles[1].Paired = true;

        //            m_memoryManager.SetScore(m_memoryManager.IncreaseNumber); //added by gabe 

        //            Debug.Log("uguali");
        //        }
        //        else
        //        {
        //            //add delay
        //            m_memoryManager.SetScore(-1);
        //            StartCoroutine(RotateCard(m_memoryManager.SelectedTiles, 1));
        //            Debug.Log("diversi");
        //        }
        //    }
        //}

        test();
    }

    

    private void StoreThisTile()
    {
        if(m_memoryManager.SelectedTiles.Count == 2) m_memoryManager.SelectedTiles.Clear();
        m_memoryManager.SelectedTiles.Add(this);
    }

    private IEnumerator RotateCard(List<MemoryWorldTile> memoryWorldTiles, int index)
    {
        m_memoryManager.CanChackTwoPair = false;
        //Debug.Log(m_memoryManager.CanChackTwoPair);
        yield return new WaitForSeconds(m_memoryManager.VelocityAfterTurnTheRotationTo0);
        memoryWorldTiles[index].RotateCard(180);
        memoryWorldTiles[index - 1].RotateCard(180);
        m_memoryManager.CanChackTwoPair = true;
        //Debug.Log(m_memoryManager.CanChackTwoPair);
    }

    /////////////// added by gabe //////////////////////////

    private void test()
    {
        Debug.Log(m_memoryManager.SelectedTiles.Count);
        if (!m_memoryManager.GameTriggered) return;
        if (Paired) return;
        if (!m_memoryManager.CanChackTwoPair) return;

        if (m_memoryManager.SelectedTiles.Count == 0)
        {
            StoreThisTile();
            RotateCard(180);
        }
        else if (m_memoryManager.SelectedTiles.Count == 1 && m_memoryManager.SelectedTiles[0] != this)
        {
            StoreThisTile();
            RotateCard(180);
            if (m_memoryManager.SelectedTiles[0].m_data.ID == m_memoryManager.SelectedTiles[1].m_data.ID)
            {
                m_memoryManager.SelectedTiles[0].Paired = true;
                m_memoryManager.SelectedTiles[1].Paired = true;
                m_memoryManager.SetScore(m_memoryManager.IncreaseNumber);
            }
            else StartCoroutine(NotMatchedTiles(m_memoryManager.SelectedTiles[0], m_memoryManager.SelectedTiles[1]));

            m_memoryManager.SelectedTiles.Clear();
        }
    }

    private IEnumerator NotMatchedTiles(MemoryWorldTile tileA, MemoryWorldTile tileB)
    {
        m_memoryManager.CanChackTwoPair = false;
        //Debug.Log(m_memoryManager.CanChackTwoPair);
        yield return new WaitForSeconds(m_memoryManager.VelocityAfterTurnTheRotationTo0);
        tileA.RotateCard(180);
        tileB.RotateCard(180);
        m_memoryManager.SetScore(-1);
        m_memoryManager.CanChackTwoPair = true;
        //Debug.Log(m_memoryManager.CanChackTwoPair);
    }
}
