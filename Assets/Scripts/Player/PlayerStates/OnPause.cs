using System.Collections;
using UnityEngine;

public class OnPause : PlayerState
{
    private PlayerInput m_inputs;

    public OnPause(Enumerators.PlayerState stateID, StatesMachine<Enumerators.PlayerState> stateMachine = null) : base(stateID, stateMachine)
    {
        m_inputs = new();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        m_inputs.Enable();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        HandleResumeRequest();
    }

    public override void OnExit()
    {
        base.OnExit();
        m_inputs.Disable();
    }

    private void HandleResumeRequest()
    {
        if (m_inputs.UI.Pause.WasReleasedThisFrame())
        {
            //m_stateMachine.ChangeState(Enumerators.PlayerState.Navigation);
            GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.ResumeGame);
        }
    }
}