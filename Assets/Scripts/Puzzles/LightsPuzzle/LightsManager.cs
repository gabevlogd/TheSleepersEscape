using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsManager : MonoBehaviour
{
    public List<LightSwitch> LightSwitches;

    public bool[] TargetPattern;

    private void OnMouseDown() => CheckWinCondition();


    public void CheckWinCondition()
    {
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


