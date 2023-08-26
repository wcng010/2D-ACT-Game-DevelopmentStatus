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
            SwitchState();
        }


        public CoolStateFlyingEye(FlyingEyeBase owner, string nameToTrigger, string animationName) : base(owner, nameToTrigger, animationName)
        {
            
        }

        private void SwitchState()
        {
            if (FlyingEyeData.IsDeath)
            {
                StateMachine.ChangeState(FlyingEyeDic[EnemyStateType.DeathStateEnemy]);
                return;
            }
            if (FlyingEyeData.IsHurt)
            {
                StateMachine.ChangeState(FlyingEyeDic[EnemyStateType.HurtStateEnemy]);
                return;
            }
            if (FlyingEyeData.IsTargetDeath)
            {
                StateMachine.RevertOrinalState();
            }
        }
    }
}