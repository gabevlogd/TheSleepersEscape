using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StatesMachine<Enumerators.PlayerState>
{
    protected override void InitStates()
    {
        AllStates.Add(Enumerators.PlayerState.Navigation, new Navigation(Enumerators.PlayerState.Navigation));
        AllStates.Add(Enumerators.PlayerState.RunningPuzzle, new RunningPuzzle(Enumerators.PlayerState.RunningPuzzle));
        AllStates.Add(Enumerators.PlayerState.RunningInteractable, new RunningInteractable(Enumerators.PlayerState.RunningInteractable));
        AllStates.Add(Enumerators.PlayerState.OnInventory, new OnInventory(Enumerators.PlayerState.OnInventory));

        CurrentState = AllStates[Enumerators.PlayerState.Navigation];
        CurrentState.OnEnter();
    }
}
