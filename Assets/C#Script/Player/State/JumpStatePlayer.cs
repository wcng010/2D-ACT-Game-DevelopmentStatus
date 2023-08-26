using System;
using System.Collections;
using System.Collections.Generic;
using C_Script.Player.BaseClass;
using C_Script.Player.Component;
using C_Script.Player.StateModel.BaseState;
using TMPro;
using UnityEngine;
namespace C_Script.Player.StateModel
{
    public class JumpStatePlayer:PlayerState
    {

        private float _speedUpTime;
        private float _jumpOriginalY;
        private float _yLerp;
        private bool _isSpeedUp = true;
        private readonly Transform _ashParent;
        private readonly Vector2 _ashPos;
        private RaycastHit2D _headRaycastHit2D;
        private GameObject _jumpAsh;
        public GameObject JumpAsh
        {
            get
            {
                if (!_jumpAsh)
                    _jumpAsh = GameObject.FindWithTag("JumpAsh");
                return _jumpAsh;
            }
        }
        
        public override void Enter() 
        {   base.Enter();
            _isSpeedUp = true;
            Owner.StartCoroutine(JumpChange());
            if (JumpAsh.transform.parent != _ashParent)
            {   JumpAsh.transform.SetParent(_ashParent);
                JumpAsh.transform.localPosition = _ashPos;
            }
            JumpAsh.SetActive(true);
            JumpAsh.transform.SetParent(JumpAsh.transform.root);
        }

        public override void PhysicExcute()
        {
            if (_isSpeedUp)
            {
                MoveBehaviour(PlayerData.MaxSpeedX,PlayerData.AccelerationX);
                JumpBehaviour(PlayerData.MaxSpeedY,PlayerData.AccelerationY);
            }
            else
            {
                MoveBehaviour(PlayerData.MaxSpeedX,PlayerData.AccelerationX);
            }

            if (Rigidbody2DOwner.velocity.y < 0)
            {
                StateMachine.ChangeState(Owner.PlayerStateDic[PlayerStateType.OnAirStatePlayer]);
            }

        }
        public override void LogicExcute()
        {
            SwitchState();
        }

        public override void Exit()
        {
            base.Exit();
            Owner.StartCoroutine(CloseJumpAsh());
        }

        IEnumerator CloseJumpAsh()
        {
            var animator = JumpAsh.GetComponent<Animator>();
            yield return new WaitUntil(() =>
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95||Owner.IsGroundThreeRays);
            JumpAsh.transform.SetParent(_ashParent);
            JumpAsh.transform.localPosition = _ashPos;
            JumpAsh.SetActive(false);
        }
        /*protected override void MoveBehaviour(float speed)
        {
            
            Rigidbody2DOwner.velocity = new Vector2(Owner.XAxis*speed ,Rigidbody2DOwner.velocity.y);
            if (Owner.XAxis<0)
            {
                TransformOwner.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                TransformOwner.localScale = new Vector3(1, 1, 1);
            }
        }*/
        
        protected void JumpBehaviour(float maxSpeedY,float accelerationY)
        {
            float velocityY = Mathf.Clamp(Rigidbody2DOwner.velocity.y + accelerationY * Time.deltaTime,-maxSpeedY,maxSpeedY);
            Rigidbody2DOwner.velocity = new Vector2(Rigidbody2DOwner.velocity.x, velocityY);
        }
        IEnumerator JumpChange()
        {
            yield return new WaitForSeconds(PlayerData.YSpeedUpTime);
            _isSpeedUp = false;
        }
        
        //protected float CalculateJumpY()
        //{
           // return Mathf.Lerp(_jumpOriginalY, _jumpOriginalY+PlayerData.JumpHeight, _yLerp+=Time.deltaTime*PlayerData.JumpSpeed);
        //}
        
        // ReSharper disable Unity.PerformanceAnalysis
       /* IEnumerator YaxisVelocityTest()
        {
            while(true)
            {
                if (Rigidbody2DOwner.velocity.y <= 0)
                {
                    StateMachine.ChangeState(Owner.PlayerStateDic[PlayerStateType.OnAirStatePlayer]);
                }
                yield return new WaitForSeconds(0.1f);
                if (StateMachine.CurrentState != this)
                    yield break;
            }
        }*/
       private void SwitchState()
       {
           RaycastHit2D bodyFrontHit2D = Owner.BodyFrontHit2D;
           RaycastHit2D headFrontHit2D = Owner.HeadFrontHit2D;
           if (!bodyFrontHit2D&&headFrontHit2D.collider)
           {
               HandRevision(headFrontHit2D.point);
               StateMachine.ChangeState(Owner.PlayerStateDic[PlayerStateType.HangingStatePlayer]);
           }
           if(Input.GetKeyDown(KeyCode.J)&& PressJKeyCount==0)
               StateMachine.ChangeState(Owner.PlayerStateDic[PlayerStateType.AttackState1Player]);
           if(Input.GetKeyDown(KeyCode.Q))
               StateMachine.ChangeState(Owner.PlayerStateDic[PlayerStateType.DashStatePlayer]);
       }
       
       private void HandRevision(Vector2 hitPoint)
       {
           Vector2 handPoint = OwnerCore.GetCoreComponent<CollisionComponent>().HandTrans.position;
           Vector2 revisionPoint = hitPoint + (Vector2)TransformOwner.position - handPoint;
           TransformOwner.position = revisionPoint;
       }

       public JumpStatePlayer(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
       {
           _ashParent = JumpAsh.transform.parent;
           _ashPos = JumpAsh.transform.localPosition;
           JumpAsh.SetActive(false);
       }
    }
}