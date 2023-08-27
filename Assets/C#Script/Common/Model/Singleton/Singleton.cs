using System;
using Unity.Burst;
using UnityEngine;

namespace C_Script.Model.Singleton
{
     public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;
        private static bool applicationIsQuitting = false;
        public static T Instance
        {
            get
            {
                if (applicationIsQuitting) return _instance;
                    if (!_instance&&Application.isPlaying)
                    {
                        _instance = FindObjectOfType<T>();
                        if (!_instance)
                        {
                            GameObject obj = new GameObject();
                            obj.name = typeof(T).Name;
                            obj.AddComponent<T>();
                        }
                    }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected void OnDestroy()
        {
            applicationIsQuitting = true;
        }
    }
}