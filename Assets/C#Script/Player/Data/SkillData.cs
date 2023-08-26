using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.Player.Data
{
    [CreateAssetMenu(fileName = "Data",menuName = "Data/SkillBool")]
    public class SkillData : ScriptableObject
    {
        public bool[] skillBools;
    }
}
