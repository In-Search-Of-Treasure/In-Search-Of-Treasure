using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    private List<Observer> observers = new List<Observer>();

    public void AddObserver(Observer obs)
    {
        observers.Add(obs);
    }

    public void Notify(NotificationType notificationType, object value = null)
    {
        foreach (var obs in observers)
        {
            obs.OnNotify(notificationType, value);
        }
    }
}
