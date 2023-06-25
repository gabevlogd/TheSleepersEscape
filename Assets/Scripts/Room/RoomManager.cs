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
    }
}
