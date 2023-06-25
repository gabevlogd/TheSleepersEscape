using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopThree : LoopBase
{
    public LoopThree(Enumerators.RoomState stateID, StatesMachine<Enumerators.RoomState> stateManager = null) : base(stateID, stateManager)
    {
    }
}
