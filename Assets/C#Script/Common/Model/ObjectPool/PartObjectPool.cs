using System.Collections;
using System.Collections.Generic;
using C_Script.Common.Model.Singleton;
using UnityEngine;

namespace C_Script.Common.Model.ObjectPool
{
    public class PartObjectPool<T> :Singleton<T> where T : PartObjectPool<T>
    {
        private Dictionary<string, GameObject> _objectPool = new Dictionary<string, GameObject>();
        protected override void Awake()
        {
            base.Awake();
        }
        public GameObject GetObject(string objName)
        {
            if (_objectPool == null) _objectPool = new Dictionary<string, GameObject>();
            if (_objectPool.ContainsKey(objName))
            {
                return _objectPool[objName];
            }
            return null;
        }

        public void PushObject(GameObject obj)
        {
            if (_objectPool == null) _objectPool = new Dictionary<string, GameObject>();
            if (!_objectPool.ContainsKey(obj.name))
            {
                _objectPool.Add(obj.name,obj);
                obj.SetActive(false);
            }
        }
        
        public void SetActive(string objName)
        {
            if (_objectPool.Count == 0) _objectPool = new Dictionary<string, GameObject>();
            string realName = objName + "(Clone)";
            if (_objectPool.ContainsKey(realName))
            {
                _objectPool[realName].SetActive(false);
                _objectPool[realName].SetActive(true);
            }
        }
        
        public void Setfalse(string objName)
        {
            if (_objectPool.Count == 0) _objectPool = new Dictionary<string, GameObject>();
            string realName = objName + "(Clone)";
            if (_objectPool.ContainsKey(realName))
                _objectPool[realName].SetActive(false);
        }
    }
}
