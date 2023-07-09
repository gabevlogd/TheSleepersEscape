using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

public class GameManager : Singleton<GameManager>
{
    public MemoryManager MemoryManager;
    public EventManagerBase<Enumerators.Events> EventManager;
    public Player Player;
    public RoomManager RoomManager;
    public DartsManager dartsManager;
    public InventoryManager InventoryManager;
    public PauseManager PauseManager;
    public HUDManager HUDManager;
    public static bool PlayerWin;

    protected override void Awake()
    {
        base.Awake();
        PlayerWin = false;
        EventManager = new();
        EventManager.Register(Enumerators.Events.GameOver, GameOver);
        //DontDestroyOnLoad(this.gameObject);
    }

    public void GameOver()
    {
        if (!PlayerWin) StartCoroutine(PerformLoseSequence());
        else SceneManager.LoadScene("GameOver");
    }

    private IEnumerator PerformLoseSequence()
    {
        EventManager.TriggerEvent(Enumerators.Events.StartFade); //fade out

        Camera.main.GetUniversalAdditionalCameraData().cameraStack.Clear(); //remove all canvas from the screen

        yield return new WaitUntil(() => HUDManager.CanFade == false); //wait for fade end

        EventManager.TriggerEvent(Enumerators.Events.SetClockView);

        //yield return new WaitForSeconds(1f); //wait one second

        EventManager.TriggerEvent(Enumerators.Events.StartFade); //fade in

        yield return new WaitUntil(() => HUDManager.CanFade == false); //wait for fade end

        yield return new WaitForSeconds(3f); //wait three seconds

        EventManager.TriggerEvent(Enumerators.Events.OpenDoorOnGameOver);

        yield return new WaitForSeconds(4f); //wait three seconds

        EventManager.TriggerEvent(Enumerators.Events.StartFade); //fade out

        yield return new WaitUntil(() => HUDManager.CanFade == false); //wait for fade end

        SceneManager.LoadScene("GameOver");

        
    }


}



