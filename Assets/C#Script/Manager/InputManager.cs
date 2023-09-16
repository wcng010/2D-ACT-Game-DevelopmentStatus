using System;
using System.Collections.Generic;
using C_Script.Common.Model.Singleton;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace C_Script.Manager
{
    public class InputManager : Singleton<InputManager>
    {
        private PlayerInput _playerInput;
        public int InputX { get; private set; }
        public int InputY { get; private set; }
        public bool InputSpace { get; private set; }
        public bool InputQ { get; private set; }
        public bool InputJ { get; set; }
        public bool InputK { get; private set; }
        public bool Alpha1 { get; private set; }


        #region Event

        [NonSerialized]public UnityEvent KeyEventQ = new ();

        [NonSerialized]public UnityEvent KeyEventAlpha1 = new ();

        #endregion
        
        private void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            
        }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            var rawMovementInput = context.ReadValue<Vector2>();
            InputX = Mathf.RoundToInt(rawMovementInput.x);
            InputY = Mathf.RoundToInt(rawMovementInput.y);
        }
        
        
        public void OnInput_Space(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                InputSpace = true;
            }

            if (context.canceled)
            {
                InputSpace = false;
            }
        }
        
        public void OnInput_Q(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                InputQ = true;
                KeyEventQ?.Invoke();
            } 
            if (context.canceled)
            {
                InputQ = false;
            }
        }
        public void OnInput_J(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                InputJ = true;
            } 
            if (context.canceled)
            {
                InputJ = false;
            }
        }
        
        public void OnInput_K(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                InputK = true;
            } 
            if (context.canceled)
            {
                InputK = false;
            }
        }

        public void OnInput_Alpha1(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Alpha1 = true;
                KeyEventAlpha1?.Invoke();
            }
            if (context.canceled)
            {
                Alpha1 = false;
            }
        }
    }
}