using System.Collections;
using UnityEngine;

public class RunningInteractable : PlayerState
{
    public RunningInteractable(Enumerators.PlayerState stateID, StatesMachine<Enumerators.PlayerState> stateMachine = null) : base(stateID, stateMachine)
    {
    }
}