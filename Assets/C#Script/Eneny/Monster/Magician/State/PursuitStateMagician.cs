using System;
using C_Script.BaseClass;
using C_Script.Eneny.Monster.Magician.BaseClass;
using C_Script.Eneny.Monster.Magician.State.StateBase;
using UnityEngine;

namespace C_Script.Eneny.Monster.Magician.State
{
    public sealed class PursuitStateMagician : MagicianState
    {
        public override void PhysicExcute()
        {
            PursuitBehaviour();
        }

        public override void LogicExcute()
        {
            SwitchState();
        }
        
        private void PursuitBehaviour()
        {
            float dir = TargetTrans.position.x - TransformOwner.position.x;
            if (dir < 0)
            {
                Rigidbody2DOwner.velocity = new Vector2(-MagicianData.PursuitSpeed,
                    Rigidbody2DOwner.velocity.y);
            }
            else
            {
                Rigidbody2DOwner.velocity = new Vector2(MagicianData.PursuitSpeed,
                    Rigidbody2DOwner.velocity.y);
            }
        }
        public PursuitStateMagician(MagicianBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            
        }
        private void SwitchState()
        {
            Vector2 toTargetVector2 = TargetTrans.position-TransformOwner.position;
            float toTargetDis = MathF.Abs(toTargetVector2.magnitude);
            TransformOwner.localScale = new Vector3(toTargetVector2.x/Mathf.Abs(toTargetVector2.x), 1, 1);
            //距离大于追击范围
            if (toTargetDis > MagicianData.PursuitRange)
            {
                StateMachine.ChangeState(MagicianDic[MagicianData.OriginState]);
            }
            //距离小于远程攻击范围
            else if (toTargetDis< MagicianData.RemoteAttackRange)
            {
                StateMachine.ChangeState(MagicianDic[EnemyStateType.RemoteAttackStateEnemy]);
            }
        }
    }
}