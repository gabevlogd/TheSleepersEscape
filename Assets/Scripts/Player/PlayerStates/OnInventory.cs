using UnityEditor;
using UnityEngine;

public class OnInventory : PlayerState
{
    private PlayerInput m_inputs;

    public OnInventory(Enumerators.PlayerState stateID, StatesMachine<Enumerators.PlayerState> stateMachine = null) : base(stateID, stateMachine)
    {
        m_inputs = new();
    }

    public override void OnEnter()
    {
        base.OnEnter();

        m_inputs.Enable();
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.OpenInventory);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        HandleInventoryClosing();
    }

    public override void OnExit()
    {
        base.OnExit();

        m_inputs.Disable();
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.CloseInventory);
    }

    private void HandleInventoryClosing()
    {
        if (m_inputs.UI.ToggleInventory.WasReleasedThisFrame())
        {
            if (m_stateMachine.PreviousState == null || m_stateMachine.PreviousState.StateID == Enumerators.PlayerState.OnTutorial)
            {
                OnTutorial.OnTutorialTwo = true;
                //m_stateMachine.ChangeState(Enumerators.PlayerState.OnTutorial);
                GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.CloseTutorial);
            }
            /*else */m_stateMachine.ChangeState(Enumerators.PlayerState.Navigation);
        }
            
    }


}