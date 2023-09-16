using System;
using System.Collections.Generic;
using C_Script.Common.Model.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace C_Script.Common.Model.ObjectPool
{
    public enum ObjectType
    {
        Skull,
        DashImage,
        WaterWave,
        HitEffect1,
        HitEffect2,
        HitEffect3
    }

    public class BigObjectPool : Singleton<BigObjectPool>
    {
        [SerializeReference]
        private Dictionary<ObjectType, List<GameObject>> _objectPool = new Dictionary<ObjectType, List<GameObject>>();
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
            if(!IsEmpty(type))
                _objectPool.Add(type,new List<GameObject>());
            _objectPool[type].Add(obj);
            obj.transform.SetParent(transform);
            obj.SetActive(false);
        }

        public void PushEmptyPool(ObjectType type, GameObject obj)
        {
            if (!IsEmpty(type))
            {
                GameObject gameObj = Instantiate(obj, transform, true);
                _objectPool.Add(type, new List<GameObject>());
                _objectPool[type].Add(gameObj);
                gameObj.SetActive(false);
            }
        }

        public bool IsEmpty(ObjectType type) => _objectPool.ContainsKey(type);
        
        public GameObject SetOneActive(ObjectType type)
        {
            if (_objectPool.ContainsKey(type))
            {
                foreach (var obj in _objectPool[type])
                {
                    if (obj.activeSelf == false)
                    {
                        obj.SetActive(true);
                        return obj;
                    }
                }
                try
                {
                    PushObject(type,GameObject.Instantiate(_objectPool[type][0]));
                    return SetOneActive(type);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e+"ObjectPool is full");
                    throw;
                }
            }
            Debug.LogError("No Things in the ObjectPool");
                return null;
        }
        public void SetAllActive(ObjectType type)
        {
            if (_objectPool.ContainsKey(type))
            {
                foreach (var obj in _objectPool[type])
                {
                    obj.SetActive(true);
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
