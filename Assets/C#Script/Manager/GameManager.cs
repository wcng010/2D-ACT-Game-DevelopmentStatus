using System;
using C_Script.Common.Model.Singleton;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace C_Script.Manager
{
    internal class GameManager : Singleton<GameManager>
    {
        private Transform _objectPoolTrans;
        public Transform ObjectPoolTrans
        {
            get
            {
                if (_objectPoolTrans == null)
                    _objectPoolTrans = GameObject.FindWithTag("ObjectPool")?.transform;
                return _objectPoolTrans;
            }
        }

        private CinemachineVirtualCamera _virtualCamera;

        public CinemachineVirtualCamera VirtualCamera
        {
            get
            {
                if (_virtualCamera == null)
                    _virtualCamera = GameObject.FindWithTag("VCM")?.GetComponent<CinemachineVirtualCamera>();
                return _virtualCamera;
            }
        }

        protected override void Awake()
        {
            _objectPoolTrans = GameObject.FindWithTag("ObjectPool")?.transform;
            _virtualCamera = GameObject.FindWithTag("VCM")?.GetComponent<CinemachineVirtualCamera>();
        }
    }
}
