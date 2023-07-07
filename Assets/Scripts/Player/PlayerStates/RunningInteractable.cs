using System.Collections;
using UnityEngine;

public class RunningInteractable : PlayerState
{
    private PlayerInput m_inputs;

    public RunningInteractable(Enumerators.PlayerState stateID, StatesMachine<Enumerators.PlayerState> stateMachine = null) : base(stateID, stateMachine)
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
        HandleInteractionEnd();
    }

    public override void OnExit()
    {
        base.OnExit();
        m_inputs.Disable();
    }

    private void HandleInteractionEnd()
    {
        if (m_inputs.Selections.Unselect.WasReleasedThisFrame())
        {
            m_stateMachine.ChangeState(Enumerators.PlayerState.Navigation);
            GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.StopInteraction);
        }
    }
}