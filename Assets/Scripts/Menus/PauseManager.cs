using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseUI;
    public Button ResumeButton;
    public Button ExitButton;

    //private PlayerInput m_inputs;

    private void Awake()
    {
        //m_inputs = new();
        //m_inputs.UI.Pause.performed += OnPause;
        //m_inputs.Enable();
        GameManager.Instance.EventManager.Registrer(Enumerators.Events.ResumeGame, OnResume);
        GameManager.Instance.EventManager.Registrer(Enumerators.Events.PauseGame, OnPause);
    }

    public void OnResume()
    {
        PauseUI.SetActive(false);
        GameManager.Instance.Player.PlayerStateMachine.ChangeState(Enumerators.PlayerState.Navigation);
        //if (Camera.main.transform.localPosition.z == 0f) //meaning: if it false it mean that player is running a puzzles so the player controller needs to rest disabled 
        //    GameManager.Instance.Player.PlayerController.EnableController();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        PauseUI.SetActive(true);
        GameManager.Instance.Player.PlayerController.DisableController();
    }

    public void OnPause()
    {
        PauseUI.SetActive(true);
    }

    public void OnExit() => Application.Quit();

}
