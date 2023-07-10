using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopOne : LoopBase
{
    public LoopOne(Enumerators.RoomState stateID, StatesMachine<Enumerators.RoomState> stateManager = null) : base(stateID, stateManager) { }

    public override void OnEnter()
    {
        base.OnEnter();
        RoomManager.LoopCounter = 1;

        //Radio.CanInteract = true; //per adesso qui poi chiedo ai designer dove
        //GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.EnableRadio); //per adesso qui poi chiedo ai designer dove

        // set active true darts
        GameManager.Instance.RoomManager.Puzzles[0].SetActive(true);
        //set active true walkman
        GameManager.Instance.RoomManager.Items[0].SetActive(true);
    }

    public override void OnExit()
    {
        base.OnExit();

        // set active false darts
        GameManager.Instance.RoomManager.Puzzles[0].SetActive(false);

        //remove walkman from inventory
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.RemoveWalkman);

        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.DisableDials);
    }

}
