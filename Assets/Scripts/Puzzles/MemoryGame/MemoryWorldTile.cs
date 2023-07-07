using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MemoryWorldTile : MonoBehaviour, IPointerDownHandler
{
    private Grid<MemoryTile> m_grid;

    MemoryTile m_data;

    MemoryManager m_memoryManager;

    [HideInInspector]
    public bool Paired;

    Animator m_animator;

    private void Awake()
    {
        m_memoryManager = GameManager.Instance.MemoryManager;
        m_memoryManager.CanChackTwoPair = true;
        m_animator = GetComponentInChildren<Animator>();
    }


    public void SetTileData(MemoryTile memoryTile) => m_data = memoryTile;
    
    //public int GetId() => data.ID;

    public int SetID(int value) => m_data.ID = value;

    public void SetGridManager(Grid<MemoryTile> g) => m_grid = g;

    public void NewLocalPosition(Vector3 position) => transform.localPosition = position;

    //public void RotateCard(float rotatioValue) => transform.Rotate(0f, rotatioValue, 0f, Space.Self);
    public void RotateCard(bool canRotate) => m_animator.SetBool("canRotate", canRotate);


    public void OnPointerDown(PointerEventData eventData)
    {
        //if (!m_memoryManager.GameTriggered) return;
        //if (Paired) return;
        //if (!m_memoryManager.CanChackTwoPair) return;


        //if (m_memoryManager.SelectedTiles.Count == 0)
        //{
        //    StoreThisTile();
        //    m_memoryManager.SelectedTiles[0].RotateCard(180);
        //}
        //else if (m_memoryManager.SelectedTiles.Count == 1 && m_memoryManager.SelectedTiles[0] != this)
        //{
        //    StoreThisTile();
        //    m_memoryManager.SelectedTiles[1].RotateCard(180);
        //}

        //if (m_memoryManager.SelectedTiles.Count == 2)
        //{
        //    if (m_memoryManager.SelectedTiles[0].m_data.ID == m_memoryManager.SelectedTiles[1].m_data.ID)
        //    {
        //        m_memoryManager.SelectedTiles[0].Paired = true;
        //        m_memoryManager.SelectedTiles[1].Paired = true;
        //        m_memoryManager.SetScore(m_memoryManager.IncreaseNumber);
        //        Debug.Log("same");

        //    }
        //    else
        //    {
        //        //add delay
        //        m_memoryManager.SetScore(-1);
        //        StartCoroutine(RotateCard(m_memoryManager.SelectedTiles[0], m_memoryManager.SelectedTiles[1]));
        //        Debug.Log("different");
        //    }

        //    m_memoryManager.SelectedTiles.Clear();

        //}

        Test();

    }

    private void StoreThisTile()
    {
        GameManager.Instance.SoundEventManager.TriggerEvent(Enumerators.MusicEvents.PlaySoundPlayer, GameManager.Instance.SoundManager.SelectedTiled);
        if (m_memoryManager.SelectedTiles.Count == 2) m_memoryManager.SelectedTiles.Clear();
        m_memoryManager.SelectedTiles.Add(this);
    }

    //private IEnumerator RotateCard(MemoryWorldTile tileA, MemoryWorldTile tileB)
    //{
    //    m_memoryManager.CanChackTwoPair = false;
    //    //Debug.Log(m_memoryManager.CanChackTwoPair);
    //    yield return new WaitForSeconds(m_memoryManager.VelocityAfterTurnTheRotationTo0);
    //    tileA.RotateCard(180);
    //    tileB.RotateCard(180);
    //    m_memoryManager.SetScore(-1);
    //    m_memoryManager.CanChackTwoPair = true;
    //    //Debug.Log(m_memoryManager.CanChackTwoPair);
    //}


    //private void test()
    //{
    //    Debug.Log(m_memoryManager.SelectedTiles.Count);
    //    if (!m_memoryManager.GameTriggered) return;
    //    if (Paired) return;
    //    if (!m_memoryManager.CanChackTwoPair) return;

    //    if (m_memoryManager.SelectedTiles.Count == 0)
    //    {
    //        StoreThisTile();
    //        RotateCard(180);
    //    }
    //    else if (m_memoryManager.SelectedTiles.Count == 1 && m_memoryManager.SelectedTiles[0] != this)
    //    {
    //        StoreThisTile();
    //        RotateCard(180);
    //        if (m_memoryManager.SelectedTiles[0].m_data.ID == m_memoryManager.SelectedTiles[1].m_data.ID)
    //        {
    //            m_memoryManager.SelectedTiles[0].Paired = true;
    //            m_memoryManager.SelectedTiles[1].Paired = true;
    //            m_memoryManager.SetScore(m_memoryManager.IncreaseNumber);
    //        }
    //        else StartCoroutine(NotMatchedTiles(m_memoryManager.SelectedTiles[0], m_memoryManager.SelectedTiles[1]));

    //        m_memoryManager.SelectedTiles.Clear();
    //    }
    //}

    private IEnumerator NotMatchedTiles(MemoryWorldTile tileA, MemoryWorldTile tileB)
    {
        //GameManager.Instance.SoundEventManager.TriggerEvent(Enumerators.MusicEvents.PlaySoundPlayer, GameManager.Instance.SoundManager.NoPair);
        yield return new WaitForSeconds(m_memoryManager.VelocityAfterTurnTheRotationTo0);
        tileA.RotateCard(true);
        tileB.RotateCard(true);
        m_memoryManager.SetScore(-1);
        m_memoryManager.CanChackTwoPair = true;
    }


    private void Test()
    {
        if (!m_memoryManager.GameTriggered) return;
        if (Paired) return;
        if (!m_memoryManager.CanChackTwoPair) return;

        if (m_memoryManager.SelectedTiles.Count == 0)
        {
            StoreThisTile();
            StartCoroutine(Rotate(m_memoryManager.SelectedTiles[0], false));
        }
        else if (m_memoryManager.SelectedTiles.Count == 1 && m_memoryManager.SelectedTiles[0] != this)
        {
            StoreThisTile();
            StartCoroutine(Rotate(m_memoryManager.SelectedTiles[1], false));

            if (m_memoryManager.SelectedTiles[0].m_data.ID == m_memoryManager.SelectedTiles[1].m_data.ID)
            {
                m_memoryManager.SelectedTiles[0].Paired = true;
                m_memoryManager.SelectedTiles[1].Paired = true;
                m_memoryManager.SetScore(m_memoryManager.IncreaseNumber);
                m_memoryManager.CanChackTwoPair = true;
                //GameManager.Instance.SoundEventManager.TriggerEvent(Enumerators.MusicEvents.PlaySoundPlayer, GameManager.Instance.SoundManager.Pair);
            }
            else
            {
                m_memoryManager.CanChackTwoPair = false;
                StartCoroutine(NotMatchedTiles(m_memoryManager.SelectedTiles[0], m_memoryManager.SelectedTiles[1]));

            }
            m_memoryManager.SelectedTiles.Clear();
        }
    }


    private IEnumerator Rotate(MemoryWorldTile tileA, bool canRotate)
    {
        m_memoryManager.CanChackTwoPair = false;
        tileA.RotateCard(canRotate);
        yield return new WaitForSeconds(m_memoryManager.VelocityAfterTurnTheRotationTo0);
        if(m_memoryManager.SelectedTiles.Count == 1) m_memoryManager.CanChackTwoPair = true;
        
    }



}
