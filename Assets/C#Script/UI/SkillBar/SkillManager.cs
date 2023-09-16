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
    }
}
