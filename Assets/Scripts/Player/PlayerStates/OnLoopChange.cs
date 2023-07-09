using System.Collections;
using UnityEngine;

public class OnLoopChange : PlayerState
{
    public OnLoopChange(Enumerators.PlayerState stateID, StatesMachine<Enumerators.PlayerState> stateMachine = null) : base(stateID, stateMachine)
    {
        GameManager.Instance.EventManager.Register(Enumerators.Events.EnterLoopChange, EnterState);
    }


    public void EnterState() => m_stateMachine.ChangeState(Enumerators.PlayerState.OnLoopChange);
}

