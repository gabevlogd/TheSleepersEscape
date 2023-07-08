using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public MemoryManager MemoryManager;
    public EventManagerBase<Enumerators.Events> EventManager;
    public Player Player;
    public RoomManager RoomManager;
    public DartsManager dartsManager;
    public InventoryManager InventoryManager;
    public PauseManager PauseManager;
    public static bool PlayerWin;

    protected override void Awake()
    {
        base.Awake();
        EventManager = new();
        EventManager.Register(Enumerators.Events.GameOver, GameOver);
        //DontDestroyOnLoad(this.gameObject);
    }

    public void GameOver() => SceneManager.LoadScene("GameOver");




}



