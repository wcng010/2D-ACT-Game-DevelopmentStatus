using C_Script.BaseClass;
using UnityEngine;

namespace C_Script.Eneny.Boss.SwordSaint.State
{
    public class PursuitStateSwordSaint : SwordSaintState
    {
        private Vector2 toTargetVector2;
        private float distance;
        public PursuitStateSwordSaint(SwordSaintBase owner, string nameToTrigger, string animationName) : base(owner, nameToTrigger, animationName)
        {
            
        }
        
        public override void LogicExcute()
        {
            SwitchState();
        }

        public override void PhysicExcute()
        {
            base.PhysicExcute();
            PursuitBehaviour();
        }

        private void PursuitBehaviour()
        {
            Rigidbody2DOwner.velocity = new Vector2(SwordSaintData.PursuitSpeed*TransformOwner.localScale.x, Rigidbody2DOwner.velocity.y);
        }
        private void SwitchState()
        {
            if(SwordSaintData.IsDeath)
            {
                StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.DeathStateEnemy]);
                return;
            }
            if (SwordSaintData.IsTargetDeath)
            {
                StateMachine.RevertOrinalState();
                return;
            }
            if (SwordSaintData.IsHurt)
            {
                StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.HurtStateEnemy]);
                return;
            }
            toTargetVector2 = SwordSaintModel.TargetTrans.position - TransformOwner.position;
            distance = toTargetVector2.magnitude;
            TransformOwner.localScale = new Vector3(toTargetVector2.x/Mathf.Abs(toTargetVector2.x), 1, 1);
            if (distance < SwordSaintData.AttackRange)
            {
                float randomValue = Random.value;
                if (randomValue < 0.25)
                {
                    StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.MeleeAttack1StateEnemy]);
                }
                else if (randomValue < 0.5)
                {
                    StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.MeleeAttack2StateEnemy]);
                }
                else if (randomValue < 0.75)
                {
                    StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.MeleeAttack3StateEnemy]);
                }
                else 
                {
                    StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.ComboAttackStateEnemy]);
                }
            }
            else
            {
                StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.PursuitStateEnemy]);
            }
        }

        public override void Exit()
        {
            base.Exit();
            Rigidbody2DOwner.velocity = Vector2.zero;
        }
    }
}