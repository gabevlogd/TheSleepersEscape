using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct InventoryData 
{
    public Transform ItemsWheel;
    public List<ItemBase> Items;
    public PlayerInput Inputs;
    public Camera InventoryCamera;

}
