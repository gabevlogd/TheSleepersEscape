using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemsDetector : MonoBehaviour
{


    public List<ItemData> ItemsDatas = new List<ItemData>();

    private List<ItemBase> m_interactablesSpotted = new List<ItemBase>();
    private PlayerInput m_inputs;

    private void Awake()
    {
        m_inputs = new();
        m_inputs.Enable();
    }


    private void Update() => TryPickUp();



    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ItemBase interactable))
        {
            //Debug.Log("item");
            Spotted(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ItemBase interactable))
        {
            //Debug.Log("itemNo");
            Unspotted(interactable);
        }
    }


    private void Spotted(ItemBase interactable) => m_interactablesSpotted.Add(interactable);

    private void Unspotted(ItemBase interactable)
    {
        if (!m_interactablesSpotted.Contains(interactable)) return;
        m_interactablesSpotted.Remove(interactable);
        m_interactablesSpotted.TrimExcess();
        
    }


    private void PickUp()
    {
        
        ItemsDatas.Add(m_interactablesSpotted[0].data);
        Destroy(m_interactablesSpotted[0].gameObject, 0.2f);
        Unspotted(m_interactablesSpotted[0]);
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.ItemCollected);
    }

    private void TryPickUp()
    {
        if (GameManager.Instance.Player.PlayerStateMachine.CurrentState.StateID != Enumerators.PlayerState.Navigation) return;

        if (m_inputs.Selections.PickUp.WasReleasedThisFrame() && m_interactablesSpotted.Count > 0)
        {
            if (GameManager.Instance.Player.PlayerStateMachine.CurrentState.StateID != Enumerators.PlayerState.Navigation) return;

            if (m_interactablesSpotted[0].data.ItemName == "Walkman")
            {
                GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.PlayWalkman);
                GameManager.Instance.SoundEventManager.TriggerEvent(Enumerators.MusicEvents.PlaySoundEnv, GameManager.Instance.SoundManager.WalkmanPlay);
            }
            else if (m_interactablesSpotted[0].data.ItemName == "Note 1")
            {
                GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.EnableRadio);
                GameManager.Instance.SoundEventManager.TriggerEvent(Enumerators.MusicEvents.PlaySoundEnv, GameManager.Instance.SoundManager.PickUp);
            }
            else if (m_interactablesSpotted[0].data.ItemName == "Photo") GameManager.Instance.SoundEventManager.TriggerEvent(Enumerators.MusicEvents.PlaySoundEnv, GameManager.Instance.SoundManager.PickUp);
            else
            {
                GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.EnableDials);
                GameManager.Instance.SoundEventManager.TriggerEvent(Enumerators.MusicEvents.PlaySoundEnv, GameManager.Instance.SoundManager.PickUp);
            }

            PickUp();
        }
    }

}
