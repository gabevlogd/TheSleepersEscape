using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) GameManager.Instance.RoomManager.RoomStatesMachine.ChangeState(Enumerators.RoomState.LoopOne);
        if (Input.GetKeyDown(KeyCode.Alpha2)) GameManager.Instance.RoomManager.RoomStatesMachine.ChangeState(Enumerators.RoomState.LoopTwo);
        if (Input.GetKeyDown(KeyCode.Alpha3)) GameManager.Instance.RoomManager.RoomStatesMachine.ChangeState(Enumerators.RoomState.LoopThree);
    }
}
