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
        RoomManager.LoopCounter++;

        GameManager.Instance.RoomManager.Puzzles[1].SetActive(true);
    }

    public override void OnExit()
    {
        base.OnExit();

        GameManager.Instance.RoomManager.Puzzles[1].SetActive(false);
    }

}
