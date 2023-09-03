﻿using System.Collections;
using C_Script.Common.Model.ObjectPool;
using C_Script.Player.BaseClass;
using C_Script.Player.Component;
using C_Script.Player.MVC.Model;
using C_Script.Player.StateModel.BaseState;
using UnityEngine;

namespace C_Script.Player.StateModel.SupperState
{
    public class OnAirStatePlayer : PlayerState
    {
        // ReSharper disable Unity.PerformanceAnalysis
        private string _fallAsh;
        public override void Enter()
        {
            base.Enter();
        }

        public override void PhysicExcute()
        {
            FallBahaviour();
        }

        public override void LogicExcute()
        {
            SwitchState();
        }

        public override void Exit()
        {
            base.Exit();
        }
        
        protected void FallBahaviour()=> MoveBehaviour(PlayerData.MaxSpeedX,PlayerData.AccelerationX);
        
        private void SwitchState()
        {
            if (Owner.IsGroundOneRay)
            {
                PlayerModel.PlayerAudioTrigger.LandPlay();
                MyObjectPool.Instance.SetActive(_fallAsh);
                StateMachine.RevertOrinalState();
            }
            RaycastHit2D headFrontHit2D= Owner.HeadFrontHit2D;
            RaycastHit2D bodyFrontHit2D = Owner.BodyFrontHit2D;
            if (!bodyFrontHit2D&&headFrontHit2D)
            {
                HandRevision(headFrontHit2D.point);
                StateMachine.ChangeState(Owner.PlayerStateDic[PlayerStateType.HangingStatePlayer]);
            }
            if(Input.GetKeyDown(KeyCode.J)&& PressJKeyCount==0)
                StateMachine.ChangeState(Owner.PlayerStateDic[PlayerStateType.AttackState1Player]);
            if(Input.GetKeyDown(KeyCode.Q)&&!SkillData.skillBools["Dash"])
                StateMachine.ChangeState(Owner.PlayerStateDic[PlayerStateType.DashStatePlayer]);
        }
        
        private void HandRevision(Vector2 hitPoint)
        {
            Vector2 handPoint = OwnerCore.GetCoreComponent<CollisionComponent>().HandTrans.position;
            Vector2 revisionPoint = hitPoint + (Vector2)TransformOwner.position - handPoint;
            TransformOwner.position = revisionPoint;
        }
        

        public OnAirStatePlayer(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            MyObjectPool.Instance.PushObject(GameObject.Instantiate( PlayerData.FallAsh, PlayerModel.ObjectPool));
            _fallAsh = PlayerData.FallAsh.name;
        }
    }
}