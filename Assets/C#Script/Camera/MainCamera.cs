using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.Camera
{
    public class MainCamera : MonoBehaviour
    {
        private Transform _playerTrans;
        [SerializeField]
        private float xAxis;
        [SerializeField]
        private float yAxis;
        private void Awake()
        {
            _playerTrans = GameObject.FindWithTag("Player").transform;
        }

        private void LateUpdate()
        {
            var position = _playerTrans.position;
            transform.position = new Vector3(position.x+xAxis,position.y+yAxis,-10);
        }
    }
}
