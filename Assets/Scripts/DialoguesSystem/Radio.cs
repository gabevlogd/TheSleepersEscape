using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Radio : MonoBehaviour
{
    //public static bool CanInteract;

    public List<Dialogue> Dialogues;
    public Canvas Canvas;
    public TextMeshProUGUI PrintedText;
    public float TextSpeed;

    protected Collider m_collider;
    protected DialoguesHandler m_dialoguesManager;
    protected bool m_runDialogue;
    protected int m_scriptLineIndex;
    protected float m_time;
    protected string m_scriptLineToPrint;
    protected string m_currentDialogueName;
    protected List<string> m_currentDialogue;


    private void Awake()
    {
        m_dialoguesManager = new(Dialogues);
        m_currentDialogue = new();
        m_collider = GetComponent<Collider>();

        GameManager.Instance.EventManager.Register(Enumerators.Events.EnableRadio, EnableRadioInteraction);
        GameManager.Instance.EventManager.Register(Enumerators.Events.DisableRadio, DisableRadioInteraction);
    }

    private void Update() => HandlePrintProcess();


    private void OnMouseUp()
    {
        //if (GameManager.Instance.InventoryManager.InventoryCanvas.gameObject.activeInHierarchy) return;
        //if (GameManager.Instance.PauseManager.PauseUI.activeInHierarchy) return;
        if (GameManager.Instance.Player.PlayerStateMachine.CurrentState.StateID != Enumerators.PlayerState.Navigation) return;

        /*if (CanInteract)*/ TurnOnRadio();
    }

    public void EnableRadioInteraction() => m_collider.enabled = true;
    public void DisableRadioInteraction() => m_collider.enabled = false;

    /// <summary>
    /// Triggers the radio starting a new dialogue (UI text box)
    /// </summary>
    public void TurnOnRadio()
    {
        //CanInteract = false;
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.StartDialogue);
        DisableRadioInteraction();
        m_currentDialogue = m_dialoguesManager.GetDialogue(out m_currentDialogueName);
        m_scriptLineToPrint = m_currentDialogue[0];
        Canvas.gameObject.SetActive(true);
        m_runDialogue = true;
        PrintedText.text = "";
        m_scriptLineIndex = -1;
        m_time = 0;   
    }

    /// <summary>
    /// Stops the radio (Close UI text box)
    /// </summary>
    public void TurnOffRadio()
    {

        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.StopDialogue);
        if (RoomManager.LoopCounter == 1) GameManager.Instance.RoomManager.Items[2].SetActive(true); //active the paper pick up;
        m_runDialogue = false;
        Canvas.gameObject.SetActive(false);
    }

    /// <summary>
    /// Checks if the script line is already all printed, if it is, blocks the print
    /// </summary>
    private void CheckScriptLinePrintStatus()
    {
        if (m_scriptLineToPrint != null && m_scriptLineIndex == m_scriptLineToPrint.Length - 1)
        {
            m_scriptLineIndex = -1;
            m_time = 0f;
            m_scriptLineToPrint = null;
        }
    }

    /// <summary>
    /// Prints on UI text box the current script line character by character 
    /// </summary>
    private void PrintScriptLine()
    {
        if (m_scriptLineToPrint == null || PrintedText.text == m_scriptLineToPrint) return;

        int time = (int)m_time;
        m_time += Time.deltaTime * TextSpeed;


        if (time < (int)m_time)
        {
            m_scriptLineIndex++;
            PrintedText.text += m_currentDialogue[0][m_scriptLineIndex];
        }
    }

    /// <summary>
    /// checks input for script line instant autocomplete request
    /// </summary>
    private void CheckFullLinePrintRequest()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_scriptLineToPrint != null)
        {
            PrintedText.text = m_scriptLineToPrint;
            m_scriptLineIndex = m_scriptLineToPrint.Length - 1;
        }
    }


    /// <summary>
    /// Selects the next script line to print, if there are no others, it interrupts the dialogue
    /// </summary>
    private void LoadNextScriptLine()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_scriptLineToPrint == null)
        {
            if (m_currentDialogue.Count > 1)
            {
                m_currentDialogue.Remove(m_currentDialogue[0]);
                m_scriptLineToPrint = m_currentDialogue[0];
                PrintedText.text = "";
            }
            else if (m_currentDialogue.Count == 1) TurnOffRadio();
        }
    }

    

    private void HandlePrintProcess()
    {
        if (m_runDialogue)
        {
            CheckScriptLinePrintStatus();
            CheckFullLinePrintRequest();
            PrintScriptLine();
            LoadNextScriptLine();
        }
    }

    
}
