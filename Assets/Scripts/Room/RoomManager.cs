using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public RoomStatesMachine RoomStatesMachine;
    public List<GameObject> Puzzles;

    private void Awake()
    {
        RoomStatesMachine = new();
        RoomStatesMachine.CurrentState = RoomStatesMachine.AllStates[Enumerators.RoomState.LoopOne];
        RoomStatesMachine.CurrentState.OnEnter();
    }

    private void Update()
    {
        RoomStatesMachine.CurrentState.OnUpdate();
    }
}
