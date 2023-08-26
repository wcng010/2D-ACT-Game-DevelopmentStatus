using C_Script.BaseClass;
using UnityEngine;

namespace C_Script.Eneny.Boss.SwordSaint.State
{
    public class IdleStateSwordSaint : SwordSaintState
    {
        public IdleStateSwordSaint(SwordSaintBase owner, string nameToTrigger, string animationName) : base(owner, nameToTrigger, animationName)
        {
        }

        public override void LogicExcute()
        {
            SwitchState();
        }

        private void SwitchState()
        {
            Vector2 toTargetVector2 = SwordSaintModel.TargetTrans.position - TransformOwner.position;
            float distance = toTargetVector2.magnitude;
            if (distance < SwordSaintData.PursuitRange)
            {
                StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.PursuitStateEnemy]);
            }
        }
    }
}
