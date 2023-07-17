using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class ItemsWheel
{
    private List<Vector3> m_itemsPositions;
    private InventoryData m_inventoryData;

    private Transform m_transform;
    private Quaternion m_defaultRotation;
    private Quaternion m_targetRotation;

    private Vector3 m_raycastDirection;
    public Vector3 LookAtDirection { get => m_raycastDirection; }

    private float m_angularSpeed = 150f;

    public ItemsWheel(ref InventoryData inventoryData)
    {
        m_inventoryData = inventoryData;
        m_transform = m_inventoryData.ItemsWheel;

        m_defaultRotation = m_transform.rotation;
        m_targetRotation = m_defaultRotation;

        GameManager.Instance.EventManager.Register(Enumerators.Events.OpenInventory, Enable);
        GameManager.Instance.EventManager.Register(Enumerators.Events.CloseInventory, Disable);
        GameManager.Instance.EventManager.Register(Enumerators.Events.ItemCollected, AddNewItem);
        GameManager.Instance.EventManager.Register(Enumerators.Events.RemoveWalkman, RemoveWalkman);
        GameManager.Instance.EventManager.Register(Enumerators.Events.PickUpNote, AddNote2);

        
    }

    public void Enable()
    {
        m_inventoryData.Inputs.UI.SlideItems.performed += SlideItems;
        if (m_inventoryData.Items.Count > 0)
            ShowSelectedItemInfo(m_inventoryData.Items[0].data.Description);
    }

    public void Disable() => m_inventoryData.Inputs.UI.SlideItems.performed -= SlideItems;


    public void SlideItems(InputAction.CallbackContext context)
    {
        if (m_inventoryData.Items.Count == 0) return;
        if (GameManager.Instance.Player.PlayerStateMachine.CurrentState.StateID != Enumerators.PlayerState.OnInventory) return;

        if (m_targetRotation != m_transform.rotation) return;
        //Debug.Log("SlideItem");

        float slideDirection = context.ReadValue<float>();
        m_targetRotation = m_transform.rotation * Quaternion.Euler(0f, GetAngle() * slideDirection, 0f);
    }

    public void HandleWheelRotation()
    {
        if (m_targetRotation == m_transform.rotation) return;
        ShowSelectedItemInfo("");
        m_transform.rotation = Quaternion.RotateTowards(m_transform.rotation, m_targetRotation, Time.deltaTime * m_angularSpeed);

        if (Mathf.Abs(Quaternion.Dot(m_transform.rotation, m_targetRotation) - 1f) <= 0.0001f)
        {
            m_transform.rotation = m_targetRotation;
            ShowSelectedItemInfo();
        }
    }

    public void ShowSelectedItemInfo(string value = null)
    {
        RaycastHit hitInfo;
        Physics.Raycast(m_transform.position, m_raycastDirection, out hitInfo);
        if (hitInfo.collider != null)
        {
            //Debug.Log(hitInfo.collider.name);
            string itemDescription = hitInfo.collider.GetComponent<ItemBase>().data.Description;
            GameManager.Instance.InventoryManager.SetItemDescription(itemDescription);
        }
        else GameManager.Instance.InventoryManager.SetItemDescription(value);
    }


    /// <summary>
    /// returns the angle between two successive positions on the invenotry wheel
    /// </summary>
    /// <returns></returns>
    private float GetAngle() => (360f / m_inventoryData.Items.Count);

    /// <summary>
    /// Gets all the correct positions on the inventory wheel where to place the items
    /// </summary>
    private List<Vector3> GetPositions(int itemsCount)
    {
        float theta = 0;
        float angularIncrement = GetAngle() * Mathf.Deg2Rad;
        List<Vector3> itemsPositions = new();

        for (int i = 0; i < itemsCount; i++)
        {
            itemsPositions.Add(new Vector3(Mathf.Cos(theta), 0f, Mathf.Sin(theta)) * 0.5f);
            theta += angularIncrement;
        }
        return itemsPositions;
    }

    /// <summary>
    /// Places the inventory items on the correct inventory wheel's positions
    /// </summary>
    private void PlaceItems()
    {
        for (int i = 0; i < m_inventoryData.Items.Count; i++)
        {
            m_inventoryData.Items[i].transform.localPosition = m_itemsPositions[i];
        }
    }

    /// <summary>
    /// Updates the showed items in the inventory wheel
    /// </summary>
    public void UpdateWheel()
    {
        m_transform.rotation = m_defaultRotation * Quaternion.Euler(0f, 90f, 0f);
        m_raycastDirection = m_transform.right;
        m_targetRotation = m_transform.rotation;
        m_itemsPositions = GetPositions(m_inventoryData.Items.Count);
        PlaceItems();
    }

    /// <summary>
    /// Adds new item to the inventory
    /// </summary>
    public void AddNewItem()
    {
        ItemData newItemData = GameManager.Instance.Player.ItemsDetector.ItemsDatas[0];

        GameObject newItem = MonoBehaviour.Instantiate(newItemData.ItemMesh, m_inventoryData.ItemsWheel);
        ItemBase newItemBase = newItem.AddComponent<ItemBase>();
        newItemBase.data = newItemData;

        newItemBase.gameObject.layer = newItemData.LayerUI;

        //ConstraintSource targetSource = new ConstraintSource();
        //targetSource.sourceTransform = m_inventoryData.InventoryCamera.transform;
        //newItemBase.GetComponent<LookAtConstraint>().SetSource(0, targetSource);
        newItemBase.GetComponent<LookAtComponent>().enabled = true;

        if (m_inventoryData.Items == null) m_inventoryData.Items = new List<ItemBase>();
        m_inventoryData.Items.Add(newItemBase);
        GameManager.Instance.Player.ItemsDetector.ItemsDatas.Remove(newItemData);

        UpdateWheel();
    }

    /// <summary>
    /// Remove walkman event
    /// </summary>
    public void RemoveWalkman()
    {
        //m_inventoryData.Items.Remove(m_inventoryData.Items[0]);
        UpdateWheel();
    }

    /// <summary>
    /// Add note 2 event
    /// </summary>
    public void AddNote2()
    {
        ItemData newItemData = GameManager.Instance.RoomManager.Items[3].GetComponent<ItemBase>().data;

        GameObject newItem = MonoBehaviour.Instantiate(newItemData.ItemMesh, m_inventoryData.ItemsWheel);
        ItemBase newItemBase = newItem.AddComponent<ItemBase>();
        newItemBase.data = newItemData;

        newItemBase.gameObject.layer = newItemData.LayerUI;

        //ConstraintSource targetSource = new ConstraintSource();
        //targetSource.sourceTransform = m_inventoryData.InventoryCamera.transform;
        //newItemBase.GetComponent<LookAtConstraint>().SetSource(0, targetSource);
        newItemBase.GetComponent<LookAtComponent>().enabled = true;

        if (m_inventoryData.Items == null) m_inventoryData.Items = new List<ItemBase>();
        m_inventoryData.Items.Add(newItemBase);
        GameManager.Instance.Player.ItemsDetector.ItemsDatas.Remove(newItemData);

        UpdateWheel();
    }




    
}
