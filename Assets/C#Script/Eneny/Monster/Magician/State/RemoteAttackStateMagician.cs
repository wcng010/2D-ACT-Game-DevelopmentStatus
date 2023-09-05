using System;
using System.Collections;
using C_Script.BaseClass;
using C_Script.Eneny.Monster.Magician.BaseClass;
using C_Script.Eneny.Monster.Magician.Component;
using C_Script.Eneny.Monster.Magician.State.StateBase;
using C_Script.Player.StateModel.BaseState;
using UnityEngine;

namespace C_Script.Eneny.Monster.Magician.State
{
    public class RemoteAttackStateMagician:MagicianState
    {
        private int _counter;
        private GameObject _meteorite;
        private Vector2 _toTargetVector2;

        // ReSharper disable Unity.PerformanceAnalysis
        public override void Enter()
        {
            base.Enter();
            Owner.StartCoroutine(RemoteAttackBehaviour());
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void PhysicExcute()
        {
            
        }

        public override void LogicExcute()
        {
            SwitchState();
        }
        
        IEnumerator RemoteAttackBehaviour()
        {
            while (true)
            {
                if ((Mathf.Abs(_toTargetVector2.x) < MagicianData.RemoteAttackRange
                    &&(Mathf.Abs(_toTargetVector2.x)>MagicianData.MeleeAttackRange)))
                {
                    if (!_meteorite)
                    {
                        float temp = ++_counter;
                        yield return new WaitForSeconds(MagicianData.RemoteAttackCoolDown);
                        if(StateMachine.CurrentState!=this||Math.Abs(temp - _counter) > 0.1f) yield break;
                        _meteorite = OwnerCore.GetCoreComponent<MeteoriteComponent>().FireMateorite();
                    }
                }
                yield return new WaitForSeconds(0.05f);
                if(StateMachine.CurrentState!=this)
                    yield break;
            }
        }
        
        public RemoteAttackStateMagician(MagicianBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            
        }
        private void SwitchState()
        {
            _toTargetVector2 = TargetTrans.position-TransformOwner.position;
            float toTargetDis = MathF.Abs(_toTargetVector2.magnitude);
            TransformOwner.localScale = new Vector3(_toTargetVector2.x/Mathf.Abs(_toTargetVector2.x), 1, 1);
            //距离大于远程攻击范围
            if (toTargetDis > MagicianData.RemoteAttackRange)
            {
                StateMachine.ChangeState(MagicianDic[EnemyStateType.PursuitStateEnemy]);
            }
            //距离小于近战攻击范围
            else if (toTargetDis < MagicianData.MeleeAttackRange)
            {
                StateMachine.ChangeState(MagicianDic[EnemyStateType.MeleeAttackStateEnemy]);
            }
        }
    }
}