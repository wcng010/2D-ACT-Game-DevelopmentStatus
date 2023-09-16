using System;
using System.Collections;
using C_Script.BaseClass;
using C_Script.Common.Model.EventCentre;
using C_Script.Eneny.Monster.FlyingEye.Base;
using C_Script.Eneny.Monster.FlyingEye.State.StateBase;
using C_Script.Player.Component;
using UnityEngine;

namespace C_Script.Eneny.Monster.FlyingEye.State
{
    public class HeavyAttackStateFlyingEye:FlyingEyeState
    {
        private Vector2 _toTargetVector2;
        private Vector2 _revisionPoint;
        public override void Enter()
        {
            base.Enter();
            Rigidbody2DOwner.velocity = Vector2.zero;
            _revisionPoint = 
                new Vector2(TransformOwner.localScale.x * FlyingEyeData.HeavyAttackPosOffSet.x,FlyingEyeData.HeavyAttackPosOffSet.y) + (Vector2)TargetTrans.position;
            Owner.StartCoroutine(AttackRayTest());
        }
        public override void LogicExcute()
        {
            if (IsAniamtionFinshed)
                StateMachine.ChangeState(FlyingEyeDic[EnemyStateType.CoolDownStateEnemy]);
            SwitchState();
            TransformOwner.position = Vector2.Lerp(TransformOwner.position, _revisionPoint, Time.deltaTime);
        }
        
        private void SwitchState()
        {
            _toTargetVector2 =  TargetTrans.position - TransformOwner.position;
        }
        
        IEnumerator AttackRayTest()
        {
            if(StateMachine.CurrentState!=this||IsAniamtionFinshed) yield break;
            yield return new WaitUntil(() => IsAnimationName && AnimatorOwner.GetCurrentAnimatorStateInfo(0).normalizedTime>0.75);
            if(StateMachine.CurrentState!=this||IsAniamtionFinshed) yield break;
            while (true)
            {
                RaycastHit2D hit = Physics2D.Raycast(TransformOwner.position, new Vector2(TransformOwner.localScale.x, 0),
                    FlyingEyeData.AttackRange, 1 << LayerMask.NameToLayer("Player"));
                if (hit)
                {
                    if(StateMachine.CurrentState!=this||IsAniamtionFinshed) yield break;
                    hit.transform.GetComponentInChildren<PlayerHealth>().PlayerDamage
                    (CalculateAttackWithoutCrit(FlyingEyeData.AttackPower), _toTargetVector2,ForceDirection.Forward);
                    yield break;
                }
                yield return new WaitForSeconds(0.05f);
            }
        }
        
        public HeavyAttackStateFlyingEye(FlyingEyeBase owner, string nameToTrigger, string animationName) : base(owner, nameToTrigger, animationName)
        {
        }
    }
}