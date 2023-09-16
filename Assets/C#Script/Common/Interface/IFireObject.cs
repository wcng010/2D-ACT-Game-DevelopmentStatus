using System;
using C_Script.Common.Model.ObjectPool;
using UnityEditor;
using UnityEngine;

namespace C_Script.Interface
{
    public interface IFireObject  
    {
        ObjectType FireObjectType();
        void FireObject();
    }
}
