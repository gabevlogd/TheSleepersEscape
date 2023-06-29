using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Button PlayButton;
    public Button ExitButton;


    public void OnPlay() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    public void OnExit() => Application.Quit();
}
