using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

public static class GameEventPool<T> where T : new()
{
    private static HashSet<T> _poolSet = new HashSet<T>();
    private static Stack<T> _pool = new Stack<T>();
    private static Action<T> _init;

    static GameEventPool()
    {
        Type type = typeof(T);
        FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

        List<Expression> expressions = new List<Expression>();
        ParameterExpression param = Expression.Parameter(typeof(T), "value");
        foreach (FieldInfo fieldInfo in fieldInfos)
        {
            var field = Expression.Field(param, fieldInfo);
            expressions.Add(Expression.Assign(field, Expression.Default(fieldInfo.FieldType)));
        }

        var body = Expression.Block(expressions);
        _init = Expression.Lambda<Action<T>>(body, param).Compile();
    }

    public static void Push(T item)
    {
        if (_poolSet.Contains(item)) return;

        _init.Invoke(item);
        _poolSet.Add(item);
        _pool.Push(item);
    }

    public static T Get()
    {
        if (_pool.Count > 0)
        {
            return _pool.Pop();
        }

        return new T();
    }
}

public class GameEventBase
{
    private static int _eventCount = 0;
    public static int GetEventId()
    {
        return ++_eventCount;
    }
}

public class GameEvent<T> : GameEventBase, IDisposable where T : GameEvent<T>, new()
{
    public static int eventId;

    static GameEvent()
    {
        eventId = GetEventId();
    }

    public void Dispose()
    {
        GameEventPool<T>.Push((T)this);
    }

    public static T Get()
    {
        return GameEventPool<T>.Get();
    }

    public static void Push(T item)
    {
        GameEventPool<T>.Push(item);
    }
}
