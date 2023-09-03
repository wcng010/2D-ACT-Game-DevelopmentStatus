using System.Collections.Generic;
using C_Script.Common.Model.ObjectPool;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace C_Script.Player.Data
{
    [CreateAssetMenu(fileName = "Data",menuName = "Data/SkillBool")]
    public class SkillData : ScriptableObject
    {
        public SerializedDictionary<string,bool> skillBools = new SerializedDictionary<string, bool>();
    }
}
