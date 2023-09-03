using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using C_Script.BaseClass;
using C_Script.Common.Model.BehaviourModel;
using C_Script.Common.Model.EventCentre;
using C_Script.Eneny.EnemyCreator;
using C_Script.Player.Component;
using UnityEngine;

namespace C_Script.Eneny.Boss.SwordSaint.State
{
    public class SwordSaintState : State<SwordSaintBase>
    {
        protected SwordSaintData SwordSaintData => DataSo as SwordSaintData;
        protected SwordSaintModel SwordSaintModel => Model as SwordSaintModel;
        protected Dictionary<EnemyStateType, State<SwordSaintBase>> SwordSaintStateDic => Owner.SwordSaintStateDic;
        protected BossFactory BossFactory => Owner.Factory;

        protected Transform TargetTrans => SwordSaintModel.TargetTrans;

        public SwordSaintState(SwordSaintBase owner, string nameToTrigger, string animationName) : base(owner, nameToTrigger, animationName)
        {
           
        }
        
        protected IEnumerator AttackBehaviour(Vector2 forceDir,ForceDirection forceDirection)
        {
            if(StateMachine.CurrentState!=this||IsAniamtionFinshed) yield break;
            yield return new WaitUntil(() => IsAnimationName);
            yield return new WaitUntil(()=>AnimatorOwner.GetCurrentAnimatorStateInfo(0).normalizedTime>0.65);
            while (true) {
                if(StateMachine.CurrentState!=this||IsAniamtionFinshed) yield break;
                RaycastHit2D hit = Physics2D.Raycast(TransformOwner.position, new Vector2(TransformOwner.localScale.x, 0),
                    SwordSaintData.AttackRange, 1 << LayerMask.NameToLayer("Player"));
                if (hit)
                {
                    hit.transform.GetComponentInChildren<PlayerHealth>().PlayerDamage
                    (CalculateAttack(SwordSaintData.AttackPower,SwordSaintData.CriticalRate,SwordSaintData.CriticalDamage),
                        forceDir,forceDirection);
                    //Player enter hurtState
                    CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerHurt);
                    yield break;
                }
                yield return new WaitForSeconds(0.05f);
            }
        }
        
        protected IEnumerator ComboBehaviour(string AnimationNow,Vector2 ForceDir,ForceDirection forceDirection)
        {
            if(StateMachine.CurrentState!=this) yield break;
            yield return new WaitUntil(()=>AnimatorOwner.GetCurrentAnimatorStateInfo(0).normalizedTime>0.65);
            while (true) {
                if(StateMachine.CurrentState!=this||!AnimatorOwner.GetCurrentAnimatorStateInfo(0).IsName(AnimationNow)) yield break;
                RaycastHit2D hit = Physics2D.Raycast(TransformOwner.position, new Vector2(TransformOwner.localScale.x, 0),
                    SwordSaintData.AttackRange, 1 << LayerMask.NameToLayer("Player"));
                if (hit)
                {
                    hit.transform.GetComponentInChildren<PlayerHealth>().PlayerDamage
                    (CalculateAttack(SwordSaintData.AttackPower,SwordSaintData.CriticalRate,SwordSaintData.CriticalDamage),
                        ForceDir,forceDirection);
                    //Player enter hurtState
                    CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerHurt);
                    yield break;
                }
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
