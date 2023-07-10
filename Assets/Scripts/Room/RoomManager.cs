using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static int LoopCounter;

    public RoomStatesMachine RoomStatesMachine;
    public List<GameObject> Puzzles;
    public List<GameObject> Items;
    public List<GameObject> GameObjects;
    public List<GameObject> RoomLights;
    public List<GameObject> PuzzleLights;

    private void Awake()
    {
        LoopCounter = 1;
        RoomStatesMachine = new();
        RoomStatesMachine.CurrentState = RoomStatesMachine.AllStates[Enumerators.RoomState.LoopOne];
        RoomStatesMachine.CurrentState.OnEnter();

        GameManager.Instance.EventManager.Register(Enumerators.Events.TurnOffLights, TurnOffLights);
        GameManager.Instance.EventManager.Register(Enumerators.Events.TurnOnLights, TurnOnLights);
    }

    private void Update()
    {
        RoomStatesMachine.CurrentState.OnUpdate();
    }

    public void TurnOffLights()
    {
        foreach (GameObject light in RoomLights) light.SetActive(!light.activeInHierarchy);
        foreach (GameObject light in PuzzleLights) light.SetActive(!light.activeInHierarchy);
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.EnableSwitch);
    }

    public void TurnOnLights()
    {
        foreach (GameObject light in RoomLights) light.SetActive(!light.activeInHierarchy);
    }
}
