using System;
using C_Script.BaseClass;
using C_Script.Eneny.Monster.FlyingEye.Base;
using C_Script.Eneny.Monster.FlyingEye.State.StateBase;
using UnityEngine;
namespace C_Script.Eneny.Monster.FlyingEye.State
{
    public class IdleStateFlyingEye:FlyingEyeState
    {
        public override void Enter()
        {
            base.Enter();
            Rigidbody2DOwner.velocity = Vector2.zero;
        }

        public override void LogicExcute()
        {
            SwitchState();
        }

        private void SwitchState()
        {
            Vector2 toTargetVector2 = TransformOwner.position - TargetTrans.position;
            float toTaretDis = MathF.Abs(toTargetVector2.magnitude);
            if (toTaretDis < FlyingEyeData.PursuitRange)
            {
                StateMachine.ChangeState(FlyingEyeDic[EnemyStateType.PursuitStateEnemy]);
            }
        }

        public IdleStateFlyingEye(FlyingEyeBase owner, string nameToTrigger, string animationName) : base(owner, nameToTrigger, animationName)
        {
        }
    }
}