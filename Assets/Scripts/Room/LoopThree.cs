using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopThree : LoopBase
{
    public LoopThree(Enumerators.RoomState stateID, StatesMachine<Enumerators.RoomState> stateManager = null) : base(stateID, stateManager)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        RoomManager.LoopCounter++;
        Radio.CanInteract = true; //per adesso qui poi chiedo ai designer dove
        GameManager.Instance.RoomManager.Puzzles[2].SetActive(true);

        LightsManager.GameTriggered = true; //qui solo per il debug, va dopo aver messo l'ultima comobinazione nella porta
    }

    public override void OnExit()
    {
        base.OnExit();

        GameManager.Instance.RoomManager.Puzzles[2].SetActive(false);

        LightsManager.GameTriggered = false; //qui solo per il debug, va dopo aver finito il gioco delle luci
    }

    
}
