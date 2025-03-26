using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

public enum EventName
{
    OnHealthChanged,
    OnDead,
}

public static class Event_Manager
{
    private static Dictionary<EventName, Action<object>> eventDictionary = new();

    public static void Subscribe(EventName eventName, Action<object> listener)
    {
        if (!eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] = null;
        }
        eventDictionary[eventName] += listener;
    }

    public static void Subscribe(EventName eventName, Action listener)
    {
        Subscribe(eventName, _ => listener());
    }

    public static void Unsubscribe(EventName eventName, Action<object> listener )
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] -= listener;
            if (eventDictionary[eventName] == null)
            {
                eventDictionary.Remove(eventName);
            }
        }
    }

    public static void Unsubscribe(EventName eventName, Action listener)
    {
        Unsubscribe(eventName, _ => listener());
    }

    public static void SentEvent(EventName eventName, object eventData = null)
    {
        if (eventDictionary.TryGetValue(eventName, out var listener ))
        {
            listener?.Invoke(eventData);
        }
    }
}