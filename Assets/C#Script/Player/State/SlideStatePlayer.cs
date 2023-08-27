using C_Script.Player.BaseClass;
using C_Script.Player.StateModel.BaseState;
using UnityEngine;

namespace C_Script.Player.StateModel
{
    public class SlideStatePlayer:PlayerState
    {
        public override void Enter()
        {
            base.Enter();
            Rigidbody2DOwner.AddForce(new Vector2(PlayerData.slideForce,0) *(TransformOwner.localScale.x),ForceMode2D.Impulse);
            ChangeColliderYSize(Collider2DOwner,PlayerData.SlideSizeY);
        }
        public override void Exit()
        {
            base.Exit();
            ChangeColliderYSize(Collider2DOwner,PlayerData.IdleSizeY);
        }
        
        public SlideStatePlayer(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
        }
    }
}