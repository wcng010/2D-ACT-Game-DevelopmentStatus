using System;
using C_Script.Common.Model.EventCentre;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace C_Script.Common.Model.Singleton
{
     public class Singleton<T> : MonoBehaviour where T : Singleton<T>
     { 
         [SerializeField] private bool isSaveNextScene; 
         private static T _instance;
         //private static bool applicationIsQuitting;
         public static T Instance
        {
            get
            {
                //if (applicationIsQuitting) return _instance;
                if (!_instance&&Application.isPlaying)
                {
                    _instance = FindObjectOfType<T>();
                    if (!_instance)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        obj.transform.SetParent(GameObject.FindWithTag("Singleton")?.transform);
                        obj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }
        
        protected virtual void Awake()
        {
            //SceneManager.activeSceneChanged += RefreshOnLoad;
            if (_instance == null)
            {
                _instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
            if (isSaveNextScene)
                DontDestroyOnLoad(gameObject);
        }
        
       /* private void RefreshOnLoad(Scene arg0, Scene arg1)
        {
            applicationIsQuitting = false;
        }
        */
     }
}