using UnityEngine;

public class PocketWatch : MonoBehaviour
{
    public Transform MinutesHand;
    public Transform SecondsHand;

    [Min(1)]
    public int TimerStartingMinutes;
    [Range(1, 30)]
    public float speedForDebug;

    private bool m_performTimer;
    private float m_timer;
    private int m_leftMinutes;
    private int m_leftSeconds;
    private float m_SecondsRotationAngle = 6f;
    private float m_MinutesRotationAngle = 30f;

    private void OnEnable()
    {
        GameManager.Instance.EventManager.Register(Enumerators.Events.StartPuzzle, StartTimer);
        GameManager.Instance.EventManager.Register(Enumerators.Events.ResetPuzzle, StopTimer);
    }

    private void Update() => UpdatePocketClock();

    public void StartTimer()
    {
        m_performTimer = true;
        m_timer = TimerStartingMinutes * 60f;
        m_leftMinutes = Mathf.FloorToInt(m_timer / 60f);
        m_leftSeconds = (int)m_timer;

        MinutesHand.localRotation *= Quaternion.Euler(0f, 0f, m_leftMinutes * m_MinutesRotationAngle);
        SecondsHand.localRotation *= Quaternion.Euler(0f, 0f, m_leftSeconds * m_SecondsRotationAngle);
    }

    public void StopTimer()
    {
        m_performTimer = false;
        //HoursHand.localRotation = Quaternion.identity;
        MinutesHand.localRotation = Quaternion.identity;
        SecondsHand.localRotation = Quaternion.identity;
    }

    private void UpdateTimer()
    {
        m_timer -= Time.deltaTime * speedForDebug;
        if (m_timer <= 0f) StopTimer();
    }

    private void UpdateMinutesHand()
    {
        if (Mathf.FloorToInt(m_timer / 60f) != m_leftMinutes - 1)
        {
            m_leftMinutes--;
            MinutesHand.localRotation = Quaternion.identity * Quaternion.Euler(0f, 0f, m_leftMinutes * m_MinutesRotationAngle);
        }
    }

    private void UpdateSecondsHand()
    {
        if (Mathf.FloorToInt(m_timer) != m_leftSeconds)
        {
            m_leftSeconds--;
            SecondsHand.localRotation = Quaternion.identity * Quaternion.Euler(0f, 0f, m_leftSeconds * m_SecondsRotationAngle);
        }
    }

    private void UpdatePocketClock()
    {
        if (m_performTimer)
        {
            UpdateTimer();
            UpdateSecondsHand();
            UpdateMinutesHand();
        }
    }
}
