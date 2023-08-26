using C_Script.Player.BaseClass;
using C_Script.Player.StateModel.BaseState;
using UnityEngine;
namespace C_Script.Player.StateModel
{
    public class TurnAroundStatePlayer : PlayerState
    {
        private float DeceVelocity;
        private GameObject TurnAsh { 
            get {
                if(!_turnAsh)
                    _turnAsh = GameObject.FindWithTag(nameof(TurnAsh));
                return _turnAsh;
            } }
        private GameObject _turnAsh;
        
        public override void Enter()
        {
            base.Enter();
            DeceVelocity = Rigidbody2DOwner.velocity.x;
            _turnAsh.SetActive(true);
        }

        public override void PhysicExcute()
        {
            base.PhysicExcute();
            Retardance();
        }

        private void Retardance()
        {
            DeceVelocity = Mathf.Lerp(DeceVelocity,PlayerData.DecelerationSpeed,Time.fixedDeltaTime*PlayerData.Deceleration);
            Rigidbody2DOwner.velocity = new Vector2(DeceVelocity, Rigidbody2DOwner.velocity.y);
        }

        public override void Exit()
        {
            base.Exit();
            TransformOwner.localScale = new Vector3(-TransformOwner.localScale.x, 1, 1);
            _turnAsh.SetActive(false);
        }
        
        public TurnAroundStatePlayer(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            _turnAsh = GameObject.FindWithTag(nameof(TurnAsh));
            _turnAsh.SetActive(false);
        }
    }
}