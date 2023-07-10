using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseUI;
    public GameObject Main;
    public GameObject Settings;

    public Button ResumeButton;
    public Button SettingsButton;
    public Button BackButton;
    public Button ExitButton;

    public Slider MouseSensSlider;
    public TextMeshProUGUI MouseSensText;
    public static float MouseSens;


    private void Awake()
    {
        GameManager.Instance.EventManager.Register(Enumerators.Events.ResumeGame, OnResume);
        GameManager.Instance.EventManager.Register(Enumerators.Events.PauseGame, OnPause);

        OpenMainScreen();
        CloseSettingsScreen();
        ClosePauseScreen();
    }

    public void OnResume()
    {
        if (Settings.activeInHierarchy) CloseSettingsScreen();
        if (!Main.activeInHierarchy) OpenMainScreen();

        ClosePauseScreen();
        if (GameManager.Instance.Player.PlayerStateMachine.PreviousState == null || GameManager.Instance.Player.PlayerStateMachine.PreviousState.StateID == Enumerators.PlayerState.OnTutorial) 
            GameManager.Instance.Player.PlayerStateMachine.ChangeState(Enumerators.PlayerState.OnTutorial);
        else 
            GameManager.Instance.Player.PlayerStateMachine.ChangeState(Enumerators.PlayerState.Navigation);
    }

    public void OnSettings()
    {
        CloseMainScreen();
        OpenSettingsScreen();
    }

    public void OnBack()
    {
        CloseSettingsScreen();
        OpenMainScreen();
    }

    public void OnSlideMouseSens()
    {
        MouseSens = MouseSensSlider.value;
        MouseSensText.text = "Mouse Sensibility: " + MouseSens;
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.UpdateSettings);
    }



    public void OnPause() => OpenPauseScreen();
    public void OnExit() => SceneManager.LoadScene("MainMenu");



    private void OpenPauseScreen() => PauseUI.SetActive(true);
    private void ClosePauseScreen() => PauseUI.SetActive(false);

    private void OpenMainScreen() => Main.SetActive(true);
    private void CloseMainScreen() => Main.SetActive(false);

    private void OpenSettingsScreen() => Settings.SetActive(true);
    private void CloseSettingsScreen() => Settings.SetActive(false);

}
