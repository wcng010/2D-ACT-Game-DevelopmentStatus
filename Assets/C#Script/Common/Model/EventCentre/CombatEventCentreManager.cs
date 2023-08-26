using System.Collections.Generic;
using C_Script.Model.Singleton;
using UnityEngine;
using UnityEngine.Events;

namespace C_Script.Common.Model.EventCentre
{
    public class CombatEventCentreManager: Singleton<CombatEventCentreManager>
    {
     
        private static readonly IDictionary<CombatEventType, UnityEvent> Events =
            new Dictionary<CombatEventType, UnityEvent>(); //Events字典装有若干个事件，一一对应事件类型，
        public void Subscribe(CombatEventType eventType, UnityAction listener)
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

        public void Unsubscribe(CombatEventType eventType, UnityAction listener)
        {
            UnityEvent thisEvent;
            if (Events.TryGetValue(eventType, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public void Publish(CombatEventType eventType)
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
    }
}