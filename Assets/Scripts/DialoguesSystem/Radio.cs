using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Radio : MonoBehaviour
{
    public List<Dialogue> RadioDialogues;
    public float TextSpeed;

    private DialoguesHandler m_dialoguesManager;
    private Canvas m_canvas;
    private TextMeshProUGUI m_printedText;

    private float m_time;
    private int m_currentDialogueIndex;
    private string m_currentDialogueName;
    private bool m_runDialogue;
    private List<string> m_currentDialogue;

    private void Awake()
    {
        m_dialoguesManager = new(RadioDialogues);
        m_canvas = GetComponentInChildren<Canvas>();
        m_printedText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) m_dialoguesManager.PlayDialogue();

        HandlePrintedText();
    }

    public void TurnOnRadio()
    {
        m_currentDialogue = m_dialoguesManager.GetDialogue(out m_currentDialogueName);
        m_canvas.gameObject.SetActive(true);
        m_runDialogue = true;
        m_printedText.text = "";
        m_currentDialogueIndex = 0;
        m_time = 0;

        

    }

    private void HandlePrintedText()
    {
        if (!m_runDialogue) return;

        int time = (int)m_time;
        m_time += Time.deltaTime * TextSpeed;
        if (time < (int)m_time) m_currentDialogueIndex++;

        //m_printedText.text += 

        if (m_currentDialogueIndex >= m_currentDialogue.Count) m_runDialogue = false;
    }
}
