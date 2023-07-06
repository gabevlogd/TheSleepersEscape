using UnityEditor;
using UnityEngine;

public class RunningPuzzle : PlayerState
{
    public RunningPuzzle(Enumerators.PlayerState stateID, StatesMachine<Enumerators.PlayerState> stateMachine = null) : base(stateID, stateMachine)
    {
    }
}