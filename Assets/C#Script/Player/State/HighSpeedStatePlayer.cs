using System;
using System.Collections;
using System.Collections.Generic;
using C_Script.Player.Base;
using C_Script.Player.Component;
using C_Script.Player.State.BaseState;
using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace C_Script.Player.StateModel
{
    public class HighSpeedStatePlayer : PlayerState
    {
        private int MoveDir;//Right:1,Left:-1
        private bool IsFullSpeed;
        private bool IsSpeedUp;
        private int _trigger;

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
            if (XAxis > 0) MoveDir = 1;
            else if (XAxis < 0) MoveDir = -1;
            _trigger = 0;
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
            else if (IsFullSpeed&&XAxis>0 && MoveDir==-1)
                StateMachine.ChangeState(StateDictionary[PlayerStateType.TurnAroundStatePlayer]);
            else if (IsFullSpeed&&XAxis<0 && MoveDir == 1)
                StateMachine.ChangeState(StateDictionary[PlayerStateType.TurnAroundStatePlayer]);
            else if (XAxis == 0 && _trigger==0)
            {
                Owner.StartCoroutine(CoyoteStop());
                _trigger = 1;
            }
                //Jump
            else if (SpaceKey)
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
            //CrouchMove
            else if (YAxis < 0)
                StateMachine.ChangeState(StateDictionary[PlayerStateType.CrouchMoveStatePlayer]);
        }



        IEnumerator CoyoteStop()
        {
            yield return new WaitForSecondsRealtime(PlayerData.CoyoteTime);
            if(StateMachine.CurrentState == this)
                StateMachine.ChangeState(StateDictionary[PlayerStateType.OnGroundStatePlayer]);
        }




        public HighSpeedStatePlayer(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            _moveAsh = GameObject.FindWithTag(nameof(MoveAsh));
        }
    }
}
