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
        RoomManager.LoopCounter = 2;

        //Radio.CanInteract = true; //per adesso qui poi chiedo ai designer dove
        //GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.EnableRadio); //per adesso qui poi chiedo ai designer dove

        GameManager.Instance.RoomManager.Puzzles[1].SetActive(true); //set active true game of 15
        GameManager.Instance.RoomManager.GameObjects[0].SetActive(true); //set true active cigar box
        GameManager.Instance.RoomManager.GameObjects[1].SetActive(true); //set true active clock
    }

    public override void OnExit()
    {
        base.OnExit();

        GameManager.Instance.RoomManager.Puzzles[1].SetActive(false); //set active false game of 15
        GameManager.Instance.RoomManager.GameObjects[0].SetActive(false); //set active false cigar box
        GameManager.Instance.RoomManager.GameObjects[1].SetActive(false); //set active false clock
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.DisableDials);
    }

}
