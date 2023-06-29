using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Target : MonoBehaviour 
{
    [SerializeField]
    private Transform m_bullEyePosition;
    [SerializeField]
    private GameObject m_darts;
    [SerializeField]
    private Transform m_cameraTriggerer;

    [SerializeField]
    private GameObject m_viewfinderIn;
    [SerializeField]
    private GameObject m_viewfinderOut;
   
    [Header("Settings")]
    [SerializeField]
    private int m_totalThrows;
    private int m_initialThrows;
    [SerializeField]
    private float m_centerRadius;
    [Header("Throwing")]
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

    private PlayerInput m_inputs;

    private Vector3 m_mousePosition;
    private Vector3 m_viewfinderInTarget;
    [SerializeField]
    private float m_viewfinderVelocity;
    [SerializeField]
    private float m_viewfinderOutRadius;

    private List<GameObject> m_throwedDarts;

    private void Awake()
    {
        m_inputs = new PlayerInput();
        m_inputs.Darts.Throw.performed += PerformThrow;
        m_inputs.Darts.MoveTarget.performed += PerformMousePosition;
        m_inputs.Enable();
        m_initialThrows = m_totalThrows;
        GameManager.Instance.EventManager.Registrer(Enumerators.Events.StartPuzzle, StartGame);
        GameManager.Instance.EventManager.Registrer(Enumerators.Events.ResetPuzzle, ResetGame);
        GameManager.Instance.EventManager.Registrer(Enumerators.Events.PuzzleCompleted,EndGame);
    }

    private void OnDisable()
    {
        GameManager.Instance.EventManager.Unregistrer(Enumerators.Events.StartPuzzle, StartGame);
        GameManager.Instance.EventManager.Unregistrer(Enumerators.Events.ResetPuzzle, ResetGame);
        GameManager.Instance.EventManager.Unregistrer(Enumerators.Events.PuzzleCompleted, EndGame);
    }

    private void Start()
    {
        m_viewfinderIn.SetActive(false);
        m_viewfinderOut.SetActive(false);  
    }

    private void Update()
    {
        //Debug.DrawLine(m_cameraTriggerer.position, m_viewfinderIn.transform.position);
        HandleViewfinderOut();
        MoveToRandomPosition();
    }

    private void HandleViewfinderOut()
    {
        if (m_gameTriggered)
        {
            m_viewfinderOut.transform.position = m_mousePosition;
        }
    }

    private void PerformMousePosition(InputAction.CallbackContext context)
    {
        if (m_gameTriggered)
        {
            Vector2 mousePosition = context.ReadValue<Vector2>();
            m_mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Vector3.Distance(transform.position, Camera.main.transform.position) - 0.1f));
        }
    }

    private void PerformThrow(InputAction.CallbackContext context)
    {
        if (ReadyToThrow && m_initialThrows > 0 && m_gameTriggered) Throw();
    }

    private IEnumerator EnableThrowAbility()
    {
        yield return new WaitForSeconds(1f);
        ReadyToThrow = true;
    }

    private void Throw()
    {
        ReadyToThrow = false;

        // maybe the rotation is incorrect wait the 3D model
        GameObject projectile = Instantiate(m_darts, m_cameraTriggerer.position, Quaternion.identity);

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 direction = m_viewfinderIn.transform.position - Camera.main.transform.position;
        
        Vector3 forceToAdd = direction.normalized * m_throwForceForword + transform.up * m_throwUpwardForce;
        
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        m_throwedDarts.Add(projectile);

        m_initialThrows--;

    }

    public bool BullEyeHitted(Vector3 hittedPosition)
    {
        if (Vector3.Distance(m_bullEyePosition.position, hittedPosition) <= m_centerRadius) return true;
        else return false;

    }

    private void SetRandomPosition()
    {
        m_viewfinderInTarget = Random.insideUnitCircle * m_viewfinderOutRadius;
    }

    private void MoveToRandomPosition()
    {
        if (m_gameTriggered)
        {
            m_viewfinderIn.transform.localPosition = Vector3.MoveTowards(m_viewfinderIn.transform.localPosition, m_viewfinderInTarget, m_viewfinderVelocity * Time.deltaTime);
            if (Vector3.Distance(m_viewfinderIn.transform.localPosition, m_viewfinderInTarget) <= 0.001f) SetRandomPosition();
        }   
    }

    public void SetScore(int value)
    {
        
        m_score += value;
        CheckWinCondition();
        CheckLoseCondition();
        //Debug.Log(m_score + " " + value);
    }
    
    private void CheckWinCondition()
    {
        
        if (m_score == m_pointsForWin )
        {

            Debug.Log("win");
            GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.PuzzleCompleted);
        }
        

    }

    private void CheckLoseCondition()
    {
        if (m_score < m_pointsForWin && m_initialThrows == 0)
        {
            Debug.Log("loose");
            GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.ResetPuzzle);
        }
    }

    public void StartGame()
    {
        StartCoroutine(EnableThrowAbility());
        Cursor.visible = false;
        m_viewfinderIn.SetActive(true);
        m_viewfinderOut.SetActive(true);
        if (m_throwedDarts!=null) 
            foreach (GameObject darts in m_throwedDarts) Destroy(darts.gameObject);
        m_throwedDarts = new List<GameObject>();
        m_gameTriggered = true;
    }

    public void EndGame()
    {
        m_gameTriggered = false;
        m_cameraTriggerer.gameObject.SetActive(false);
        Cursor.visible = true;
        m_viewfinderIn.SetActive(false);
        m_viewfinderOut.SetActive(false);
    }

    public void ResetGame()
    {
        m_viewfinderIn.SetActive(false);
        m_viewfinderOut.SetActive(false);
        m_gameTriggered = false;
        m_score = 0;
        m_initialThrows = m_totalThrows;
        Cursor.visible = true;
        m_viewfinderOut.transform.localPosition = new Vector3(0, 0, -0.09f);
        m_viewfinderIn.transform.localPosition = new Vector3(0, 0, -0.1f);
        ReadyToThrow = false;
    }

}
