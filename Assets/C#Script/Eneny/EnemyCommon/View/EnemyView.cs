using System;
using UnityEngine;
using UnityEngine.Events;

namespace C_Script.Eneny.EnemyCommon.View
{
    public class EnemyView : BaseClass.View
    {
        //暴击
        [NonSerialized]public UnityEvent EnemyHurtCrit = new ();
        //不暴击
        [NonSerialized] public UnityEvent EnemyHurtNoCrit = new();

        [NonSerialized]public UnityEvent EnemyDeath = new ();
    }
}