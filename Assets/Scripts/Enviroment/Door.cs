using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator m_animator;
    private Vector3 m_closedPosition;

    private void Awake()
    {
        m_closedPosition = transform.position;
        m_animator = GetComponent<Animator>();
        GameManager.Instance.EventManager.Register(Enumerators.Events.OpenDoor, OpenDoor);
        GameManager.Instance.EventManager.Register(Enumerators.Events.CloseDoor, CloseDoor);
    }

    public void OpenDoor()
    {
        Debug.Log("Door opening");
        m_animator.Play("OpenDoor");
    }

    public void CloseDoor()
    {
        Debug.Log("Door closing");
        m_animator.Play("Closed");
        transform.position = m_closedPosition;
    }
}
