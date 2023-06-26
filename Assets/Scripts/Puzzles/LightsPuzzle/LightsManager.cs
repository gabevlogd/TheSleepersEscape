using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsManager : MonoBehaviour
{
    public List<LightSwitch> LightSwitches;

    public static bool GameTriggered;

    public bool[] TargetPattern;

    private void Awake() => GameTriggered = false;

    private void OnMouseDown() => CheckWinCondition();


    public void CheckWinCondition()
    {
       if (LightSwitches.Count != TargetPattern.Length)
        {
            Debug.Log("the length of Target Pattern need to match the number of switches, modify it in the Light Manager inspector");
            return;
        }

        for (int i = 0; i < LightSwitches.Count; i++)
        {
            if (LightSwitches[i].Triggered != TargetPattern[i])
            {
                Debug.Log("not win");
                return;
            }
        }

        for (int i = 0; i < LightSwitches.Count; i++)
        {
            LightSwitches[i].GetComponent<Collider>().enabled = false;
            LightSwitches[i].GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
        }
        Debug.Log("win");
    }

}


