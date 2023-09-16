using C_Script.Player.Base;
using C_Script.Player.State.BaseState;
using UnityEngine;

namespace C_Script.Player.StateModel
{
    public class StopStatePlayer : PlayerState
    {
        public override void Enter()
        {
        }

        public override void PhysicExcute()
        {
            
        }

        public override void LogicExcute()
        {
            
        }

        public override void Exit()
        {
            
        }
        
        public StopStatePlayer(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
        }
    }
}