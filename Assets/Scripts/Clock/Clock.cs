using System;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    public float Timer;// LAST MINUTE
    public TextMeshPro TimerText;
    private bool m_timerOn;
    private bool m_gameOver;

    // Start is called before the first frame update
    void Start()
    {
        if (TimerText != null)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(Timer);
            TimerText.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        }

        m_timerOn = true;
    }

    private void Update()
    {
        TimerFunction();
    }

    private void TimerFunction()
    {
        if (m_timerOn && !Door.IsOpen)
        {
            if (GameManager.Instance.Player.PlayerStateMachine.CurrentState.StateID != Enumerators.PlayerState.OnPause)
                Timer -= Time.deltaTime;

            if (Timer <= 8f && !m_gameOver)
            {
                m_gameOver = true;
                GameManager.PlayerWin = false;
                GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.ResumeGame);
                GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.GameOver);
            }

            if (Timer <= 0)
            {
                m_timerOn = false;
                Timer = 0;
            }

            if (TimerText != null)
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(Timer);
                TimerText.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
            }
        }



    }
}
