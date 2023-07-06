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

        //Radio.CanInteract = true; //per adesso qui poi chiedo ai designer dove
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.EnableRadio); //per adesso qui poi chiedo ai designer dove

        GameManager.Instance.RoomManager.Puzzles[1].SetActive(true);
    }

    public override void OnExit()
    {
        base.OnExit();

        GameManager.Instance.RoomManager.Puzzles[1].SetActive(false);
    }

}
