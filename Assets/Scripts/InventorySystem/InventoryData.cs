using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct InventoryData 
{
    public Transform ItemsWheel;
    public List<GameObject> Items;
    public PlayerInput Inputs;
}
