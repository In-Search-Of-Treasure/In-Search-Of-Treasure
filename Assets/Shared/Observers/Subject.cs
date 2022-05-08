using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    private List<Observer> observers = new List<Observer>();

    public void AddObserver(Observer obs)
    {
        observers.Add(obs);
    }

    public void Notify(object value, NotificationType notificationType)
    {
        foreach (var obs in observers)
        {
            obs.OnNotify(value, notificationType);
        }
    }
}
