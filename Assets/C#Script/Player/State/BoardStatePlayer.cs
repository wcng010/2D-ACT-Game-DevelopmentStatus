using System.Numerics;
using C_Script.Player.BaseClass;
using C_Script.Player.StateModel.BaseState;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace C_Script.Player.StateModel
{

    public class BoardStatePlayer:PlayerState
    {

        private Vector2 _workspace;
        private readonly float _speed=10f;
        private Vector2 _landPoint = Vector2.zero;
        public override void Enter()
        {
            base.Enter();
            _landPoint = Vector2.zero;
            _landPoint = LandPoint();
            Collider2DOwner.enabled = false;
            Rigidbody2DOwner.gravityScale = 0;
        }
        public override void PhysicExcute()
        {
            
        }

        public override void LogicExcute()
        {
            base.LogicExcute();
            LandGround(_landPoint);
        }

        public override void Exit()
        {
            base.Exit();
            Collider2DOwner.enabled = true;
            Rigidbody2DOwner.gravityScale = PlayerData.GravityScale;
        }
        private void LandGround(Vector2 landPoint)
        {
            if(landPoint!= Vector2.zero)
                TransformOwner.position = Vector2.Lerp(TransformOwner.position, landPoint, Time.deltaTime*_speed);
        }

        private Vector2 LandPoint()
        {
            RaycastHit2D hit2D = Owner.HeadFrontHit2D;
            if (hit2D)
            {
                var temp = hit2D.point;
                var limit = 0;
                while (limit++<20)
                {
                    temp += new Vector2(0, 0.05f);
                   RaycastHit2D hit = Physics2D.Raycast(temp, Vector2.up, 0.5f,LayerMask.NameToLayer("Ground"));
                   if (!hit)
                       return temp + new Vector2(0.1f*TransformOwner.localScale.x,PlayerData.IdleSizeY/2) ;
                }
            }
            return Vector2.zero;
        }
        
        public BoardStatePlayer(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            
        }
    }
}