using C_Script.Eneny.Monster.Magician.BaseClass;
using C_Script.Eneny.Monster.Magician.State.StateBase;
using C_Script.Player.StateModel.BaseState;
using UnityEngine;

namespace C_Script.Eneny.Monster.Magician.State
{
    public class HurtStateMagician:MagicianState
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void Enter()
        {
            base.Enter();
        }

        public override void PhysicExcute()
        {
            SetColliderOffset(Collider2DOwner,new Vector2(0,0));
        }

        public override void LogicExcute()
        {
            base.LogicExcute();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public HurtStateMagician(MagicianBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            
        }
    }
}