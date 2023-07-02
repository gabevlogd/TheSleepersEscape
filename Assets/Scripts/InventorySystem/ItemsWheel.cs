using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemsWheel
{
    private List<Vector3> m_itemsPositions;
    private InventoryData m_inventoryData;

    private Transform m_transform;
    private Quaternion m_defaultRotation;
    private Quaternion m_targetRotation;

    private float m_angularSpeed = 100f;

    public ItemsWheel(ref InventoryData inventoryData)
    {
        m_inventoryData = inventoryData;
        m_transform = m_inventoryData.ItemsWheel;

        m_defaultRotation = m_transform.rotation;
        m_targetRotation = m_defaultRotation;

        GameManager.Instance.EventManager.Registrer(Enumerators.Events.OpenInventory, Enable);
        GameManager.Instance.EventManager.Registrer(Enumerators.Events.CloseInventory, Disable);
        
    }

    public void Enable() => m_inventoryData.Inputs.UI.SlideItems.performed += SlideItems;
    public void Disable() => m_inventoryData.Inputs.UI.SlideItems.performed -= SlideItems;


    public void SlideItems(InputAction.CallbackContext context)
    {
        if (m_targetRotation != m_transform.rotation) return;
        Debug.Log("SlideItem");

        float slideDirection = context.ReadValue<float>();
        m_targetRotation = m_transform.rotation * Quaternion.Euler(0f, GetAngle() * slideDirection, 0f);
    }

    public void HandleWheelRotation()
    {
        if (m_targetRotation == m_transform.rotation) return;

        m_transform.rotation = Quaternion.RotateTowards(m_transform.rotation, m_targetRotation, Time.deltaTime * m_angularSpeed);

        if (Mathf.Abs(Quaternion.Dot(m_transform.rotation, m_targetRotation) - 1f) <= 0.0001f)
        {
            m_transform.rotation = m_targetRotation;
        }
    }



    private float GetAngle() => (360f / m_inventoryData.Items.Count);

    private List<Vector3> GetPositions(int itemsCount)
    {
        float theta = 0;
        float angularIncrement = GetAngle() * Mathf.Deg2Rad;
        List<Vector3> itemsPositions = new();

        for (int i = 0; i < itemsCount; i++)
        {
            itemsPositions.Add(new Vector3(Mathf.Cos(theta), 0f, Mathf.Sin(theta)));
            theta += angularIncrement;
        }
        return itemsPositions;
    }

    private void PlaceItems()
    {
        for (int i = 0; i < m_inventoryData.Items.Count; i++)
        {
            m_inventoryData.Items[i].transform.localPosition = m_itemsPositions[i];
        }
    }

    public void UpdateWheel()
    {
        m_transform.rotation = m_defaultRotation * Quaternion.Euler(0f, 90f, 0f);
        m_targetRotation = m_transform.rotation;
        m_itemsPositions = GetPositions(m_inventoryData.Items.Count);
        PlaceItems();
    }




    
}
