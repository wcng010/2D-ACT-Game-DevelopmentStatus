using System;
using C_Script.BaseClass;
using C_Script.Eneny.Monster.Magician.BaseClass;
using C_Script.Eneny.Monster.Magician.State.StateBase;
using C_Script.Player.StateModel.BaseState;
using UnityEngine;

namespace C_Script.Eneny.Monster.Magician.State
{
    public class IdleStateMagician:MagicianState
    {
        public override void Enter()
        {
            
        }

        public override void PhysicExcute()
        {
            
        }
        

        public override void LogicExcute()
        {
            SwitchState();
        }

        public override void Exit()
        {
            
        }
        
        public IdleStateMagician(MagicianBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void SwitchState()
        {
            Vector2 toTargetVector2 = TargetTrans.position-TransformOwner.position;
            float toTargetDis = MathF.Abs(toTargetVector2.magnitude);
            if (MagicianData.IsDeath)
            {
                StateMachine.ChangeState(MagicianDic[EnemyStateType.DeathStateEnemy]);
            }
            else if (MagicianData.IsHurt)
            {
                StateMachine.ChangeState(MagicianDic[EnemyStateType.HurtStateEnemy]);
            }
            else if (MagicianData.IsTargetDeath)
            {
                StateMachine.RevertOrinalState();
            }
            else if (toTargetDis < MagicianData.PursuitRange)
            {
                StateMachine.ChangeState(MagicianDic[EnemyStateType.PursuitStateEnemy]);
            }
        }
    }
}