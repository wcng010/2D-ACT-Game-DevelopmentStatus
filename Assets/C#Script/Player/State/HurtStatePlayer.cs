using C_Script.Common.Model.EventCentre;
using C_Script.Player.Base;
using C_Script.Player.State.BaseState;
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
        }
        public HurtStatePlayer(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {

        }
    }
}