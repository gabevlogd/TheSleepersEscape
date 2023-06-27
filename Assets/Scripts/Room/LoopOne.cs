using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopOne : LoopBase
{
    public LoopOne(Enumerators.RoomState stateID, StatesMachine<Enumerators.RoomState> stateManager = null) : base(stateID, stateManager) { }

    public override void OnEnter()
    {
        base.OnEnter();

        // set active true darts
        GameManager.Instance.RoomManager.Puzzles[0].SetActive(true);
    }

    public override void OnExit()
    {
        base.OnExit();

        // set active false darts
        GameManager.Instance.RoomManager.Puzzles[0].SetActive(false);
    }

}
