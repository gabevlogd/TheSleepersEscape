using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPoint : MonoBehaviour
{
    public GameObject InteractablePoint;
    [Min(0.5f)]
    public float MinTriggerDistance;
    private Collider m_collider;

    private void Awake()
    {
        m_collider = GetComponent<Collider>();
    }

    private void OnMouseOver()
    {

        //if (Camera.main.transform.localPosition.z != 0f) return;
        //GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.ShowInteractablePoint);
        //GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.HideInteractablePoint);

        if (!InteractablePoint.gameObject.activeInHierarchy && Vector3.Distance(Camera.main.transform.position, m_collider.ClosestPoint(Camera.main.transform.position)) <= MinTriggerDistance)
        {
            InteractablePoint.gameObject.SetActive(true);
            //GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.ShowInteractablePoint);
            return;
        }
        else if (InteractablePoint.gameObject.activeInHierarchy && Vector3.Distance(Camera.main.transform.position, m_collider.ClosestPoint(Camera.main.transform.position)) > MinTriggerDistance)
        {
            InteractablePoint.gameObject.SetActive(false);
            //GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.HideInteractablePoint);
        }
    }

    private void OnMouseExit()
    {
        //if (Camera.main.transform.localPosition.z != 0f) return;

        InteractablePoint.gameObject.SetActive(false);
        //GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.HideInteractablePoint);
    }

    private void OnDisable()
    {
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.HideInteractablePoint);
    }


}
