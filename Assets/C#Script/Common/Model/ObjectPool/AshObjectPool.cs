using C_Script.Common.Interface;
using UnityEngine;

namespace C_Script.Common.Model.ObjectPool
{
    [RequireComponent(typeof(PoolPart))]
    public class AshObjectPool : PartObjectPool<AshObjectPool>,IObjectPool
    {
        protected override void Awake()
        {
            base.Awake();
        }
        public Transform SetFollower()
        {
            return GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
