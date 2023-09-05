using System;
using C_Script.BaseClass;
using C_Script.Eneny.Monster.Magician.BaseClass;
using C_Script.Eneny.Monster.Magician.State.StateBase;
using C_Script.Player.StateModel.BaseState;
using UnityEngine;

namespace C_Script.Eneny.Monster.Magician.State
{
    public class PatrolStateMagician : MagicianState
    {
        //-1:左，1：右
        private int _patrolState=-1;
        
        public override void PhysicExcute()
        {
            PatrolBehaviour();
        }

        public override void LogicExcute()
        {
            SwitchState();
        }
        
        protected virtual void PatrolBehaviour()
        {  
            if (_patrolState == -1 && TransformOwner.position.x > OriginPoint.x- MagicianData.PatrolRange)
            {
                TransformOwner.localScale = new Vector3(-1, 1, 1);
                Rigidbody2DOwner.velocity = new Vector2(-MagicianData.PatrolSpeed,Rigidbody2DOwner.velocity.y);
            }
            else if (_patrolState == -1 && TransformOwner.position.x <=
                     OriginPoint.x - MagicianData.PatrolRange)
            {
                _patrolState = 1;
            }
            else if (_patrolState == 1 && TransformOwner.position.x <
                     OriginPoint.x + MagicianData.PatrolRange)
            {
                TransformOwner.localScale = new Vector3(1, 1, 1);
                Rigidbody2DOwner.velocity = new Vector2(MagicianData.PatrolSpeed, Rigidbody2DOwner.velocity.y);
            }
            else if (_patrolState == 1 && TransformOwner.position.x >=
                     OriginPoint.x + MagicianData.PatrolRange)
            {
                _patrolState = -1;
            }
        }


        public PatrolStateMagician(MagicianBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            
        }
        
        private void SwitchState()
        {
            Vector2 toTargetVector2 = TargetTrans.position-TransformOwner.position;
            float toTargetDis = MathF.Abs(toTargetVector2.magnitude);
            if (toTargetDis < MagicianData.PursuitRange)
            {
                StateMachine.ChangeState(MagicianDic[EnemyStateType.PursuitStateEnemy]);
            }
        }
    }
}