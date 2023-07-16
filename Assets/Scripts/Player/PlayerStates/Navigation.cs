using UnityEngine;

public class Navigation : PlayerState
{
    private PlayerInput m_inputs;
    private Vector3 m_pastPosition;

    public Navigation(Enumerators.PlayerState stateID, StatesMachine<Enumerators.PlayerState> stateMachine = null) : base(stateID, stateMachine)
    {
        m_inputs = new();
        GameManager.Instance.EventManager.Register(Enumerators.Events.ResetPuzzle, EnterNavigationState);
        GameManager.Instance.EventManager.Register(Enumerators.Events.PuzzleCompleted, EnterNavigationState);
        GameManager.Instance.EventManager.Register(Enumerators.Events.OpenDoor, EnterNavigationState);
        GameManager.Instance.EventManager.Register(Enumerators.Events.ExitLoopChange, EnterNavigationState);

    }

    public override void OnEnter()
    {
        base.OnEnter();

        m_inputs.Enable();

        GameManager.Instance.Player.PlayerController.EnableController();
        GameManager.Instance.Player.CameraController.EnableController();

        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.ShowHud);

    }

    public override void OnUpdate()
    {
       
        base.OnUpdate();
        HandleStepsSFX();
        HandleInventoryOpening();
        HandlePauseMenu();
    }

    public override void OnExit()
    {
        base.OnExit();

        m_inputs.Disable();

        GameManager.Instance.Player.PlayerController.DisableController();
        GameManager.Instance.Player.CameraController.DisableController();

        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.HideHud);
    }

    private void HandleInventoryOpening()
    {
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

    public void EnterNavigationState() => m_stateMachine.ChangeState(Enumerators.PlayerState.Navigation);

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

}
