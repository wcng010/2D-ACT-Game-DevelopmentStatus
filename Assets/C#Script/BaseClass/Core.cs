using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.BaseClass
{
    public class Core: MonoBehaviour
    {
        private readonly List<CoreComponent> _coreComponents = new List<CoreComponent>();
        private void Awake()
        {
            
        }
        //遍历核心组件，顺序调用其Update函数
        public void LogicUpdate()
        {
            foreach (CoreComponent component in _coreComponents)
            {
                component.LogicUpdate();
            }
        }
        public void AddComponent(CoreComponent component)
        {
            if (!_coreComponents.Contains(component))
            {
                _coreComponents.Add(component);
            }
        }

        public T GetCoreComponent<T>() where T : CoreComponent
        {
            //查找列表中符合要求的CoreComponent
            var comp = _coreComponents.OfType<T>().FirstOrDefault();

            if (comp)
                return comp;

            comp = GetComponentInChildren<T>();

            if (comp)
                return comp;

            Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
            return null;
        }

        public T GetCoreComponent<T>([NotNull] ref T value) where T : CoreComponent
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            value = GetCoreComponent<T>();
            return value;
        }
    }
}
