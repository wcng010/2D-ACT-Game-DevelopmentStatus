using C_Script.Player.BaseClass;
using C_Script.Player.StateModel.BaseState;
using UnityEngine;

namespace C_Script.Player.StateModel
{
    public class CrouchAttackStatePlayer:PlayerState
    {
        public override void Enter()
        { 
            base.Enter();
            ChangeColliderYSize(Collider2DOwner,PlayerData.CrouchSizeY);
            Owner.StartCoroutine(AttackRayTestWithPower(PlayerData.CrouchAttackRange,
                AnimationTime,"player_CrouchAttack",Collider2DOwner.size.y/2));
            Owner.Rigidbody2D.velocity = Vector2.zero;
        }
        public override void Exit()
        {
            base.Exit();
            ChangeColliderYSize(Collider2DOwner,PlayerData.IdleSizeY);
            Time.timeScale = 1;
        }
        
        public CrouchAttackStatePlayer(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {

        }
    }
}