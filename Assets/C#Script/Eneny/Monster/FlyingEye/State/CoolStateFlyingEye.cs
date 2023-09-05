using C_Script.BaseClass;
using C_Script.Eneny.Monster.FlyingEye.Base;
using C_Script.Eneny.Monster.FlyingEye.State.StateBase;
using UnityEngine;

namespace C_Script.Eneny.Monster.FlyingEye.State
{
    public class CoolStateFlyingEye : FlyingEyeState
    {

        public override void Enter()
        {
            base.Enter();
            Rigidbody2DOwner.velocity = Vector2.zero;
        }

        public override void LogicExcute()
        {
            base.LogicExcute();
        }


        public CoolStateFlyingEye(FlyingEyeBase owner, string nameToTrigger, string animationName) : base(owner, nameToTrigger, animationName)
        {
            
        }
        
    }
}