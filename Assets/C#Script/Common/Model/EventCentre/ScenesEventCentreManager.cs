using System;
using System.Collections.Generic;
using C_Script.Common.Model.Singleton;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace C_Script.Common.Model.EventCentre
{
    public class ScenesEventCentreManager : Singleton<ScenesEventCentreManager>
    {
     
        private static readonly IDictionary<ScenesEventType, UnityEvent> Events =
            new Dictionary<ScenesEventType, UnityEvent>(); //Events字典装有若干个事件，一一对应事件类型，
        
        private UnityAction<Scene, Scene> LoadSceneEvent = (scene1, scene2) => { };
        public void Subscribe(ScenesEventType eventType, UnityAction listener)
        {
            UnityEvent thisEvent; //事件
            if (Events.TryGetValue(eventType, out thisEvent))
            {
                thisEvent.AddListener(listener); //向事件中添加函数
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                Events.Add(eventType, thisEvent);
            }
        }

        public void Unsubscribe(ScenesEventType eventType, UnityAction listener)
        {
            UnityEvent thisEvent;
            if (Events.TryGetValue(eventType, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public void Publish(ScenesEventType eventType)
        {
            UnityEvent thisEvent;
            if (Events.TryGetValue(eventType, out thisEvent))
            {
                thisEvent.Invoke();
            }
            else
            {
                Debug.LogError(eventType + "isn't Exist");
            }
        }

        public void AddLoadSceneEvent(UnityAction<Scene, Scene> action)
        {
            Array actionArray = LoadSceneEvent.GetInvocationList();
            if (Array.IndexOf(actionArray, action) != -1)
            {
                SceneManager.activeSceneChanged -= LoadSceneEvent;
                LoadSceneEvent += action;
                SceneManager.activeSceneChanged += LoadSceneEvent;
            }
        }
    }
}
