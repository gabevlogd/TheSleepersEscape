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
		GameOver

	}

    public enum RoomState
    {
        LoopOne,
        LoopTwo,
        LoopThree
        
    }

    public enum PlayerState
    {
        Navigation,
        RunningPuzzle,
        RunningInteractable,
        OnInventory,
        OnDialogue,
        OnPause,
        OnLoopChange,
        OnGameOver
    }

}
