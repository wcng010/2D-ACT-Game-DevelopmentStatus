using System;
using C_Script.Player.BaseClass;
using C_Script.Player.Component;
using C_Script.Player.StateModel.BaseState;
using UnityEditor;
using UnityEngine;

namespace C_Script.Player.StateModel
{
    public class HighSpeedStatePlayer : PlayerState
    {
        private int MoveDir;//Right:1,Left:-1
        private bool IsFullSpeed;
        private bool IsSpeedUp;
        
        private GameObject MoveAsh { 
            get {
                if(!_moveAsh)
                    _moveAsh = GameObject.FindWithTag(nameof(MoveAsh));
                return _moveAsh;
            } }
        
        private GameObject _moveAsh;
        public override void Enter()
        {
            base.Enter();
            if (Owner.XAxis > 0) MoveDir = 1;
            else if (Owner.XAxis < 0) MoveDir = -1;
            IsFullSpeed = false;
            PlayerData.WalkAshEffectTrriger = true;
            MoveAsh.SetActive(true);
        }
        public override void PhysicExcute()
        {
            MoveBehaviour(PlayerData.MaxSpeedX,PlayerData.AccelerationX);
            var maxspeed = PlayerData.MaxSpeedX;
            if (Mathf.Abs(Mathf.Abs(Rigidbody2DOwner.velocity.x) - maxspeed) < maxspeed-PlayerData.TurnAroundSpeed)
            {
                IsFullSpeed = true;
            }
        }
        public override void LogicExcute()
        {
            SwitchState();
        }
        
        public override void Exit()
        {
            base.Exit();
            PlayerData.SpeedUpbotton = false;
            PlayerData.WalkAshEffectTrriger = false;
            MoveAsh.SetActive(false);
            PlayerModel.PlayerAudioTrigger.RunStop();
        }
        private void SwitchState()
        {
            if(!PlayerData.SpeedUpbotton) 
                StateMachine.ChangeState(Owner.PlayerStateDic[PlayerStateType.LowSpeedStatePlayer]);
            if (!Owner.IsGroundThreeRays) 
                StateMachine.ChangeState(Owner.PlayerStateDic[PlayerStateType.OnAirStatePlayer]);
            //Return OnGroundState
            else if (IsFullSpeed&&Input.GetKeyDown(KeyCode.D) && MoveDir==-1)
                StateMachine.ChangeState(StateDictionary[PlayerStateType.TurnAroundStatePlayer]);
            else if (IsFullSpeed&&Input.GetKeyDown(KeyCode.A) && MoveDir == 1) 
                StateMachine.ChangeState(StateDictionary[PlayerStateType.TurnAroundStatePlayer]);
            else if (Owner.XAxis == 0)
                StateMachine.ChangeState(StateDictionary[PlayerStateType.OnGroundStatePlayer]);
            //Jump
            else if (Input.GetKeyDown(KeyCode.Space))
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
            //CrouchMove
            else if (Owner.YAxis < 0)
                StateMachine.ChangeState(StateDictionary[PlayerStateType.CrouchMoveStatePlayer]);
        }

        public HighSpeedStatePlayer(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            _moveAsh = GameObject.FindWithTag(nameof(MoveAsh));
        }
    }
}
