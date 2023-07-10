using System;
using System.Collections.Generic;


public class SoundEventManager<TKeyEvent>
{

    /// <summary>
    /// dictionary for one param (da testare)
    /// </summary>
    private Dictionary<TKeyEvent, Delegate> m_eventMapOneParam = new Dictionary<TKeyEvent, Delegate>();


    /// <summary>
    /// register the events one param (da testare)
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="observer"></param>
    public void Register<T>(TKeyEvent eventName, Action<T> observer)
    {
        if (m_eventMapOneParam.ContainsKey(eventName))
            m_eventMapOneParam[eventName] = Delegate.Combine(m_eventMapOneParam[eventName], observer);
        else
            m_eventMapOneParam.Add(eventName, observer);

    }



    /// <summary>
    /// UnRegister the events 
    /// i implemented for this template class (da testare)
    /// one param
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="observer"></param>
    public void Unregister<T>(TKeyEvent eventName, Action<T> observer)
    {
        if (m_eventMapOneParam.ContainsKey(eventName))
            m_eventMapOneParam[eventName] = Delegate.Remove(m_eventMapOneParam[eventName], observer);

    }




    /// <summary>
    /// the function to Trigger the event in a specific situation with a parameter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="eventName"></param>
    /// <param name="parameter"></param>
    public void TriggerEvent<T>(TKeyEvent eventName, T parameter)
    {
        if (m_eventMapOneParam.ContainsKey(eventName))
        {
            Action<T> action = m_eventMapOneParam[eventName] as Action<T>;
            action.Invoke(parameter);
        }
    }




}
