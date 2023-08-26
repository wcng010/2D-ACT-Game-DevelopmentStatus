using UnityEngine;

namespace C_Script.Eneny.Boss.SwordSaint.State
{
    public class DeathStateSwordSaint : SwordSaintState
    {
        public override void Enter()
        {
            base.Enter();
            AfterDeath();
        }

        public override void PhysicExcute()
        {
           
        }

        public override void LogicExcute()
        {

        }
        
        private void AfterDeath()
        {
            SwordSaintData.IsDeath = true;
            Owner.gameObject.layer = LayerMask.NameToLayer("Dead");
            Owner.gameObject.tag = "Untagged";
        }
        public DeathStateSwordSaint(SwordSaintBase owner, string nameToTrigger, string animationName) : base(owner, nameToTrigger, animationName)
        {
        }
    }
}