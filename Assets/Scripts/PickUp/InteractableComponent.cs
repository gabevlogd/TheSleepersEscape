using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableComponent : MonoBehaviour
{

    private List<ItemBase> m_interactablesSpotted = new List<ItemBase>();

    public List<ItemData> ItemBases = new List<ItemData>();

    private PlayerInput m_inputs;

    private void Awake()
    {
        m_inputs = new();
        m_inputs.Enable();
    }


    private void Update()
    {
        if (m_inputs.Selections.PickUp.WasReleasedThisFrame() && m_interactablesSpotted.Count > 0)
        {
            PickUp();

        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ItemBase interactable))
        {
            Debug.Log("item");
            Spotted(interactable);
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ItemBase interactable))
        {
            Debug.Log("itemNo");
            Unspotted(interactable);

        }
    }

    private void Spotted(ItemBase interactable)
    {
        m_interactablesSpotted.Add(interactable);
        
    }

    private void Unspotted(ItemBase interactable)
    {
        if (!m_interactablesSpotted.Contains(interactable)) return;
        m_interactablesSpotted.Remove(interactable);
        m_interactablesSpotted.TrimExcess();
        
    }


    private void PickUp()
    {
        ItemBases.Add(m_interactablesSpotted[0].data);
        Destroy(m_interactablesSpotted[0].gameObject, 0.2f);
        Unspotted(m_interactablesSpotted[0]);
    }

}
