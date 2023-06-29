using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Speaker
{
    public string SpeakerName;
    public List<string> Script;

    public string Speak()
    {
        string result = Script[0];

        Script.Remove(result);

        return result;
    }
}
