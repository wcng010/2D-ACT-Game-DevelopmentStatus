using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace C_Script.Common.DataStructure
{
    public class MyDictionary<TKey, TValue>
    {
        [SerializeReference] private List<KeyValuePair<TKey, TValue>> _list;
        
        public MyDictionary()
        {
            _list = new List<KeyValuePair<TKey, TValue>>();
        }

        public void Add(TKey key, TValue value)
        {
            _list.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public bool ContainsKey(TKey key)
        {
            foreach (var pair in _list)
            {
                if (pair.Key.Equals(key))
                {
                    return true;
                }
            }
            return false;
        }

        public TValue this[TKey key]
        {
            get
            {
                foreach (var pair in _list)
                {
                    if (pair.Key.Equals(key))
                    {
                        return pair.Value;
                    }
                }
                throw new KeyNotFoundException();
            }
            set
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    if (_list[i].Key.Equals(key))
                    {
                        _list[i] = new KeyValuePair<TKey, TValue>(key, value);
                        return;
                    }
                }
                Add(key, value);
            }
        }

        public bool Remove(TKey key)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i].Key.Equals(key))
                {
                    _list.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
    }
}