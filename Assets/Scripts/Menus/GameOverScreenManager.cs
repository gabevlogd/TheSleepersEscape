using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreenManager : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject LoseScreen;

    public Image FadeEffect;
    public float FadeSpeed;

    private bool m_canFade;
    private int m_fadeDirection;
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
        m_fadeColor = FadeEffect.color;
        if (m_fadeColor.a <= 0)
        {
            m_fadeDirection = 1;
            m_fadeColor.a = 0;
        }
        else if (m_fadeColor.a >= 1)
        {
            m_fadeDirection = -1;
            m_fadeColor.a = 1;
        }
        else return;

        m_canFade = true;
    }

    private void HandleFade()
    {
        if (m_canFade)
        {
            m_fadeColor.a += Time.deltaTime * FadeSpeed * m_fadeDirection;
            FadeEffect.color = m_fadeColor;

            if (m_fadeColor.a <= 0f || m_fadeColor.a >= 1f) m_canFade = false;
        }
    }
}
