using C_Script.BaseClass;
using C_Script.Eneny.Monster.FlyingEye.Data;
using C_Script.Eneny.Monster.Magician.BaseClass;
using C_Script.Eneny.Monster.Magician.State.StateBase;
using UnityEngine;

namespace C_Script.Eneny.Monster.Magician.State
{
    public class CoolStateMagician:MagicianState
    {
        public override void Enter()
        {
            base.Enter();
            Rigidbody2DOwner.velocity = Vector2.zero;
        }

        public override void LogicExcute()
        {
            base.LogicExcute();
            SwitchState();
        }
        private void SwitchState()
        {
            if (MagicianData.IsDeath)
            {
                StateMachine.ChangeState(MagicianDic[EnemyStateType.DeathStateEnemy]);
                return;
            }
            if (MagicianData.IsHurt)
            {
                StateMachine.ChangeState(MagicianDic[EnemyStateType.HurtStateEnemy]);
                return;
            }
            if (MagicianData.IsTargetDeath)
            {
                StateMachine.RevertOrinalState();
            }
        }
        public CoolStateMagician(MagicianBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            
        }
    }
}