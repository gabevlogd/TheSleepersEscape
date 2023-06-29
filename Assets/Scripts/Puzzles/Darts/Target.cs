using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Target : MonoBehaviour 
{
    [SerializeField]
    private Transform m_bullEyePosition;

    [SerializeField]
    private GameObject m_darts;
    [SerializeField]
    private Transform m_cameraTriggerer;
    //[SerializeField]
    //private float ViewfinderDistanceToThePlayer;

    [Header("Settings")]
    [SerializeField]
    private int m_totalThrows;
    [SerializeField]
    private float m_centerRadius;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    [SerializeField]
    private float m_throwForceForword;
    [SerializeField]
    private float m_throwUpwardForce;

    public static bool ReadyToThrow;

    [SerializeField]
    private bool m_gameTriggered;

    [SerializeField]
    private int m_score;
    [SerializeField]
    private int m_pointsForWin;


    private void Start()
    {
        ReadyToThrow = true;
        //ViewPoint.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, ViewfinderDistanceToThePlayer));
    }

    /// <summary>
    /// debug
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(throwKey) && ReadyToThrow && m_totalThrows > 0 && m_gameTriggered)
        {
            Throw();
        }
        

    }


    private void Throw()
    {
        ReadyToThrow = false;

        // maybe the rotation is incorrect wait the 3D model
        GameObject projectile = Instantiate(m_darts, m_cameraTriggerer.position, Quaternion.identity);

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 forceToAdd = m_cameraTriggerer.forward * m_throwForceForword + transform.up * m_throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        m_totalThrows--;

    }

    public bool BullEyeHitted(Vector3 hittedPosition)
    {
        if (Vector3.Distance(m_bullEyePosition.position, hittedPosition) <= m_centerRadius) return true;
        else return false;

    }



    public void SetScore(int value)
    {
        m_score += value;
        CheckWinCondition();
        CheckLoseCondition();
        Debug.Log(m_score + " " + value);
    }

    private void CheckWinCondition()
    {
        
        if (m_score == m_pointsForWin )
        {

            Debug.Log("win");
            //GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.PuzzleCompleted);
        }
        

    }

    private void CheckLoseCondition()
    {
        if (m_score < m_pointsForWin && m_totalThrows == 0)
        {
            Debug.Log("loose");
            //GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.ResetPuzzle);
        }
    }


    public void StartGame()
    {
        m_gameTriggered = true;
    }

    public void EndGame()
    {
        m_gameTriggered = false;
        m_cameraTriggerer.gameObject.SetActive(false);
    }

    public void ResetGame()
    {
        m_gameTriggered = false;
        m_score = 0;

    }

}
