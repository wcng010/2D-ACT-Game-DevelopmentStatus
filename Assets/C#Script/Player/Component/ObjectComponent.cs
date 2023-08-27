using System;
using System.Collections.Generic;
using C_Script.BaseClass;
using C_Script.Interface;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.Player.Component
{
    public class ObjectComponent : CoreComponent
    {
        [SerializeReference] public Dictionary<String, GameObject> ObjectDictionary = new Dictionary<string, GameObject>();
        public ObjectListSo objectListSo;
        public void FireObject(String objectName, Transform ownerTrans) =>
            ObjectDictionary?[objectName].GetComponent<IFireObject>().FireObject(ownerTrans);
        private void Start()
        {
            foreach (var obj in objectListSo.objList)
            {
                ObjectDictionary.Add(obj.GetComponent<IFireObject>().ObjectNames(),obj.GetComponent<IFireObject>().GameObj());      
            }
        }
    }
}
