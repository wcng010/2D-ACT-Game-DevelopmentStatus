using C_Script.Eneny.Monster.FlyingEye.Base;
using C_Script.Eneny.Monster.FlyingEye.State.StateBase;
using UnityEngine;

namespace C_Script.Eneny.Monster.FlyingEye.State
{
    public class HurtStateFlyingEye:FlyingEyeState
    {

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
            Rigidbody2DOwner.velocity = Vector2.zero;
            FlyingEyeData.IsHurt = false;
        }

        public HurtStateFlyingEye(FlyingEyeBase owner, string nameToTrigger, string animationName) : base(owner, nameToTrigger, animationName)
        {
        }
    }
}