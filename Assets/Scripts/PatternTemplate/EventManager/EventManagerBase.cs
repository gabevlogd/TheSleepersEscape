using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerBase<TKeyEvent> 
{
    /// <summary>
    /// my eventMap i use dictionary to have more control
    /// </summary>
    private Dictionary<TKeyEvent, List<Action>> m_eventMap = new Dictionary<TKeyEvent, List<Action>>();

    /// <summary>
    /// register the events 
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="observer"></param>
    public void Registrer(TKeyEvent eventName, Action observer)
    {
        if (!ValidPreCondition(eventName, observer)) return;

        if (!m_eventMap.ContainsKey(eventName))
            m_eventMap.Add(eventName, new List<Action>());

        m_eventMap[eventName].Add(observer);
        //Debug.Log("Someone is register");
        //Debug.Log(eventName);

    }

    /// <summary>
    /// UnRegister the events 
    /// I still haven't figured out when to use it 
    /// i implemented for this template class
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="observer"></param>
    public void Unregistrer(TKeyEvent eventName, Action observer)
    {
        if (!ValidPreCondition(eventName, observer)) return;

        if (!m_eventMap.ContainsKey(eventName)) return;

        List<Action> event_observer = m_eventMap[eventName];

        event_observer.Remove(observer);
    }

    /// <summary>
    /// the function to Trigger the event in a specific situation
    /// </summary>
    /// <param name="eventName"></param>
    public void TriggerEvent(TKeyEvent eventName)
    {
        if (m_eventMap.ContainsKey(eventName))
        {
            List<Action> event_observer = m_eventMap[eventName];

            foreach (Action notify in event_observer)
                notify.Invoke();

            Debug.Log("Event: " + eventName + " triggered");
        }
    }

    /// <summary>
    /// valid PreCondition
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="observer"></param>
    /// <returns></returns>
    private bool ValidPreCondition(TKeyEvent eventName, Action observer)
    {
        if (observer == null) return false;

        if (string.IsNullOrEmpty(eventName.ToString())) return false;

        return true;
    }

}




