using System.Collections;
using C_Script.Eneny.Boss.DemonBoss.BaseClass;
using C_Script.Eneny.Boss.DemonBoss.State.StateBase;
using C_Script.Object;
using C_Script.Player.Component;
using C_Script.Player.StateModel.BaseState;
using UnityEngine;

namespace C_Script.Eneny.Boss.DemonBoss.State
{
    public class AttackStateDemonBoss : DemonBossState
    {
        public override void Enter()
        {
            base.Enter();
            Owner.StartCoroutine(EarthFire());
        }

        public override void LogicExcute()
        {
            base.LogicExcute();
           /* if (IsAniamtionFinshed)
            {
                StateMachine.RevertOrinalState();
            }*/
        }

        public override void Exit()
        {
            base.Exit();
        }
        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator EarthFire()
        {
            yield return new WaitForSeconds(1.3f);
            Owner.StartCoroutine(AttackFront());
            Owner.Factory.effect1.GetComponent<EarthFire>().OnFire();            
            yield return new WaitUntil(() => IsAniamtionFinshed);
            Owner.Factory.effect1.GetComponent<EarthFire>().FightFire();
            Owner.Factory.effect1.SetActive(false);
        }

        private IEnumerator AttackFront()
        {
            while (true)
            {
                if(StateMachine.CurrentState!=this)yield break;
                RaycastHit2D hit2D = OwnerCore.GetCoreComponent<CollisionComponent>().EnemyFrontCheck;
                if (hit2D)
                {
                    hit2D.transform.GetComponentInChildren<PlayerHealth>().FatalBlow();
                }
                yield return new WaitForSeconds(0.1f);
            }
        }


        public AttackStateDemonBoss(DemonBossBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
        }
    }
}