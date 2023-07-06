using UnityEditor;
using UnityEngine;

public class OnDialogue : PlayerState
{
    public OnDialogue(Enumerators.PlayerState stateID, StatesMachine<Enumerators.PlayerState> stateMachine = null) : base(stateID, stateMachine)
    {
        GameManager.Instance.EventManager.Registrer(Enumerators.Events.StartDialogue, StartDialogue);
        GameManager.Instance.EventManager.Registrer(Enumerators.Events.StopDialogue, StopDialogue);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        GameManager.Instance.Player.PlayerController.EnableController();
    }

    public void StartDialogue()
    {
        m_stateMachine.ChangeState(Enumerators.PlayerState.OnDialogue);
    }

    public void StopDialogue()
    {
        m_stateMachine.ChangeState(Enumerators.PlayerState.Navigation);
    }


}