using System.Collections;
using C_Script.Player.BaseClass;
using C_Script.Player.Component;
using C_Script.Player.StateModel.BaseState;
using UnityEngine;
namespace C_Script.Player.StateModel.SupperState
{
    public class OnGroundStatePlayer:PlayerState
    {

        public override void Enter()
        {
            Rigidbody2DOwner.velocity = Vector2.zero;
            Rigidbody2DOwner.constraints =
                RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        
        public override void LogicExcute()
        {
            SwitchState();
        }

        public override void Exit()
        {
            Rigidbody2DOwner.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        private void SwitchState()
        {
            if (!Owner.IsGroundOneRay)
            {
                StateMachine.ChangeState(StateDictionary[PlayerStateType.OnAirStatePlayer]);
            }
            //Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StateMachine.ChangeState(StateDictionary[PlayerStateType.JumpStatePlayer]);
            }
            //Roll
            else if (Input.GetKeyDown(KeyCode.K))
            {
                StateMachine.ChangeState(StateDictionary[PlayerStateType.RollStatePlayer]);
            }
            //Dash
            else if (Input.GetKeyDown(KeyCode.Q)&&!SkillData.skillBools["Dash"])
            {
                StateMachine.ChangeState(StateDictionary[PlayerStateType.DashStatePlayer]);
            }
            else if (Input.GetKeyDown(KeyCode.J) && PressJKeyCount == 0)
            {
                StateMachine.ChangeState(StateDictionary[PlayerStateType.AttackState1Player]);
            }
            //Move
            else if (Owner.XAxis != 0)
            {
                StateMachine.ChangeState(StateDictionary[PlayerStateType.LowSpeedStatePlayer]);
            }
            //Crouch
            else if (Owner.YAxis < 0)
            {
                StateMachine.ChangeState(StateDictionary[PlayerStateType.CrouchStatePlayer]);
            }
        }

        public OnGroundStatePlayer(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
        }
    }
}