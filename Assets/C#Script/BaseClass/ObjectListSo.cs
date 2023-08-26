using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.BaseClass
{
    [CreateAssetMenu(fileName = "Data",menuName = "Data/ObjectList")]
    public class ObjectListSo : ScriptableObject
    { 
        public List<GameObject> objList;
    }
}
