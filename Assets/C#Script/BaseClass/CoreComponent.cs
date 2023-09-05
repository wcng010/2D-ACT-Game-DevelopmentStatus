using System;
using Cinemachine;
using UnityEngine;

namespace C_Script.BaseClass
{
    public class CoreComponent : MonoBehaviour 
    {
        protected Core Core;

        protected Model Model;

        protected View View;
        protected virtual void Awake()
        {
            Model = transform.GetComponentInParent<Model>();
            Core = transform.GetComponentInParent<Core>();
            View = transform.GetComponentInParent<View>();
            if(!Core) { Debug.LogError("There is no Core on the parent"); }
            Core.AddComponent(this);
        }
        public virtual void LogicUpdate() { }
    }
}
