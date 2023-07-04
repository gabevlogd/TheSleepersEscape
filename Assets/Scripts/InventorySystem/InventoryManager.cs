using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InventoryManager : MonoBehaviour
{
    public Canvas InventoryCanvas;
    public InventoryData InventoryData;
    private ItemsWheel m_itemsWheel;

    private void Awake()
    {
        InventoryData.Inputs = new();
        InventoryData.Inputs.Enable();
        InventoryData.Inputs.UI.ToggleInventory.performed += ToggleInventory;

        m_itemsWheel = new(ref InventoryData);
    }

    private void OnEnable()
    {
        m_itemsWheel.UpdateWheel(); //just for debug
    }


    private void Update()
    {
        m_itemsWheel.HandleWheelRotation();
    }


    private void ToggleInventory(InputAction.CallbackContext context)
    {
        if (Camera.main.transform.localPosition.z != 0f) return;

        InventoryCanvas.gameObject.SetActive(!InventoryCanvas.gameObject.activeInHierarchy);

        if (InventoryCanvas.gameObject.activeInHierarchy) GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.OpenInventory);
        else GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.CloseInventory);
    }
}
