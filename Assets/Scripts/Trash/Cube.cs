using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class Cube : MonoBehaviour, ISelectable
{
    //private delegate void Action<in T>(T obj);
    //private Action<float> m_moveSelectable;
    private float m_targetDepth;

    private void OnMouseDown()
    {
        //Debug.Log("OnMouseDown");
        PickObject();
    }

    private void OnMouseDrag()
    {
        //Debug.Log("OnMouseDrag");
        MoveObject(m_targetDepth);
    }


    public void PickObject()
    {
        //Debug.Log("PickObject");
        m_targetDepth = Vector3.Distance(Player.playerTransform.position, transform.position);

    }

    public void MoveObject(float targetDepth)
    {
        //Debug.Log("MoveObject");
        m_targetDepth = Mathf.Clamp(m_targetDepth + Input.mouseScrollDelta.y, 1f, 10f);
        Vector3 worldPosition = new Vector3(Camera.main.pixelWidth * 0.5f, Camera.main.pixelHeight * 0.5f, targetDepth);
        transform.position = Vector3.MoveTowards(transform.position, Camera.main.ScreenToWorldPoint(worldPosition), 5f);

        
    }



}
