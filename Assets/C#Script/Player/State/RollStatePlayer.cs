using C_Script.Player.Base;
using C_Script.Player.State.BaseState;
using UnityEngine;

namespace C_Script.Player.StateModel
{
    public class RollStatePlayer:PlayerState
    {
        public override void Enter()
        {
            base.Enter();
            Rigidbody2DOwner.AddForce(new Vector2(PlayerData.rollForce,0) *(TransformOwner.localScale.x),ForceMode2D.Impulse);

        }

        public RollStatePlayer(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
        }
    }
}