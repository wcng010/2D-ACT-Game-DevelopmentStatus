using C_Script.BaseClass;
using UnityEngine;

namespace C_Script.Eneny.Boss.SwordSaint.State
{
    public class DodgeStateSwordSaint : SwordSaintState
    {
        public DodgeStateSwordSaint(SwordSaintBase owner, string nameToTrigger, string animationName) : base(owner, nameToTrigger, animationName)
        {
            
        }
        public override void Enter()
        {
            base.Enter();
            Rigidbody2DOwner.AddForce(new Vector2(Mathf.Cos(SwordSaintData.DodgeAngle)*-TransformOwner.localScale.x,
                Mathf.Sin(SwordSaintData.DodgeAngle))*SwordSaintData.DodgeForce,ForceMode2D.Impulse);
        }

        public override void LogicExcute()
        {
            if (IsAniamtionFinshed)
                StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.ComboAttackStateEnemy]);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}