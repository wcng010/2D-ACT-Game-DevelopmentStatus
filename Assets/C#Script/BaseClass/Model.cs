using System;
using C_Script.Eneny.Monster.FlyingEye.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.BaseClass
{
    public enum ForceDirection
    {
        None,
        Up,
        Down,
        Forward
    }


    public class Model:MonoBehaviour
    {
        [field: FoldoutGroup("UnityComponent")] [field: SerializeField] private AttackObjectDataSo data;
        protected AttackObjectDataSo Data =>_dataSo? _dataSo :_dataSo = Instantiate(data);

        private AttackObjectDataSo _dataSo;
        
        [field:FoldoutGroup("Custom")] [field: SerializeField] public View View { get; private set; }
        [field:FoldoutGroup("Custom")] [field: SerializeField] public Controller Controller { get; private set; }
    }
}