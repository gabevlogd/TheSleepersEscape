public static class Enumerators 
{
    public enum Events
    {
        PuzzleCompleted,
        StartPuzzle,
        ResetPuzzle,
        OpenDoor,
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
        RemoveWalkman,
        EnterLoopChange,
        ExitLoopChange,
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
        OnLoopChange
    }

	public enum MusicEvents
	{
		PlaySoundPlayer,
        PlaySoundDoor,
        PlaySoundRadio,
        PlaySoundWatch,
        PlaySoundGenerator // si spegne quando si spengono le luci si riaccende quando si riaccendono le luci
	}
}
