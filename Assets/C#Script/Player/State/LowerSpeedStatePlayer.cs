using C_Script.Player.Base;
using C_Script.Player.State.BaseState;
using UnityEngine;

namespace C_Script.Player.StateModel
{
    public class LowerSpeedStatePlayer:PlayerState
    {
        private float _time = 0;
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
        }

        public override void PhysicExcute()
        {
            MoveBehaviour(PlayerData.MaxSpeedX*3/4,PlayerData.AccelerationX*3/4);
        }
        // ReSharper disable Unity.PerformanceAnalysis
        public override void LogicExcute()
        {
            _time += Time.deltaTime;
            if (_time > 1f)
            {
                PlayerData.SpeedUpbotton = true;
                _time = 0;
            }
            SwitchState();
        }

        public override void Exit() {
            base.Exit();
            PlayerModel.PlayerAudioTrigger.RunStop();
        }
        private void SwitchState()
        {
            if(PlayerData.SpeedUpbotton) 
                StateMachine.ChangeState(Owner.PlayerStateDic[PlayerStateType.HighSpeedStatePlayer]);
            if (!Owner.IsGroundThreeRays)
            {
                StateMachine.ChangeState(Owner.PlayerStateDic[PlayerStateType.OnAirStatePlayer]);
            }
            //Return OnGroundState
            else if (XAxis == 0)
                StateMachine.ChangeState(StateDictionary[PlayerStateType.OnGroundStatePlayer]);
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

        public LowerSpeedStatePlayer(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            _moveAsh = GameObject.FindWithTag(nameof(MoveAsh));
            _moveAsh.SetActive(false);
        }
    }
}