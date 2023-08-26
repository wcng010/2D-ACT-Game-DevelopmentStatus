using System;
using C_Script.BaseClass;
using C_Script.Eneny.Monster.FlyingEye.Base;
using C_Script.Eneny.Monster.FlyingEye.State.StateBase;
using UnityEngine;
using Random = UnityEngine.Random;

namespace C_Script.Eneny.Monster.FlyingEye.State
{
    public class FlyStateFlyingEye:FlyingEyeState
    {

        private Vector2 _toTargetVector2;

        public override void Enter()
        {
            base.Enter();
        }

        public override void PhysicExcute()
        {
            Rigidbody2DOwner.velocity = FlyingEyeData.PursuitSpeed * _toTargetVector2.normalized;
        }

        public override void LogicExcute()
        {
            SwitchState();
            TransformOwner.localScale = new Vector3(_toTargetVector2.x>0?1:-1,1,1);
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
                return;
            }
            _toTargetVector2 = TargetTrans.position-TransformOwner.position;
            float toTaretDis = MathF.Abs(_toTargetVector2.magnitude);
            if (toTaretDis > FlyingEyeData.PursuitRange)
            {
                StateMachine.ChangeState(FlyingEyeDic[EnemyStateType.IdleStateEnemy]);
            }
            else if (toTaretDis < FlyingEyeData.AttackRange)
            {
                if (Random.value < FlyingEyeData.CriticalRate)
                    StateMachine.ChangeState(FlyingEyeDic[EnemyStateType.LightAttackStateEnemy]);
                else
                    StateMachine.ChangeState(FlyingEyeDic[EnemyStateType.HeavyAttackStateEnemy]);
            }
        }

        public FlyStateFlyingEye(FlyingEyeBase owner, string nameToTrigger, string animationName) : base(owner, nameToTrigger, animationName)
        {
        }
    }
}