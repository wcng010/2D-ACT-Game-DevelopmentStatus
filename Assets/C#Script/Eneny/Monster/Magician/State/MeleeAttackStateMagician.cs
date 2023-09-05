using System;
using System.Collections;
using C_Script.BaseClass;
using C_Script.Common.Model.EventCentre;
using C_Script.Eneny.Monster.Magician.BaseClass;
using C_Script.Eneny.Monster.Magician.State.StateBase;
using C_Script.Player.Component;
using C_Script.Player.StateModel.BaseState;
using C_Script.StaticWay;
using UnityEngine;


namespace C_Script.Eneny.Monster.Magician.State
{
    public class MeleeAttackStateMagician:MagicianState
    {
        private Vector2 _toTargetVector2;

        public override void Enter()
        {
            base.Enter();
            Owner.StartCoroutine(AttackRayTest());
        }

        public override void PhysicExcute()
        {
            //TransformOwner.localScale = new Vector3(_toTargetVector2.x/Mathf.Abs(_toTargetVector2.x), 1, 1);
        }

        public override void LogicExcute()
        {
            if(IsAniamtionFinshed)
                StateMachine.ChangeState(Owner.MagicianDic[EnemyStateType.CoolDownStateEnemy]);
            SwitchState();
        }

        public override void Exit()
        {
            base.Exit();
            SetColliderOffset(Collider2DOwner,new Vector2(0,0.004239365f));
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        IEnumerator AttackRayTest()
        {
            if(StateMachine.CurrentState!=this||IsAniamtionFinshed) yield break;
                yield return new WaitUntil(() => IsAnimationName);
                SetColliderOffset(Collider2DOwner,new Vector2(0,0.004239365f));
                yield return new WaitUntil(()=>AnimatorOwner.GetCurrentAnimatorStateInfo(0).normalizedTime>0.5);
                SetColliderOffset(Collider2DOwner,new Vector2(-0.3073f,0.004239365f));
                while (true) {
                if(StateMachine.CurrentState!=this||IsAniamtionFinshed) yield break;
                RaycastHit2D hit = Physics2D.Raycast(TransformOwner.position, new Vector2(TransformOwner.localScale.x, 0),
                    MagicianData.MeleeAttackRange, 1 << LayerMask.NameToLayer("Player"));
                if (hit)
                {
                    hit.transform.GetComponentInChildren<PlayerHealth>().PlayerDamage
                        (CalculateAttack(MagicianData.AttackPower,MagicianData.CriticalRate,MagicianData.CriticalDamage),
                            Quaternion.AngleAxis(MagicianData.MeleeAttackAngle,_toTargetVector2.x>0?Vector3.forward:Vector3.back)*_toTargetVector2 ,ForceDirection.Up);
                    yield break;
                }
                yield return new WaitForSeconds(0.05f);
            }
        }
        public MeleeAttackStateMagician(MagicianBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            
        }
        
        private void SwitchState()
        {
            _toTargetVector2 = TargetTrans.position-TransformOwner.position;
        }
    }
}