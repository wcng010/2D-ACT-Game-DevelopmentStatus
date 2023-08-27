using System.Collections;
using C_Script.BaseClass;
using UnityEngine;
using UnityEngine.UIElements;

namespace C_Script.Eneny.Boss.SwordSaint.State
{
    public class ComboStateSwordSaint: SwordSaintState
    {
        private string _comboAnimName1;
        private string _comboAnimName2;
        private string _comboAnimName3;
        private bool _comboStart1;
        private bool _comboStart2;
        private bool _comboStart3;
        private bool _comboFinished1;
        private bool _comboFinished2;
        private bool _comboFinished3;
        public ComboStateSwordSaint(SwordSaintBase owner, string nameToTrigger, string comboAnimName1,string comboAnimName2, string comboAnimName3) : base(owner, nameToTrigger, null)
        {
            _comboAnimName1 = comboAnimName1;
            _comboAnimName2 = comboAnimName2;
            _comboAnimName3 = comboAnimName3;
        }

        public override void Enter()
        {
            AnimatorOwner.SetBool(NameToTrigger,true);
            Owner.StartCoroutine(WaitAnimationFinish());
        }

        public override void PhysicExcute()
        {
            
        }

        public override void LogicExcute()
        {
            if (_comboFinished3)
                StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.ReadyStateEnemy]);
            SwitchState();
        }

        public override void Exit()
        {
            AnimatorOwner.SetBool(NameToTrigger,false);
            _comboStart1 = false;
            _comboStart2 = false;
            _comboStart3 = false;
            _comboFinished1 = false;
            _comboFinished2 = false;
            _comboFinished3 = false;
            SwordSaintData.IsHurt = false;
        }

        IEnumerator WaitAnimationFinish()
        {
            yield return new WaitUntil(() => AnimatorOwner.GetCurrentAnimatorStateInfo(0).IsName(_comboAnimName1));
            _comboStart1 = true;
            Owner.StartCoroutine(ComboBehaviour(_comboAnimName1,
                new Vector2(SwordSaintData.Attack1Direction.x *TransformOwner.localScale.x ,SwordSaintData.Attack1Direction.y),ForceDirection.Up));
            yield return new WaitUntil(() => AnimatorOwner.GetCurrentAnimatorStateInfo(0).normalizedTime>0.95f);
            _comboFinished1 = true;
            yield return new WaitUntil(() => AnimatorOwner.GetCurrentAnimatorStateInfo(0).IsName(_comboAnimName2));
            Owner.StartCoroutine(ComboBehaviour(_comboAnimName2,
                new Vector2(SwordSaintData.Attack2Direction.x *TransformOwner.localScale.x ,SwordSaintData.Attack2Direction.y),ForceDirection.Down));
            _comboStart2 = true;
            yield return new WaitUntil(() => AnimatorOwner.GetCurrentAnimatorStateInfo(0).normalizedTime>0.95f);
            _comboFinished2 = true;
            yield return new WaitUntil(() => AnimatorOwner.GetCurrentAnimatorStateInfo(0).IsName(_comboAnimName3));
            Owner.StartCoroutine(ComboBehaviour(_comboAnimName3,
                new Vector2(SwordSaintData.Attack3Direction.x *TransformOwner.localScale.x ,SwordSaintData.Attack3Direction.y),ForceDirection.Forward));
            _comboStart3 = true;
            yield return new WaitUntil(() => AnimatorOwner.GetCurrentAnimatorStateInfo(0).normalizedTime>0.95f);
            _comboFinished3 = true;
        }

        private void SwitchState()
        {
            if (SwordSaintData.IsDeath)
                StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.DeathStateEnemy]);
        }
    }
}