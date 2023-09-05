using System;
using System.Collections;
using C_Script.BaseClass;
using C_Script.Common.Model.EventCentre;
using C_Script.Eneny.Monster.Magician.Data;
using C_Script.Player.Component;
using UnityEngine;

namespace C_Script.Eneny.Boss.SwordSaint.State
{
    public class Attack1StateSwordSaint : SwordSaintState
    {
        public Attack1StateSwordSaint(SwordSaintBase owner, string nameToTrigger, string animationName) : base(owner, nameToTrigger, animationName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Rigidbody2DOwner.velocity = Vector2.zero;
            Owner.StartCoroutine(AttackBehaviour
                (new Vector2(SwordSaintData.Attack1Direction.x*TransformOwner.localScale.x ,SwordSaintData.Attack1Direction.y),ForceDirection.Up));
        }

        public override void LogicExcute()
        {
            if (IsAniamtionFinshed)
                StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.ReadyStateEnemy]);
        }

        public override void Exit()
        {
            base.Exit();
        }


    }
}