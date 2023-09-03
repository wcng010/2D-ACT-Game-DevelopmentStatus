using System;
using UnityEngine;

namespace C_Script.Test
{
    public class Test : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Awake");
        }

        private void OnEnable()
        {
            Debug.Log("OnEnable");
        }

        void Start()
        {
            Debug.Log("Start");
        }

        private void OnDisable()
        {
            Debug.Log("OnDisable");
        }

        private void OnDestroy()
        {
            Debug.Log("OnDestroy");
        }


    }
}
