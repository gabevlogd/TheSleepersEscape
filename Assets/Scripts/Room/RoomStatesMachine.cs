using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStatesMachine : StatesMachine<Enumerators.RoomState>
{
    protected override void InitStates()
    {
        AllStates.Add(Enumerators.RoomState.LoopOne, new LoopOne(Enumerators.RoomState.LoopOne, this));
        AllStates.Add(Enumerators.RoomState.LoopTwo, new LoopTwo(Enumerators.RoomState.LoopTwo, this));
        AllStates.Add(Enumerators.RoomState.LoopThree, new LoopThree(Enumerators.RoomState.LoopThree, this));
    }
}
