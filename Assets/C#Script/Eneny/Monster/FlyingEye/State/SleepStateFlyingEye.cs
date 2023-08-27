using System;
using C_Script.BaseClass;
using C_Script.Eneny.Monster.FlyingEye.Base;
using C_Script.Eneny.Monster.FlyingEye.State.StateBase;
using UnityEngine;

namespace C_Script.Eneny.Monster.FlyingEye.State
{
    public class SleepStateFlyingEye:FlyingEyeState
    {
        public override void LogicExcute()
        {
            base.LogicExcute();
            TargetApproach();
        }
        
        private void TargetApproach()
        {
            Vector2 toTargetVector2 = TransformOwner.position - TargetTrans.position;
            float toTaretDis = MathF.Abs(toTargetVector2.magnitude);
            if (toTaretDis < FlyingEyeData.PursuitRange)
            {
                StateMachine.ChangeState(FlyingEyeDic[EnemyStateType.WakeStateEnemy]);
            }
        }

        public SleepStateFlyingEye(FlyingEyeBase owner, string nameToTrigger, string animationName) : base(owner, nameToTrigger, animationName)
        {
        }
    }
}