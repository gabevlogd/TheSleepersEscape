using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitPoint : MonoBehaviour
{
    public Image FadeEffect;
    public Transform RespawnPoint;

    public float FadeSpeed;
    public float TimeBetweenFades;

    private bool m_canFade;
    private int m_fadeDirection;
    private Color m_fadeColor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player)) GoToNexLoop();
    }

    private void Update() => HandleFade();

    private void GoToNexLoop() => StartCoroutine(PerformLoopChange());

    private IEnumerator PerformLoopChange()
    {
        //GameManager.Instance.Player.PlayerController.DisableController(); 
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.EnterLoopChange);

        StartFade(); 

        yield return new WaitUntil(() => m_canFade == false);

        ChangeRoomState();
        RespawnPlayer();
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.CloseDoor);
        
        yield return new WaitForSeconds(TimeBetweenFades);

        StartFade();

        yield return new WaitUntil(() => m_canFade == false);

        //GameManager.Instance.Player.PlayerController.EnableController();
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.ExitLoopChange);
    }

    private void RespawnPlayer()
    {
        GameManager.Instance.Player.transform.position = RespawnPoint.position;
        GameManager.Instance.Player.transform.rotation = RespawnPoint.rotation;
    }

    private void StartFade()
    {
        m_fadeColor = FadeEffect.color;
        if (m_fadeColor.a <= 0)
        {
            m_fadeDirection = 1;
            m_fadeColor.a = 0;
        }
        else if (m_fadeColor.a >= 1)
        {
            m_fadeDirection = -1;
            m_fadeColor.a = 1;
        }
        else return;

        m_canFade = true;
    }

    private void HandleFade()
    {
        if (m_canFade)
        {
            m_fadeColor.a += Time.deltaTime * FadeSpeed * m_fadeDirection;
            FadeEffect.color = m_fadeColor;

            if (m_fadeColor.a <= 0f || m_fadeColor.a >= 1f) m_canFade = false;
        }
    }

    private void ChangeRoomState()
    {
        if (RoomManager.LoopCounter == 1) GameManager.Instance.RoomManager.RoomStatesMachine.ChangeState(Enumerators.RoomState.LoopTwo);
        else if (RoomManager.LoopCounter == 2) GameManager.Instance.RoomManager.RoomStatesMachine.ChangeState(Enumerators.RoomState.LoopThree);
        else if (RoomManager.LoopCounter == 3)
        {
            Debug.Log("End game");
            GameManager.PlayerWin = true;
            GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.GameOver);
        }
    }


}