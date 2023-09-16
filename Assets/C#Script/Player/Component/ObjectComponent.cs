using System;
using System.Collections.Generic;
using C_Script.BaseClass;
using C_Script.Common.Model.ObjectPool;
using C_Script.Interface;
using Mono.Cecil;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.Player.Component
{
    public class ObjectComponent : CoreComponent
    {
        [SerializeReference] public Dictionary<ObjectType, GameObject> ObjectDictionary = new Dictionary<ObjectType, GameObject>();
        public ObjectListSo objectListSo;
        public void FireObject(ObjectType objectType) =>
            ObjectDictionary?[objectType].GetComponent<IFireObject>().FireObject();
        private void Start()
        {
            foreach (var obj in objectListSo.objList)
            {
                ObjectDictionary.Add(obj.GetComponent<IFireObject>().FireObjectType(),obj);
                BigObjectPool.Instance.PushObject(obj.GetComponent<IFireObject>().FireObjectType(),Instantiate(obj));
            }
        }
    }
}
