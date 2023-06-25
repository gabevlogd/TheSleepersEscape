using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopOne : LoopBase
{
    public LoopOne(Enumerators.RoomState stateID, StatesMachine<Enumerators.RoomState> stateManager = null) : base(stateID, stateManager) { }

    
}
