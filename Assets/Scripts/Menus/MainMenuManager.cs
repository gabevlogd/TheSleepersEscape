using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainTab;
    public GameObject CreditsTab;

    public Button PlayButton;
    public Button ExitButton;

    private void Awake() => ShowMainTab();
    

    public void OnPlay() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    public void OnExit() => Application.Quit();

    public void ShowCreditsTab()
    {
        CreditsTab.SetActive(true);
        HideMainTab();
    }

    public void HideCreditsTab()
    {
        CreditsTab.SetActive(false);
    }

    public void ShowMainTab()
    {
        MainTab.SetActive(true);
        HideCreditsTab();
    }

    public void HideMainTab()
    {
        MainTab.SetActive(false);
    }



}
