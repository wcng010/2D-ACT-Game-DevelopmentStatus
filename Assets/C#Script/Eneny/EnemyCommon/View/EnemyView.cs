using System;
using UnityEngine;
using UnityEngine.Events;

namespace C_Script.Eneny.EnemyCommon.View
{
    public class EnemyView : BaseClass.View
    {
        [NonSerialized]public UnityEvent EnemyHurt = new ();

        [NonSerialized]public UnityEvent EnemyDeath = new ();
    }
}