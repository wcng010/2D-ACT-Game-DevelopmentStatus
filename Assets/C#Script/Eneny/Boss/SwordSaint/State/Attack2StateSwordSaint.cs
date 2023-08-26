using System.Collections;
using C_Script.BaseClass;
using C_Script.Common.Model.EventCentre;
using C_Script.Player.Component;
using UnityEngine;

namespace C_Script.Eneny.Boss.SwordSaint.State
{
    public class Attack2StateSwordSaint : SwordSaintState
    {
        public Attack2StateSwordSaint(SwordSaintBase owner, string nameToTrigger, string animationName) : base(owner, nameToTrigger, animationName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Rigidbody2DOwner.velocity = Vector2.zero;
            Owner.StartCoroutine(AttackBehaviour(new Vector2(SwordSaintData.Attack2Direction.x *TransformOwner.localScale.x 
                ,SwordSaintData.Attack2Direction.y),ForceDirection.Down));
        }

        public override void LogicExcute()
        {
            if (IsAniamtionFinshed)
                StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.ReadyStateEnemy]);
            SwitchState();
        }
        public override void Exit()
        {
            base.Exit();
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
            }
        }
    }
}