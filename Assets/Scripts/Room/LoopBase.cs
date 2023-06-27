using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LoopBase : State<Enumerators.RoomState>
{
    public LoopBase(Enumerators.RoomState stateID, StatesMachine<Enumerators.RoomState> stateMachine = null) : base(stateID, stateMachine)
    {
    }

}
