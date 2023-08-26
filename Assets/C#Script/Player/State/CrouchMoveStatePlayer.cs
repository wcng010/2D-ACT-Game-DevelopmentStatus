using C_Script.Player.BaseClass;
using C_Script.Player.StateModel.BaseState;
using UnityEditor;
using UnityEngine;

namespace C_Script.Player.StateModel
{
    public class CrouchMoveStatePlayer:PlayerState
    {
        private static readonly int CrouchMove = Animator.StringToHash("CrouchMove");
        
        public override void Enter()
        {
            base.Enter();
            ChangeColliderYSize(Collider2DOwner, PlayerData.CrouchSizeY);
        }

        public override void PhysicExcute()
        {
            CrouchMoveBehaviour();
        }

        public override void LogicExcute()
        {
            SwitchState();
        }
        
        protected void CrouchMoveBehaviour()
        {
            MoveBehaviour(PlayerData.MaxSpeedCrouchX,PlayerData.AccelerationCrouchX);
        }

        
        public override void Exit()
        {
            base.Exit();
            ChangeColliderYSize(Collider2DOwner,PlayerData.IdleSizeY);
        }
        
        private void SwitchState()
        {
            //Return MoveState
            if (Owner.YAxis >= 0)
                StateMachine.ChangeState(StateDictionary[PlayerStateType.LowSpeedStatePlayer]);
            //Crouch
            else if (Owner.XAxis == 0)
                StateMachine.ChangeState(StateDictionary[PlayerStateType.CrouchStatePlayer]);
            //CrouchState
            else if(Input.GetKeyDown(KeyCode.J))
                StateMachine.ChangeState(StateDictionary[PlayerStateType.CrouchAttackStatePlayer]);
            //SlideState
            else if(Input.GetKeyDown(KeyCode.K))
                StateMachine.ChangeState(StateDictionary[PlayerStateType.SlideStatePlayer]);
        }

        public CrouchMoveStatePlayer(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {

        }
    }
}