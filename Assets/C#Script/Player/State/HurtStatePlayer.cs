using C_Script.Common.Model.EventCentre;
using C_Script.Player.BaseClass;
using C_Script.Player.StateModel.BaseState;
using UnityEngine;

namespace C_Script.Player.State
{
    public class HurtStatePlayer :PlayerState
    {
        public override void Enter()
        {
            base.Enter();
            if (PlayerData.CurrentHealth <= 0)
                StateMachine.ChangeState(Owner.PlayerStateDic[PlayerStateType.DeathStatePlayer]);
            CombatEventCentreManager.Instance.Publish(CombatEventType.CameraShake);
        }
        public override void LogicExcute()
        {
            if (!IsAnimationName) return;
            base.LogicExcute();
        }
        public HurtStatePlayer(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {

        }
    }
}