using System.Collections;
using UnityEngine;

public class OnTutorial : PlayerState
{
    private PlayerInput m_inputs;
    private Vector3 m_pastPosition;
    public bool OnTutorialOne;

    public OnTutorial(Enumerators.PlayerState stateID, StatesMachine<Enumerators.PlayerState> stateMachine = null) : base(stateID, stateMachine)
    {
        m_inputs = new();
    }

    public override void OnEnter()
    {
        base.OnEnter();

        m_inputs.Enable();

        GameManager.Instance.Player.PlayerController.EnableController();

        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.ShowHud);
    }

    public override void OnUpdate()
    {

        base.OnUpdate();

        HandleStepsSFX();

        HandleInventoryOpening();
        HandlePauseMenu();

        CheckTutorialState();
    }

    public override void OnExit()
    {
        base.OnExit();

        m_inputs.Disable();

        GameManager.Instance.Player.PlayerController.DisableController();

        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.HideHud);
    }

    private void HandleInventoryOpening()
    {
        if (!OnTutorialOne) return;
        if (m_inputs.UI.ToggleInventory.WasReleasedThisFrame())
            m_stateMachine.ChangeState(Enumerators.PlayerState.OnInventory);
    }

    private void HandlePauseMenu()
    {
        if (m_inputs.UI.Pause.WasReleasedThisFrame())
        {
            m_stateMachine.ChangeState(Enumerators.PlayerState.OnPause);
            GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.PauseGame);
        }
    }

    private void CheckTutorialState()
    {
        if (m_inputs.Traslation.Forward.WasPerformedThisFrame() || m_inputs.Traslation.Lateral.WasPerformedThisFrame())
            GameManager.Instance.StartCoroutine(SetNextTutorial());
    }

    private void HandleStepsSFX()
    {
        if (IsMoving()) GameManager.Instance.SoundEventManager.TriggerEvent(Enumerators.MusicEvents.PlayStepsSound, GameManager.Instance.SoundManager.PlayerStep);
        else GameManager.Instance.SoundEventManager.TriggerEvent(Enumerators.MusicEvents.StopStepsSound, GameManager.Instance.SoundManager.PlayerStep);

    }

    private bool IsMoving()
    {
        if (m_pastPosition != GameManager.Instance.Player.transform.position)
        {
            m_pastPosition = GameManager.Instance.Player.transform.position;
            return true;
        }
        else return false;
    }

    private IEnumerator SetNextTutorial()
    {
        yield return new WaitForSecondsRealtime(2f);
        OnTutorialOne = true;
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.NextTutorial);

    }
}