using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public MemoryManager MemoryManager;
    public EventManagerBase<Enumerators.Events> EventManager;
    public Player Player;
    public RoomManager RoomManager;
    public DartsManager dartsManager;

    public SoundEventManager<Enumerators.MusicEvents> SoundEventManager;
    public SoundManager SoundManager;


    protected override void Awake()
    {
        base.Awake();
        EventManager = new();
        SoundEventManager = new();

    }




}



