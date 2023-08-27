using C_Script.Player.BaseClass;
using C_Script.Player.StateModel.BaseState;
using UnityEngine;

namespace C_Script.Player.StateModel
{
    public class HangingStatePlayer:PlayerState
    {

        private static readonly int Hang = Animator.StringToHash("Hang");
        //private float _horizontalSpeed=0;
        
        public override void Enter()
        {
            base.Enter();
            Rigidbody2DOwner.velocity = Vector2.zero;
            Rigidbody2DOwner.gravityScale = 0;
        }

        public override void PhysicExcute()
        {
           
        }

        public override void LogicExcute()
        {
            SwicthState();
        }

        public override void Exit()
        {
            base.Exit();
            Rigidbody2DOwner.gravityScale = PlayerData.GravityScale;
            AnimatorOwner.SetBool(Hang,false);
        }
        private void SwicthState()
        {
            if (!Owner.HeadFront)
            {
                Rigidbody2DOwner.velocity = new Vector2(0, -1);
            }
            else
            {
                Rigidbody2DOwner.velocity = new Vector2(0, 0);
                if (Owner.XAxis/TransformOwner.localScale.x > 0)
                    StateMachine.ChangeState(Owner.PlayerStateDic[PlayerStateType.BoardStatePlayer]);
            }
            if (Owner.IsGroundThreeRays)
            {
                if (Owner.XAxis/TransformOwner.localScale.x > 0)
                    StateMachine.ChangeState(Owner.PlayerStateDic[PlayerStateType.BoardStatePlayer]);
            }
        }

        public HangingStatePlayer(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            
        }
    }
}