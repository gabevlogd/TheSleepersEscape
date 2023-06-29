using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialoguesManager : MonoBehaviour
{
    public static DialoguesManager Instance;

    public List<Dialogue> Dialogues;
    public List<Speaker> Speakers;

    [HideInInspector]
    public List<List<string>> DialoguesScriptLines;
    [HideInInspector]
    public List<string> DialoguesNames;

    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        GenerateDialogues();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) PlayDialogue();
    }



    public void PlayDialogue()
    {
        string dialogueName = DialoguesNames[0];
        List<string> dialogue = DialoguesScriptLines[0];

        DialoguesScriptLines.Remove(dialogue);
        DialoguesNames.Remove(dialogueName);

        Debug.Log("Playing: " + dialogueName);   

        foreach(string scriptLine in dialogue)
        {
            Debug.Log(scriptLine);
        }
    }

    public void GenerateDialogues()
    {
        DialoguesScriptLines = new List<List<string>>();
        Debug.Log(Dialogues);
        foreach(Dialogue dialogue in Dialogues)
        {
            DialoguesScriptLines.Add(dialogue.GetDialogue());
            DialoguesNames.Add(dialogue.DialogueName);
        }
    }


}


