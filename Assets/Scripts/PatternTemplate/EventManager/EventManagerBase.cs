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
    /// dictionary for one param (da testare)
    /// </summary>
    //private Dictionary<TKeyEvent, Delegate> m_eventMapOneParam = new Dictionary<TKeyEvent, Delegate>();

    /// <summary>
    /// register the events 
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="observer"></param>
    public void Register(TKeyEvent eventName, Action observer)
    {
        if (!ValidPreCondition(eventName, observer)) return;

        if (!m_eventMap.ContainsKey(eventName))
            m_eventMap.Add(eventName, new List<Action>());

        m_eventMap[eventName].Add(observer);
        //Debug.Log("Someone is register");
        //Debug.Log(eventName);

    }

    /// <summary>
    /// register the events one param (da testare)
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="observer"></param>
    //public void Register<T>(TKeyEvent eventName, Action<T> observer)
    //{
    //    if (m_eventMapOneParam.ContainsKey(eventName))
    //        m_eventMapOneParam[eventName] = Delegate.Combine(m_eventMapOneParam[eventName], observer);
    //    else
    //        m_eventMapOneParam.Add(eventName, observer);

    //}


    /// <summary>
    /// UnRegister the events 
    /// i implemented for this template class
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="observer"></param>
    public void Unregister(TKeyEvent eventName, Action observer)
    {
        if (!ValidPreCondition(eventName, observer)) return;

        if (!m_eventMap.ContainsKey(eventName)) return;

        List<Action> event_observer = m_eventMap[eventName];

        event_observer.Remove(observer);
    }


    /// <summary>
    /// UnRegister the events 
    /// i implemented for this template class (da testare)
    /// one param
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="observer"></param>
    //public void Unregister<T>(TKeyEvent eventName, Action<T> observer)
    //{
    //    if (m_eventMapOneParam.ContainsKey(eventName))
    //        m_eventMapOneParam[eventName] = Delegate.Remove(m_eventMapOneParam[eventName], observer);

    //}



    /// <summary>
    /// the function to Trigger the event in a specific situation
    /// </summary>
    /// <param name="eventName"></param>
    public void TriggerEvent(TKeyEvent eventName)
    {
        if (m_eventMap.ContainsKey(eventName))
        {
            List<Action> event_observer = m_eventMap[eventName];

            Debug.Log("Event: " + eventName + " triggered");

            foreach (Action notify in event_observer)
                notify.Invoke();
        }
        else Debug.Log("Event: " + eventName + " does not exist");
    }

    /// <summary>
    /// the function to Trigger the event in a specific situation with a parameter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="eventName"></param>
    /// <param name="parameter"></param>
    //public void TriggerEvent<T>(TKeyEvent eventName, T parameter)
    //{
    //    if (m_eventMapOneParam.ContainsKey(eventName))
    //    {
    //        Action<T> action = m_eventMapOneParam[eventName] as Action<T>;
    //        action.Invoke(parameter);
    //    }
    //}


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




