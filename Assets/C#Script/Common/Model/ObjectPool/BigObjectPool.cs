using System.Collections.Generic;
using C_Script.Model.Singleton;
using UnityEngine;

namespace C_Script.Common.Model.ObjectPool
{
    public enum ObjectType
    {
        Skull
    }
    public class BigObjectPool : Singleton<BigObjectPool>
    {
        [SerializeReference]private Dictionary<ObjectType, List<GameObject>> _objectPool = new Dictionary<ObjectType, List<GameObject>>();
        
        public List<GameObject> GetObject(ObjectType type)
        {
            if (_objectPool.ContainsKey(type))
            {
                return _objectPool[type];
            }
            return null;
        }

        public void PushObject(ObjectType type, GameObject obj)
        {
            if(!_objectPool.ContainsKey(type))
                _objectPool.Add(type,new List<GameObject>());
            _objectPool[type].Add(obj);
        }
        
        public void SetActive(ObjectType type)
        {
            if (_objectPool.ContainsKey(type))
            {
                foreach (var obj in _objectPool[type])
                {
                    obj.SetActive(true);
                    Debug.Log(obj.name);
                }
            }

        }
        
        public void SetFalse(ObjectType type)
        {
            if (_objectPool.ContainsKey(type))
            {
                foreach (var obj in _objectPool[type])
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}
