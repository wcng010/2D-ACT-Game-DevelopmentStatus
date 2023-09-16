using System.Collections;
using C_Script.Player.Base;
using C_Script.Player.Component;
using C_Script.Player.State.BaseState;
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
            if (SpaceKey)
            {
                StateMachine.ChangeState(StateDictionary[PlayerStateType.JumpStatePlayer]);
            }
            //Roll
            else if (KKey)
            {
                StateMachine.ChangeState(StateDictionary[PlayerStateType.RollStatePlayer]);
            }
            //Dash
            else if (QKey&&SkillData.skillBools["Dash"])
            {
                StateMachine.ChangeState(StateDictionary[PlayerStateType.DashStatePlayer]);
            }
            else if (JKey && PressJKeyCount == 0)
            {
                StateMachine.ChangeState(StateDictionary[PlayerStateType.AttackState1Player]);
            }
            //Move
            else if (XAxis != 0)
            {
                StateMachine.ChangeState(StateDictionary[PlayerStateType.LowSpeedStatePlayer]);
            }
            //Crouch
            else if (YAxis < 0)
            {
                StateMachine.ChangeState(StateDictionary[PlayerStateType.CrouchStatePlayer]);
            }
        }

        public OnGroundStatePlayer(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
        }
    }
}