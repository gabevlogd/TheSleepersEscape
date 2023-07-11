public static class Enumerators 
{
    public enum Events
    {
        PuzzleCompleted,
        StartPuzzle,
        ResetPuzzle,
        OpenDoor,
        OpenDoorOnGameOver,
        CloseDoor,
        OpenInventory,
        CloseInventory,
        StartInteraction,
        StopInteraction,
        ResumeGame,
        PauseGame,
        ShowHud,
        HideHud,
        ShowInteractablePoint,
        HideInteractablePoint,
        StartDialogue,
        StopDialogue, 
        EnableRadio,
        DisableRadio,
        ItemCollected,
        PlayWalkman,
        EnableDarts,
        EnableDials,
        DisableDials,
        RemoveWalkman,
        EnterLoopChange,
        ExitLoopChange,
        PickUpNote,
        TurnOffLights,
        TurnOnLights,
        EnableSwitch,
        DisableSwitch,
        UpdateSettings,
        StartFade,
        SetClockView,
        MoveExitPoint,
        NextTutorial,
        CloseTutorial,
		GameOver,
        DisableDialsCollider

	}

    public enum RoomState
    {
        LoopOne,
        LoopTwo,
        LoopThree
        
    }

    public enum PlayerState
    {
        OnTutorial,
        Navigation,
        RunningPuzzle,
        RunningInteractable,
        OnInventory,
        OnDialogue,
        OnPause,
        OnLoopChange,
        OnGameOver
    }

	public enum MusicEvents
	{
		PlaySoundPlayer,
        PlaySoundDoor,
        PlaySoundRadio,
        PlaySoundEnv,
        PlaySoundGenerator // si spegne quando si spengono le luci si riaccende quando si riaccendono le luci
	}
}
