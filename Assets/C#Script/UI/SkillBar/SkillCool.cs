using System;
using System.Collections;
using C_Script.Player.Data;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace C_Script.UI.SkillBar
{
    public abstract class SkillCool: MonoBehaviour
    {
        public virtual void UpdateSkillCool()
        {
        }
    }
}
