using C_Script.Eneny.Monster.FlyingEye.Base;
using C_Script.Eneny.Monster.FlyingEye.State.StateBase;
using UnityEngine;

namespace C_Script.Eneny.Monster.FlyingEye.State
{
    public class DeathStateFlyingEye:FlyingEyeState
    {
        public override void Enter()
        {
            base.Enter();
            AfterDeath();
        }
        public override void LogicExcute()
        {
            
        }
        
        private void AfterDeath()
        {
            Rigidbody2DOwner.velocity = Vector2.zero;
            Rigidbody2DOwner.gravityScale = 1.5f;
            Collider2DOwner.isTrigger = false;
            Collider2DOwner.size = new Vector2(0.17f, 0.17f);
            Owner.gameObject.layer = LayerMask.NameToLayer("Dead");
            Owner.gameObject.tag = "Untagged";
        }

        public DeathStateFlyingEye(FlyingEyeBase owner, string nameToTrigger, string animationName) : base(owner, nameToTrigger, animationName)
        {
        }
    }
}