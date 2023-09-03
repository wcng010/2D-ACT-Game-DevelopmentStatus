using System.Collections;
using C_Script.Player.BaseClass;
using C_Script.Player.StateModel.BaseState;
using UnityEngine;

namespace C_Script.Player.StateModel
{
    public class AttackState2Player : PlayerState
    {
        private static readonly int Attack2 = Animator.StringToHash("Attack2");
        private static readonly int Attack1 = Animator.StringToHash("Attack1");
        private static readonly int Count = Animator.StringToHash("Count");
        public override void Enter()
        {
            base.Enter();
            AnimatorOwner.SetInteger(Count,1);
            var vec = Rigidbody2DOwner.velocity;
            Rigidbody2DOwner.velocity = new Vector2(0, 0);
            Owner.StartCoroutine(WaitForLastAnimation());
        }
        /// <summary>
        /// 由于从Attack1到Attack2时，Attack2添加协程等待Attack1动画播完，
        /// 所以AnimatorIsPlaying会因为当前播放的动画不是Attack2的动画而跳出状态，这不是我们想要的。
        /// 所以需要在前面加一个判定使当前动画为Attack2动画才可退出。
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        public override void Exit()
        {
            AnimatorOwner.SetBool(Attack1,false);
            AnimatorOwner.SetBool(Attack2,false);
            AnimatorOwner.SetInteger(Count,0);
            Time.timeScale = 1;
        }
        protected virtual void AdjustPosition(Transform playertrans)
        {
            Vector2 point = playertrans.position;
            playertrans.position 
                = new Vector3((point.x + 0.25f*playertrans.localScale.x), point.y, 0);
        }
        /// <summary>
        /// 协程延迟Attack2的
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitForLastAnimation()
        {
            yield return new WaitUntil(()=>IsAnimationName);
            Owner.StartCoroutine(AttackRayTestWithPower(PlayerData.Attack2Range,AnimationTime,
                "player_Attack2",Collider2DOwner.size.y/2));
        }
        public AttackState2Player(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            
        }
    }
}
