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

        }
        public CoolStateMagician(MagicianBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            
        }
    }
}