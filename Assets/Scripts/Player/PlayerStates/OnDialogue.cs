using UnityEditor;
using UnityEngine;

public class OnDialogue : PlayerState
{
    public OnDialogue(Enumerators.PlayerState stateID, StatesMachine<Enumerators.PlayerState> stateMachine = null) : base(stateID, stateMachine)
    {
    }
}