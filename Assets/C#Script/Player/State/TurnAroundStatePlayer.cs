using C_Script.Common.Model.ObjectPool;
using C_Script.Player.Base;
using C_Script.Player.State.BaseState;
using UnityEngine;
namespace C_Script.Player.StateModel
{
    public class TurnAroundStatePlayer : PlayerState
    {
        private float _deceVelocity;
        private readonly string _turnAroundAsh;


        public override void Enter()
        {
            base.Enter();
            _deceVelocity = Rigidbody2DOwner.velocity.x;
            AshObjectPool.Instance.SetActive(_turnAroundAsh);
        }

        public override void PhysicExcute()
        {
            base.PhysicExcute();
            Retardance();
        }

        private void Retardance()
        {
            _deceVelocity = Mathf.Lerp(_deceVelocity,PlayerData.DecelerationSpeed,Time.fixedDeltaTime*PlayerData.Deceleration);
            Rigidbody2DOwner.velocity = new Vector2(_deceVelocity, Rigidbody2DOwner.velocity.y);
        }

        public override void Exit()
        {
            base.Exit();
            TransformOwner.localScale = new Vector3(-TransformOwner.localScale.x, 1, 1);
        }
        
        public TurnAroundStatePlayer(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            AshObjectPool.Instance.PushObject(GameObject.Instantiate(PlayerData.TurnAroundAsh,AshObjectPool.Instance.transform));
            _turnAroundAsh = PlayerData.TurnAroundAsh.name;
        }
    }
}