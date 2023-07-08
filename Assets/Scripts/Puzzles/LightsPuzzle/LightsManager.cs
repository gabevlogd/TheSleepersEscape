using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsManager : MonoBehaviour
{
    public List<LightSwitch> LightSwitches;

    public static bool GameTriggered;

    public bool[] TargetPattern;

    private Collider m_collider;

    private void Awake()
    {
        //GameTriggered = false;
        m_collider = GetComponent<Collider>();
        GameManager.Instance.EventManager.Register(Enumerators.Events.EnableSwitch, EnableLightsPuzzle);
        GameManager.Instance.EventManager.Register(Enumerators.Events.DisableSwitch, DisableLightsPuzzle);
    }

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

        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.DisableSwitch);
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.TurnOnLights);
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.OpenDoor);

        Debug.Log("win");
    }

    public void EnableLightsPuzzle() => m_collider.enabled = true;
    public void DisableLightsPuzzle() => m_collider.enabled = false; 

}


