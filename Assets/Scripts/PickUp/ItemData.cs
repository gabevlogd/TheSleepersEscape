using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "PickUp/CreateItem", order = 0)]
public class ItemData : ScriptableObject
{

    [SerializeField] private string m_itemName;
    [SerializeField] private int m_id;
    [SerializeField] private string m_description;
    [SerializeField] private GameObject m_itemMesh;

    public string Description { get => m_description; }
    public string ItemName { get => m_itemName; }
    public GameObject ItemMesh { get => m_itemMesh; }
}
