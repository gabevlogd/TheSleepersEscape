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

        CurrentState = AllStates[Enumerators.PlayerState.Navigation];
        CurrentState.OnEnter();
    }
}
