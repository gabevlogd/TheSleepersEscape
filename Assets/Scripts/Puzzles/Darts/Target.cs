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
    private float m_centerRadius;
    [Header("Throwing")]
    [SerializeField]
    private float m_throwForceForword;
    [SerializeField]
    private float m_throwUpwardForce;
 
    private Vector3 m_mousePosition;
    private Vector3 m_viewfinderInTarget;
    [SerializeField]
    private float m_viewfinderVelocity;
    [SerializeField]
    private float m_viewfinderOutRadius;
    private PlayerInput m_inputs;

    private void Awake()
    {
        m_inputs = new PlayerInput();
        m_inputs.Darts.Throw.performed += PerformThrow;
        m_inputs.Darts.MoveTarget.performed += PerformMousePosition;
        m_inputs.Enable();
        GameManager.Instance.EventManager.Register(Enumerators.Events.StartPuzzle, StartGame);
        GameManager.Instance.EventManager.Register(Enumerators.Events.ResetPuzzle, ResetGame);
        GameManager.Instance.EventManager.Register(Enumerators.Events.PuzzleCompleted, EndGame);
        GameManager.Instance.EventManager.Register(Enumerators.Events.EnableDarts, EnableDartsInteraction);
    }

    private void OnDisable()
    {
        GameManager.Instance.EventManager.Unregister(Enumerators.Events.StartPuzzle, StartGame);
        GameManager.Instance.EventManager.Unregister(Enumerators.Events.ResetPuzzle, ResetGame);
        GameManager.Instance.EventManager.Unregister(Enumerators.Events.PuzzleCompleted, EndGame);
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
        if (GameManager.Instance.dartsManager.m_gameTriggered)
        {
            m_viewfinderOut.transform.position = m_mousePosition;
        }
    }

    private void PerformMousePosition(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.dartsManager.m_gameTriggered)
        {
            Vector2 mousePosition = context.ReadValue<Vector2>();
            m_mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Vector3.Distance(transform.position, Camera.main.transform.position) - 0.1f));
        }
    }

    private void PerformThrow(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.dartsManager.ReadyToThrow && GameManager.Instance.dartsManager.initialThrows > 0 && GameManager.Instance.dartsManager.m_gameTriggered) Throw();
    }

    private void Throw()
    {
        GameManager.Instance.dartsManager.ReadyToThrow = false;

       
        GameObject projectile = Instantiate(m_darts, m_cameraTriggerer.position, Quaternion.identity);

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 direction = m_viewfinderIn.transform.position - Camera.main.transform.position;

        projectile.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

        Vector3 forceToAdd = direction.normalized * m_throwForceForword + transform.up * m_throwUpwardForce;
        
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        GameManager.Instance.dartsManager.m_throwedDarts.Add(projectile);

        GameManager.Instance.dartsManager.initialThrows--;

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
        if (GameManager.Instance.dartsManager.m_gameTriggered)
        {
            m_viewfinderIn.transform.localPosition = Vector3.MoveTowards(m_viewfinderIn.transform.localPosition, m_viewfinderInTarget, m_viewfinderVelocity * Time.deltaTime);
            if (Vector3.Distance(m_viewfinderIn.transform.localPosition, m_viewfinderInTarget) <= 0.001f) SetRandomPosition();
        }   
    }


    public void StartGame()
    {
        //StartCoroutine(GameManager.Instance.dartsManager.EnableThrowAbility()); //not needed anymore, problem fixed in the camera controller
        GameManager.Instance.dartsManager.ReadyToThrow = true;
        Cursor.visible = false;
        m_viewfinderIn.SetActive(true);
        m_viewfinderOut.SetActive(true);
        if (GameManager.Instance.dartsManager.m_throwedDarts != null)
            foreach (GameObject darts in GameManager.Instance.dartsManager.m_throwedDarts) Destroy(darts.gameObject);
        GameManager.Instance.dartsManager.m_throwedDarts = new List<GameObject>();
        GameManager.Instance.dartsManager.m_gameTriggered = true;
    }

    public void EndGame()
    {
        GameManager.Instance.RoomManager.Items[1].SetActive(true); //active the note 1 pick up
        GameManager.Instance.dartsManager.m_gameTriggered = false;
        m_cameraTriggerer.gameObject.SetActive(false);
        Cursor.visible = true;
        m_viewfinderIn.SetActive(false);
        m_viewfinderOut.SetActive(false);
    }

    public void ResetGame()
    {
        m_viewfinderIn.SetActive(false);
        m_viewfinderOut.SetActive(false);
        GameManager.Instance.dartsManager.m_gameTriggered = false;
        GameManager.Instance.dartsManager.score = 0;
        GameManager.Instance.dartsManager.initialThrows = GameManager.Instance.dartsManager.totalThrows;
        Cursor.visible = true;
        m_viewfinderOut.transform.localPosition = new Vector3(0, 0, -0.09f);
        m_viewfinderIn.transform.localPosition = new Vector3(0, 0, -0.1f);
        GameManager.Instance.dartsManager.ReadyToThrow = false;
    }

    public void EnableDartsInteraction()
    {
        m_cameraTriggerer.gameObject.SetActive(true);
    }
}
