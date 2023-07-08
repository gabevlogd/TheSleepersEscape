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
        //InventoryData.Inputs.UI.ToggleInventory.performed += ToggleInventory;
        GameManager.Instance.EventManager.Register(Enumerators.Events.OpenInventory, ToggleInventory);
        GameManager.Instance.EventManager.Register(Enumerators.Events.CloseInventory, ToggleInventory);

        m_itemsWheel = new(ref InventoryData);
    }


    private void Update()
    {
        m_itemsWheel.HandleWheelRotation();
    }

    public void ToggleInventory() => InventoryCanvas.gameObject.SetActive(!InventoryCanvas.gameObject.activeInHierarchy);
}
