using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour
{
    void Start()
    {
        EventManager.Register<TestEvent>(Test1, GameEventPriority.High);
        EventManager.Register<TestEvent>(Test2, GameEventPriority.Middle);
        EventManager.Register<TestEvent>(Test3, GameEventPriority.Low);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("·¢ËÍ");
            using TestEvent evt = TestEvent.Get();
            evt.message = "test";
            EventManager.SendEvent(evt);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            EventManager.Unregister<TestEvent>(Test2);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            EventManager.UnregisterTarget(this);
        }
    }

    private void Test1(TestEvent evt)
    {
        Debug.Log($"Test1:{evt.message}");
    }

    private void Test2(TestEvent evt)
    {
        Debug.Log($"Test2:{evt.message}");
    }

    private void Test3(TestEvent evt)
    {
        Debug.Log($"Test3:{evt.message}");
    }

    private void Test4(TestEvent evt)
    {
        Debug.Log($"Test4:{evt.message}");
    }
}
