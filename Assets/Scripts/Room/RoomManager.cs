using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static int LoopCounter;

    public RoomStatesMachine RoomStatesMachine;
    public List<GameObject> Puzzles;
    public List<GameObject> Items;

    private void Awake()
    {
        LoopCounter = 1;
        RoomStatesMachine = new();
        RoomStatesMachine.CurrentState = RoomStatesMachine.AllStates[Enumerators.RoomState.LoopOne];
        RoomStatesMachine.CurrentState.OnEnter();
    }

    private void Update()
    {
        RoomStatesMachine.CurrentState.OnUpdate();
    }
}
