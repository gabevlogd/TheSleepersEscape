using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class represents the object observed by the observers
/// </summary>
/// <typeparam name="TDictionaryKeyType">The type of dictionary key</typeparam>
public class Observable<TDictionaryKeyType> : MonoBehaviour
{

    protected Dictionary<TDictionaryKeyType, List<IObserver>> m_observers;


    protected virtual void Awake()
    {
        m_observers = new Dictionary<TDictionaryKeyType, List<IObserver>>();
    }
    //public Observable(Dictionary<TDictionaryKeyType, List<IObserver>> observers = null)
    //{
    //    if (observers == null) m_observers = new Dictionary<TDictionaryKeyType, List<IObserver>>();
    //    else m_observers = observers;
    //}


    /// <summary>
    /// Registers a new obrever at this observable
    /// </summary>
    public virtual void Register(TDictionaryKeyType key, IObserver observerToRegister)
    {
        if (m_observers.ContainsKey(key))
            m_observers[key].Add(observerToRegister);
        else
        {
            m_observers.Add(key, new List<IObserver>());
            m_observers[key].Add(observerToRegister);
        }
    }

    /// <summary>
    /// Unregisters an obrever from this observable
    /// </summary>
    public virtual void Unregister(TDictionaryKeyType key, IObserver observerToUnregister)
    {
        if (m_observers.ContainsKey(key) && m_observers[key].Contains(observerToUnregister))
            m_observers[key].Remove(observerToUnregister);
        else return;
    }

    /// <summary>
    /// Sends a trigger notification to observers with the corresponding key
    /// </summary>
    /// <param name="key">key of the observers to be notified</param>
    public virtual void NotifyObservers(TDictionaryKeyType key, string message = null)
    {
        foreach (IObserver observer in m_observers[key])
            observer.UpdateObservers(message);
    }

    /// <summary>
    /// Sends a trigger notification to all observers
    /// </summary>
    public virtual void NotifyObservers(string message = null)
    {
        foreach (KeyValuePair<TDictionaryKeyType, List<IObserver>> keyValuePair in m_observers)
        {
            foreach (IObserver observer in keyValuePair.Value)
            {
                observer.UpdateObservers(message);
            }
        }
    }
}




