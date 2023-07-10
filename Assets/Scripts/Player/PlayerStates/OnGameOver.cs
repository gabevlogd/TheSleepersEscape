using System.Collections;
using UnityEngine;

public class OnGameOver : PlayerState
{
    public OnGameOver(Enumerators.PlayerState stateID, StatesMachine<Enumerators.PlayerState> stateMachine = null) : base(stateID, stateMachine)
    {
        GameManager.Instance.EventManager.Register(Enumerators.Events.GameOver, EnterGameOverState);
    }

    public void EnterGameOverState() => m_stateMachine.ChangeState(Enumerators.PlayerState.OnGameOver);
}