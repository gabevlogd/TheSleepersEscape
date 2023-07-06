using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartsManager : MonoBehaviour
{

    [HideInInspector] public List<GameObject> m_throwedDarts;

    [HideInInspector] public bool m_gameTriggered;

    [HideInInspector] public bool ReadyToThrow;

    public int score;

    [SerializeField] private int m_pointsForWin;

    public int totalThrows;

    [HideInInspector] public int initialThrows;


    private void Awake()
    {
        initialThrows = totalThrows;
    }

    public IEnumerator EnableThrowAbility() //not needed anymore, problem fixed in the camera controller
    {
        yield return new WaitForSeconds(1f);
        ReadyToThrow = true;
    }
    public void SetScore(int value)
    {
        score += value;
        CheckWinCondition();
        //Debug.Log(m_score + " " + value);
    }

    private void CheckWinCondition()
    {
        if (score == m_pointsForWin)
        {
            Debug.Log("win");
            GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.PuzzleCompleted);
        }
    }

    public void CheckLoseCondition()
    {
        if (score < m_pointsForWin && initialThrows == 0)
        {
            Debug.Log("loose");
            GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.ResetPuzzle);
        }
    }

    



}
