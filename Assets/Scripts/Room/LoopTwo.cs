using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopTwo : LoopBase
{
    public LoopTwo(Enumerators.RoomState stateID, StatesMachine<Enumerators.RoomState> stateManager = null) : base(stateID, stateManager)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        GameManager.instance.RoomManager.Puzzles[0].SetActive(true);
    }

    public override void OnExit()
    {
        base.OnExit();
        GameManager.instance.RoomManager.Puzzles[0].SetActive(false);
    }
}
