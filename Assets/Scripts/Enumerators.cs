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
        DisableRadio

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
        OnPause
    }

}
