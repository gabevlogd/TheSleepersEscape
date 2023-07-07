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
    public int TargetNumber;
    public Dial Twin;

    public int CurrentNumber { get; private set; }
    public bool CorrectNumber { get; private set; }

    private Quaternion m_targetRotation;
    private bool m_mustRotate;
    

    private void Awake()
    {
        CurrentNumber = StartingSelectedNumber;

        GameManager.Instance.EventManager.Register(Enumerators.Events.EnableDials, EnableDials);
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
            if (CheckCombination()) OpenDoor();
            else m_mustRotate = false;
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
            return 36f;
        }
        else
        {
            SetCurrentNumber(1);
            return -36f;
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
        if (CurrentNumber == TargetNumber) CorrectNumber = true;
        else CorrectNumber = false;

        if (CorrectNumber && Twin.CorrectNumber) return true;
        else return false;
    }

    private void OpenDoor()
    {
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.OpenDoor);
        CameraTriggerer.gameObject.SetActive(false);
        Twin.enabled = false;
        this.enabled = false;
    }

    public void EnableDials()
    {
        //ricordati di settare il discorso dei dial corretti da attivare (possibile soluzione itrodurre un id per ogni dial e controllare il Loop Counter del room manager)
        CameraTriggerer.gameObject.SetActive(true);
    }
}
