using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitPoint : MonoBehaviour
{
    public Transform RespawnPoint;

    private void Awake()
    {
        GameManager.Instance.EventManager.Register(Enumerators.Events.MoveExitPoint, SetPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (RoomManager.LoopCounter == 3) GameManager.PlayerWin = true;
            GoToNexLoop();
        }
    }

    private void GoToNexLoop() => StartCoroutine(PerformLoopChange());

    private IEnumerator PerformLoopChange()
    {
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.EnterLoopChange);
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.StartFade);

        yield return new WaitUntil(() => GameManager.Instance.HUDManager.CanFade == false);

        ChangeRoomState();
        RespawnPlayer();
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.CloseDoor);
        
        yield return new WaitForSeconds(GameManager.Instance.HUDManager.TimeBetweenFades);

        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.StartFade);

        yield return new WaitUntil(() => GameManager.Instance.HUDManager.CanFade == false);

        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.ExitLoopChange);
    }

    private void RespawnPlayer()
    {
        GameManager.Instance.Player.transform.position = RespawnPoint.position;
        GameManager.Instance.Player.transform.rotation = RespawnPoint.rotation;
    }

    private void ChangeRoomState()
    {
        if (RoomManager.LoopCounter == 1) GameManager.Instance.RoomManager.RoomStatesMachine.ChangeState(Enumerators.RoomState.LoopTwo);
        else if (RoomManager.LoopCounter == 2) GameManager.Instance.RoomManager.RoomStatesMachine.ChangeState(Enumerators.RoomState.LoopThree);
        else if (RoomManager.LoopCounter == 3)
        {
            Debug.Log("End game");
            //GameManager.PlayerWin = true;
            GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.GameOver);
        }
    }

    public void SetPosition() => transform.position += new Vector3(0f, 0f, 6f);


}