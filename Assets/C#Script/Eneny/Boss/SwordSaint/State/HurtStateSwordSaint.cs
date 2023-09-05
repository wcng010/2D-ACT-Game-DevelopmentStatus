using C_Script.BaseClass;
using UnityEngine;

namespace C_Script.Eneny.Boss.SwordSaint.State
{
    public class HurtStateSwordSaint : SwordSaintState
    {
        public HurtStateSwordSaint(SwordSaintBase owner, string nameToTrigger, string animationName) : base(owner, nameToTrigger, animationName)
        {
            
        }

        public override void LogicExcute()
        {
            if (IsAniamtionFinshed)
            {
                float randomValue = Random.value;
                if (randomValue < 0.7)
                    StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.ReadyStateEnemy]);
                else
                    StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.DodgeStateEnemy]);
            }
            
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}