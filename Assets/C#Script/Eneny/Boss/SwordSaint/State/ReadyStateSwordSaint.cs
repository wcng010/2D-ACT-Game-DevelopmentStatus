using C_Script.BaseClass;
using UnityEngine;

namespace C_Script.Eneny.Boss.SwordSaint.State
{
    public class ReadyStateSwordSaint : SwordSaintState
    {
        public ReadyStateSwordSaint(SwordSaintBase owner, string nameToTrigger, string animationName) : base(owner, nameToTrigger, animationName)
        {
        }

        public override void Enter()
        {
            
        }

        public override void PhysicExcute()
        {
            
        }

        public override void LogicExcute()
        {
            var toTargetVector2 = SwordSaintModel.TargetTrans.position - TransformOwner.position;
            var distance = toTargetVector2.magnitude;
            TransformOwner.localScale = new Vector3(toTargetVector2.x/Mathf.Abs(toTargetVector2.x), 1, 1);
            if (distance < SwordSaintData.AttackRange)
            {
                float ramdomValue = Random.value;
                if (ramdomValue < 0.25)
                {
                    StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.MeleeAttack1StateEnemy]);
                }
                else if (ramdomValue < 0.5)
                {
                    StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.MeleeAttack2StateEnemy]);
                }
                else if (ramdomValue < 0.75)
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
            
        }
    }
}