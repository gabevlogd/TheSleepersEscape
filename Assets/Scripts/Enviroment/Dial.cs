using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dial : MonoBehaviour, IPointerClickHandler
{
    public Transform CameraTriggerer;
    [Range(0, 9)]
    public int StartingSelectedNumber;
    public float AngularSpeed;
    public int TargetNumber1;
    public int TargetNumber2;
    public int TargetNumber3;
    public Dial Twin;

    //public int DialID;

    public int CurrentNumber { get; private set; }
    public bool CorrectNumber { get; private set; }

    private Quaternion m_targetRotation;
    private bool m_mustRotate;
    

    private void Awake()
    {
        CurrentNumber = StartingSelectedNumber;

        GameManager.Instance.EventManager.Register(Enumerators.Events.EnableDials, EnableDials);
        GameManager.Instance.EventManager.Register(Enumerators.Events.DisableDials, DisableDials);
    }

    private void Update() => HandleRotation();
   
    public void OnPointerClick(PointerEventData eventData) => SetTargetRotation(GetRotationDirection(eventData));
    

    private void HandleRotation()
    {
        if (!m_mustRotate) return;

        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, m_targetRotation, Time.deltaTime * AngularSpeed * 100f);

        if (Mathf.Abs(Quaternion.Dot(transform.localRotation, m_targetRotation) - 1f) <= 0.0001f)
        {
            transform.localRotation = m_targetRotation;
            if (CheckCombination())
            {
                if (RoomManager.LoopCounter == 3) GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.TurnOffLights);
                else OpenDoor();
            }
            m_mustRotate = false;
        }
    }

    private void SetTargetRotation(float value)
    {
        if (m_mustRotate) return;

        m_targetRotation = transform.localRotation * Quaternion.Euler(value, 0f, 0f);
        m_mustRotate = true;
    }

    private float GetRotationDirection(PointerEventData eventData)
    {
        if (m_mustRotate) return 0f;

        if (eventData.position.y >= Camera.main.pixelHeight * 0.5f)
        {
            SetCurrentNumber(-1);
            return -36f;
        }
        else
        {
            SetCurrentNumber(1);
            return 36f;
        }
    }

    private void SetCurrentNumber(int value)
    {
        CurrentNumber += value;
        if (CurrentNumber > 9) CurrentNumber = 0;
        else if (CurrentNumber < 0) CurrentNumber = 9;
        Debug.Log(CurrentNumber);
    }

    private bool CheckCombination()
    {
        int currentTargetNumber = 0;
        if (RoomManager.LoopCounter == 1) currentTargetNumber = TargetNumber1;
        else if (RoomManager.LoopCounter == 2) currentTargetNumber = TargetNumber2;
        else if (RoomManager.LoopCounter == 3) currentTargetNumber = TargetNumber3;

        if (CurrentNumber == currentTargetNumber) CorrectNumber = true;
        else CorrectNumber = false;

        if (CorrectNumber && Twin.CorrectNumber) return true;
        else return false;
    }

    private void OpenDoor()
    {
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.OpenDoor);
        CameraTriggerer.gameObject.SetActive(false);
        Twin.CorrectNumber = false;
        this.CorrectNumber = false;
    }

    public void EnableDials()
    {
        //if (RoomManager.LoopCounter == 1 && DialID != 1) return;
        //else if (RoomManager.LoopCounter == 2 && DialID != 2) return;
        //else if (RoomManager.LoopCounter == 3 && DialID != 3) return;
        //ricordati di settare il discorso dei dial corretti da attivare (possibile soluzione itrodurre un id per ogni dial e controllare il Loop Counter del room manager)
        CameraTriggerer.gameObject.SetActive(true);
    }

    public void DisableDials()
    {
        CameraTriggerer.gameObject.SetActive(false);
    }
}
