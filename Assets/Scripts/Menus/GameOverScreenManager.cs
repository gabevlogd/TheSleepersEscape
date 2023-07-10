using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreenManager : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject LoseScreen;

    public Image FadeEffect;
    public float FadeSpeed;

    private bool m_canFade;
    private Color m_fadeColor;

    private void Awake()
    {
        if (GameManager.PlayerWin) WinScreen.SetActive(true);
        else LoseScreen.SetActive(true);

        StartFade();
    }

    private void Update() => HandleFade();

    private void StartFade()
    {
        if (GameManager.PlayerWin) m_fadeColor = new Color(1f, 1f, 1f, 1f);
        else m_fadeColor = new Color(0f, 0f, 0f, 1f);

        m_canFade = true;
    }

    private void HandleFade()
    {
        if (m_canFade)
        {
            m_fadeColor.a -= Time.deltaTime * FadeSpeed;
            FadeEffect.color = m_fadeColor;

            if (m_fadeColor.a >= 1f) m_canFade = false;
        }
    }

    public void BackToMenu() => SceneManager.LoadScene("MainMenu");
}
