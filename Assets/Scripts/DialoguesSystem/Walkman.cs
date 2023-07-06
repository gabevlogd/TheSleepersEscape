using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Walkman : MonoBehaviour
{
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

        GameManager.Instance.EventManager.Register(Enumerators.Events.PlayWalkman, TurnOnWalkman);

    }

    private void Update() => HandlePrintProcess();



    /// <summary>
    /// Triggers the radio starting a new dialogue (UI text box)
    /// </summary>
    public void TurnOnWalkman()
    {
        //CanInteract = false;
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.StartDialogue);
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
    public void TurnOffWalkman()
    {
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.StopDialogue);
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
            else if (m_currentDialogue.Count == 1) TurnOffWalkman();
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
