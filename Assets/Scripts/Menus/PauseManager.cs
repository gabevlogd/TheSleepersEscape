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

    private PlayerInput m_inputs;

    private void Awake()
    {
        m_inputs = new();
        m_inputs.UI.Pause.performed += OnPause;
        m_inputs.Enable();
    }

    public void OnResume()
    {
        PauseUI.SetActive(false);
        GameManager.Instance.Player.PlayerController.EnableController();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        PauseUI.SetActive(true);
        GameManager.Instance.Player.PlayerController.DisableController();
    }

    public void OnExit() => Application.Quit();

}
