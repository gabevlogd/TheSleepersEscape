using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    public string DialogueName;
    public List<string> SpeakersOrder;
    


    public List<string> GetDialogue()
    {
        List<string> dialogue = new List<string>();

        foreach (string speakerName in SpeakersOrder)
        {
            foreach(Speaker speaker in DialoguesManager.Instance.Speakers)
            {
                if (speaker.SpeakerName != speakerName) continue;

                dialogue.Add(speakerName + ": " + speaker.Speak() + "\n");
                break;
            }
        }

        return dialogue;

    }
}
