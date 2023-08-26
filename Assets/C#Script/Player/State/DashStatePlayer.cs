﻿using System.Collections;
using System.Collections.Generic;
using C_Script.Player.BaseClass;
using C_Script.Player.Component;
using C_Script.Player.StateModel.BaseState;
using UnityEngine;

namespace C_Script.Player.StateModel
{
    public enum DashState
    {
        CanDash,
        CantDash
    }

    public class DashStatePlayer : PlayerState
    {
        private CollisionComponent CollsionComponent 
        {get=> _collisionComponent ? _collisionComponent :_collisionComponent= OwnerCore. GetCoreComponent<CollisionComponent>();}
        private CollisionComponent _collisionComponent;
        private float _dashOriginalY;
        private float _dashOriginalX;
        private float _dashLength;
        private float _xLerp;
        private DashState Dash;//0->Can't,1->Can
        
        private GameObject DashAsh { 
            get {
                if(!_dashAsh)
                    _dashAsh = GameObject.FindWithTag(nameof(DashAsh));
                return _dashAsh;
            } }
        private GameObject _dashAsh;
        
        public override void Enter()
        {
            base.Enter();
            _dashLength = PlayerData.DashLength;
            //There are obstacles ahead
            if (CollsionComponent.DashDistanceCheck(_dashLength))
            {
                Dash = DashState.CantDash;
                Owner.StartCoroutine(ReviseDashPoint());
            }
            else Dash = DashState.CanDash;
            var position = TransformOwner.position;
            _dashOriginalX = position.x;
            _dashOriginalY = position.y;
            Collider2DOwner.enabled = false;
            Rigidbody2DOwner.constraints = RigidbodyConstraints2D.FreezePositionY;
            Rigidbody2DOwner.gravityScale = PlayerData.GravityScale;
            _xLerp = 0;
            DashAsh.SetActive(true);
        }
        public override void LogicExcute()
        {
            if(Dash==DashState.CanDash)
                TransformOwner.position = new Vector3(CalculateDashX(), _dashOriginalY, 0);
            if(Dash==DashState.CanDash&& Mathf.Abs(TransformOwner.position.x-_dashOriginalX-TransformOwner.localScale.x*_dashLength)<0.1f)
                StateMachine.RevertOrinalState();
        }
        public override void Exit()
        {
            base.Exit();
            Rigidbody2DOwner.constraints = RigidbodyConstraints2D.FreezeRotation;
            Collider2DOwner.enabled = true;
            Rigidbody2DOwner.gravityScale = PlayerData.GravityScale;
            DashAsh.SetActive(false);
        }
        private IEnumerator ReviseDashPoint()
        {
            while (CollsionComponent.DashDistanceCheck(_dashLength))
            {
                _dashLength -= 0.05f;
            }
            Dash = DashState.CanDash;
            yield return null;
        }

        private float CalculateDashX()
        {
            return Mathf.Lerp(_dashOriginalX, _dashOriginalX + TransformOwner.localScale.x*_dashLength,
                _xLerp+=Time.unscaledDeltaTime*PlayerData.DashSpeed);
        }

        public DashStatePlayer(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            _dashAsh = DashAsh;
            DashAsh.SetActive(false);
        }
    }
}