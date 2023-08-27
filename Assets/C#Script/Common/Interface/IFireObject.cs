using System;
using UnityEditor;
using UnityEngine;

namespace C_Script.Interface
{
    public interface IFireObject  
    {
        String ObjectNames();
        GameObject GameObj();
        void FireObject(Transform ownerTrans);
    }
}
