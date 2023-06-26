using UnityEngine;

/// <summary>
/// Basic state template 
/// </summary>
/// <typeparam name="TStateIDType">The type of the state ID</typeparam>
public class State<TStateIDType>
{
    public TStateIDType StateID;
    protected StatesMachine<TStateIDType> m_stateMachine;

    public State(TStateIDType stateID, StatesMachine<TStateIDType> stateMachine = null)
    {
        StateID = stateID;
        m_stateMachine = stateMachine;
    }

    public virtual void OnEnter()
    {
        Debug.Log("OnEnter " + StateID);

    }

    public virtual void OnUpdate()
    {
        //Debug.Log("OnUpadte " + StateID);
    }

    public virtual void OnExit()
    {
        //Debug.Log("OnExit " + StateID);
    }
}

