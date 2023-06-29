using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Radio : MonoBehaviour
{
    public static bool CanInteract;

    public List<Dialogue> RadioDialogues;
    public Canvas Canvas;
    public TextMeshProUGUI PrintedText;
    public float TextSpeed;

    private DialoguesHandler m_dialoguesManager;
    private bool m_runDialogue;
    private int m_scriptLineIndex;
    private float m_time;
    private string m_scriptLineToPrint;
    private string m_currentDialogueName;
    private List<string> m_currentDialogue;


    private void Awake()
    {
        m_dialoguesManager = new(RadioDialogues);
        m_currentDialogue = new();
        //m_canvas = GetComponentInChildren<Canvas>();
        //m_printedText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update() => HandlePrintProcess();
    

    private void OnMouseDown()
    {
        if (CanInteract) TurnOnRadio();
    }

    /// <summary>
    /// Triggers the radio starting a new dialogue (UI text box)
    /// </summary>
    public void TurnOnRadio()
    {
        CanInteract = false;
        m_currentDialogue = m_dialoguesManager.GetDialogue(out m_currentDialogueName);
        m_scriptLineToPrint = m_currentDialogue[0];
        Canvas.gameObject.SetActive(true);
        m_runDialogue = true;
        PrintedText.text = "";
        m_scriptLineIndex = -1;
        m_time = 0;   
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
            else if (m_currentDialogue.Count == 1)
            {
                m_runDialogue = false;
                Canvas.gameObject.SetActive(false);
            }
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
