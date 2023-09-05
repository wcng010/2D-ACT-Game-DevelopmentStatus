using System;
using System.Collections.Generic;
using C_Script.Player.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace C_Script.UI.SkillBar
{
    public class SkillManager : MonoBehaviour
    {
       [SerializeField] public SkillData skillData;
       private Dictionary<KeyCode, SkillCool> _skillTable = new ();

       private void Awake()
       {
           SkillCool[] components = GetComponentsInChildren<SkillCool>();
           for (int i = 0; i < components.Length; ++i)
           {
               _skillTable.Add(components[i].keyCode,components[i]);
           }
       }

       private void Update()
       {
           KeyCode keyCode = Event.current.keyCode;
           foreach (var skill in _skillTable)
           {
               if(skill.Key == keyCode)
                   // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                   skill.Value.UpdateSkillCool();
           }
       }
    }
}
