using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCallbackList
{
    public const int MAX_PRIORITY_COUNT = 11;

    private int invokeCount;
    private bool cacheDirty;
    private List<Action<GameEventBase>>[] callbackLists = new List<Action<GameEventBase>>[MAX_PRIORITY_COUNT];
    private List<Action<GameEventBase>>[] cacheCallbackLists = new List<Action<GameEventBase>>[MAX_PRIORITY_COUNT];

    public EventCallbackList()
    {
        for (int i = 0; i < MAX_PRIORITY_COUNT; i++)
        {
            callbackLists[i] = new List<Action<GameEventBase>>();
            cacheCallbackLists[i] = new List<Action<GameEventBase>>();
        }
    }

    public void Add(Action<GameEventBase> action, int priority)
    {
        var writeCallbacks = GetCallbacks();

        if (writeCallbacks[priority] == null)
            writeCallbacks[priority] = new();

        writeCallbacks[priority].Add(action);
    }

    public void Remove(Action<GameEventBase> action, int priority)
    {
        var writeCallbacks = GetCallbacks();

        if (writeCallbacks[priority] != null)
            writeCallbacks[priority].Remove(action);
    }

    public void Invoke(GameEventBase evt)
    {
        invokeCount++;
        for (int i = callbackLists.Length - 1; i >= 0; i--)
        {
            if (callbackLists[i] != null)
            {
                foreach (var callback in callbackLists[i])
                {
                    callback?.Invoke(evt);
                }
            }
        }
        invokeCount--;

        if (invokeCount == 0 && cacheDirty)
        {
            cacheDirty = false;
            for (int i = 0; i < cacheCallbackLists.Length; i++)
            {
                callbackLists[i].Clear();
                callbackLists[i].AddRange(cacheCallbackLists[i]);
                cacheCallbackLists[i].Clear();
            }
        }
    }

    private List<Action<GameEventBase>>[] GetCallbacks()
    {
        List<Action<GameEventBase>>[] list = null;
        if (invokeCount == 0)
        {
            list = callbackLists;
        }
        else
        {
            list = cacheCallbackLists;
            if (!cacheDirty)
            {
                cacheDirty = true;
                for (int i = 0; i < callbackLists.Length; i++)
                {
                    cacheCallbackLists[i].Clear();
                    cacheCallbackLists[i].AddRange(callbackLists[i]);
                }
            }
        }

        return list;
    }
}

public static class GameEventPriority
{
    public const int Low = 0;
    public const int Middle = 5;
    public const int High = 10;
}

public static class EventManager
{
    private struct EventInfo
    {
        public int eventId;
        public int priority;
        public Action<GameEventBase> action;

        public EventInfo(int eventId, int priority, Action<GameEventBase> action)
        {
            this.eventId = eventId;
            this.priority = priority;
            this.action = action;
        }
    }

    private static Dictionary<int, EventCallbackList> _events = new();
    private static Dictionary<Delegate, EventInfo> _findEvents = new();
    private static Dictionary<object, HashSet<Delegate>> _targetEvents = new();

    public static void Register<T>(Action<T> callback, int priority = GameEventPriority.Middle) where T : GameEvent<T>, new()
    {
        if (_findEvents.ContainsKey(callback))
            return;

        Action<GameEventBase> action = evt =>
        {
            callback?.Invoke(evt as T);
        };

        int eventId = GameEvent<T>.eventId;

        _findEvents.Add(callback, new EventInfo(eventId, priority, action));

        if (!_events.ContainsKey(eventId))
            _events.Add(eventId, new EventCallbackList());

        _events[eventId].Add(action, priority);

        object target = callback.Target;
        if (!_targetEvents.ContainsKey(target))
            _targetEvents.Add(target, new HashSet<Delegate>());
        _targetEvents[target].Add(callback);
    }

    public static void Unregister<T>(Action<T> callback) where T : GameEvent<T>, new()
    {
        if (!_findEvents.ContainsKey(callback))
            return;

        EventInfo info = _findEvents[callback];
        int eventId = GameEvent<T>.eventId;

        _events[eventId].Remove(info.action, info.priority);
        _targetEvents[callback.Target].Remove(callback);
        _findEvents.Remove(callback);
    }

    public static void UnregisterTarget(object target)
    {
        if (!_targetEvents.ContainsKey(target))
            return;

        foreach (var callback in _targetEvents[target])
        {
            var info = _findEvents[callback];
            _events[info.eventId].Remove(info.action, info.priority);
            _findEvents.Remove(callback);
        }

        _targetEvents.Remove(target);
    }

    public static void SendEvent<T>(T evt) where T : GameEvent<T>, new()
    {
        int eventId = GameEvent<T>.eventId;
        if (!_events.ContainsKey(eventId))
            return;

        _events[eventId].Invoke(evt);
    }
}