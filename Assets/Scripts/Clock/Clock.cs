using System;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    public float Timer;// LAST MINUTE
    public TextMeshPro TimerText;

    // Start is called before the first frame update
    void Start()
    {
        if (TimerText != null)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(Timer);
            TimerText.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        }
    }

    private void Update()
    {
        TimerFunction();
    }

    private void TimerFunction()
    {
        if (Timer != Mathf.Infinity)
        {
            Timer -= Time.deltaTime;

            if (Timer <= 0f)
            {
                Timer = 0f;
                GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.GameOver);
            }

            if (TimerText != null)
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(Timer);
                TimerText.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
            }
        }



    }
}
