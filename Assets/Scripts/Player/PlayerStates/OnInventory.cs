using UnityEditor;
using UnityEngine;

public class OnInventory : PlayerState
{
    public OnInventory(Enumerators.PlayerState stateID, StatesMachine<Enumerators.PlayerState> stateMachine = null) : base(stateID, stateMachine)
    {
    }
}