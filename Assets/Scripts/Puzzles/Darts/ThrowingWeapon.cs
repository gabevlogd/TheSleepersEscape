using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThrowingWeapon : MonoBehaviour
{
    public GameObject Darts;
    public Transform DartsPosition;
    public GameObject ViewPoint;
    [SerializeField]
    private float ViewfinderDistanceToThePlayer;

    [Header("References")]
    public Transform cam;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    public bool GameTriggered;

    public int Score;

    public int PointsForWin;

    private void Start()
    {
        readyToThrow = true;
        ViewPoint.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, ViewfinderDistanceToThePlayer));
    }

    private void Update()
    {
        if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0 && GameTriggered)
        {
            Throw();
        }
        
    }

    private void Throw()
    {
        readyToThrow = false;

        // instantiate object to throw
        GameObject projectile = Instantiate(Darts, DartsPosition.position, Quaternion.Euler(0,80,90));

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - DartsPosition.position).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        // implement throwCooldown
        Invoke("ResetThrow", throwCooldown);

    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
    

    //public void SetScore(int value)
    //{
    //    Score += value;
    //    CheckLoseCondition();
    //    CheckWinCondition();
    //}

    private void CheckWinCondition()
    {
        if (Score == PointsForWin && totalThrows >= 0)
        {
            Debug.Log("win");
            //GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.PuzzleCompleted);
        } 
    }

    private void CheckLoseCondition()
    {
        if (Score < PointsForWin && totalThrows == 0)
        {
            //GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.ResetPuzzle);
        }
    }


    public void StartGame()
    {
        GameTriggered = true;
    }

    public void EndGame()
    {
        GameTriggered = false;
        //CameraTriggerer.gameObject.SetActive(false);
    }

    public void ResetGame()
    {
        GameTriggered = false;
        Score = 0;

    }
}
