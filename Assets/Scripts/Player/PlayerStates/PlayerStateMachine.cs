using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StatesMachine<Enumerators.PlayerState>
{
    protected override void InitStates()
    {
        AllStates.Add(Enumerators.PlayerState.Navigation, new Navigation(Enumerators.PlayerState.Navigation, this));
        AllStates.Add(Enumerators.PlayerState.RunningPuzzle, new RunningPuzzle(Enumerators.PlayerState.RunningPuzzle, this));
        AllStates.Add(Enumerators.PlayerState.RunningInteractable, new RunningInteractable(Enumerators.PlayerState.RunningInteractable, this));
        AllStates.Add(Enumerators.PlayerState.OnInventory, new OnInventory(Enumerators.PlayerState.OnInventory, this));
        AllStates.Add(Enumerators.PlayerState.OnPause, new OnPause(Enumerators.PlayerState.OnPause, this));
        AllStates.Add(Enumerators.PlayerState.OnDialogue, new OnDialogue(Enumerators.PlayerState.OnDialogue, this));
        AllStates.Add(Enumerators.PlayerState.OnLoopChange, new OnLoopChange(Enumerators.PlayerState.OnLoopChange, this));
        AllStates.Add(Enumerators.PlayerState.OnGameOver, new OnGameOver(Enumerators.PlayerState.OnGameOver, this));
        AllStates.Add(Enumerators.PlayerState.OnTutorial, new OnTutorial(Enumerators.PlayerState.OnTutorial, this));

        CurrentState = AllStates[Enumerators.PlayerState.OnTutorial];
        CurrentState.OnEnter();
    }
}
