using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : State<Enumerators.PlayerState>
{
    public PlayerState(Enumerators.PlayerState stateID, StatesMachine<Enumerators.PlayerState> stateMachine = null) : base(stateID, stateMachine)
    {
    }
}
