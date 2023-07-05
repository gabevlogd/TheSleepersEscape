using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : PlayerState
{
    public Navigation(Enumerators.PlayerState stateID, StatesMachine<Enumerators.PlayerState> stateMachine = null) : base(stateID, stateMachine)
    {
    }
}
