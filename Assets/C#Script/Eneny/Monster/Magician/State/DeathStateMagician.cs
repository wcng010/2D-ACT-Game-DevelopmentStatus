using C_Script.Eneny.Monster.Magician.BaseClass;
using C_Script.Eneny.Monster.Magician.State.StateBase;
using C_Script.Player.StateModel.BaseState;
using UnityEngine;

namespace C_Script.Eneny.Monster.Magician.State
{
    public class DeathStateMagician:MagicianState
    {
        public override void Enter()
        {
            base.Enter();
            Collider2DOwner.size = new Vector2(0.2f, 0.2f);
            AfterDeath();
        }

        public override void PhysicExcute()
        {
            Collider2DOwner.offset = new Vector2(0, 0);
        }

        public override void LogicExcute()
        {

        }
        
        private void AfterDeath()
        {
            MagicianData.IsDeath = true;
            Owner.gameObject.layer = LayerMask.NameToLayer("Dead");
            Owner.gameObject.tag = "Untagged";
        }

        public DeathStateMagician(MagicianBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            
        }
    }
}