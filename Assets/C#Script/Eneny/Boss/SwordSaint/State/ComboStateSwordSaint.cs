using System.Collections;
using C_Script.BaseClass;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace C_Script.Eneny.Boss.SwordSaint.State
{
    public class ComboStateSwordSaint: SwordSaintState
    {
        private readonly string _comboAnimName1;
        private readonly string _comboAnimName2;
        private readonly string _comboAnimName3;
        private readonly string _comboAnimName4;
        private bool _comboFinished4;
        private int _skillButtonInternal;

        private GameObject SwordScar =>
            _swordScar ? _swordScar : _swordScar = UnityEngine.Object.Instantiate(SwordSaintData.SwordScar,SwordSaintModel.transform);
        private GameObject _swordScar;

        public ComboStateSwordSaint(SwordSaintBase owner, string nameToTrigger, string comboAnimName1,string comboAnimName2, string comboAnimName3,string comboAnimName4) : base(owner, nameToTrigger, null)
        {
            _comboAnimName1 = comboAnimName1;
            _comboAnimName2 = comboAnimName2;
            _comboAnimName3 = comboAnimName3;
            _comboAnimName4 = comboAnimName4;
            SwordScar.SetActive(false);
        }

        public override void Enter()
        {
            
            AnimatorOwner.SetBool(NameToTrigger,true);
            if(SwordSaintData.SkillButton1)
            {
                SwordScar.SetActive(true);
                _skillButtonInternal = 1;
            }
            else
            {
                _skillButtonInternal = 0;
            }
            Owner.StartCoroutine(WaitAnimationFinish());
            var toTargetVector2 = SwordSaintModel.TargetTrans.position - TransformOwner.position;
            TransformOwner.localScale = new Vector3(toTargetVector2.x/Mathf.Abs(toTargetVector2.x), 1, 1);
        }

        public override void PhysicExcute()
        {
            
        }

        public override void LogicExcute()
        {
            if (_comboFinished4)
                StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.ReadyStateEnemy]);
            SwitchState();
        }

        public override void Exit()
        {
            AnimatorOwner.SetBool(NameToTrigger,false);
            BossFactory.warningSign.SetActive(false);
            SwordScar.SetActive(false);
            _comboFinished4 = false;
            SwordSaintData.IsHurt = false;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        IEnumerator WaitAnimationFinish()
        {
            yield return new WaitUntil(() => AnimatorOwner.GetCurrentAnimatorStateInfo(0).IsName(_comboAnimName1)); ReadyAnimationStart();
            yield return new WaitUntil(() => AnimatorOwner.GetCurrentAnimatorStateInfo(0).normalizedTime>0.95f); ReadyAnimationFininsh();
            yield return new WaitUntil(() => AnimatorOwner.GetCurrentAnimatorStateInfo(0).IsName(_comboAnimName2)); Attack1AnimationStart();
            yield return new WaitUntil(() => AnimatorOwner.GetCurrentAnimatorStateInfo(0).IsName(_comboAnimName3)); Attack2AnimationStart();
            yield return new WaitUntil(() => AnimatorOwner.GetCurrentAnimatorStateInfo(0).IsName(_comboAnimName4)); Attack3AnimationStart();
            yield return new WaitUntil(() => AnimatorOwner.GetCurrentAnimatorStateInfo(0).normalizedTime>0.95f); Attack3AnimationFininsh();
        }

        private void SwitchState()
        {
            if (SwordSaintData.IsDeath)
                StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.DeathStateEnemy]);
        }

        private void ReadyAnimationStart()
        {
            BossFactory.warningSign.SetActive(true);
        }
        
        private void ReadyAnimationFininsh()
        {
            BossFactory.warningSign.SetActive(false);
        }
        
        private void Attack1AnimationStart()
        {
            SwordScarChange("Scar1",SwordSaintData.SwordScarRelativePos1);
            Owner.StartCoroutine(ComboBehaviour(_comboAnimName2,
                new Vector2(SwordSaintData.Attack1Direction.x *TransformOwner.localScale.x ,SwordSaintData.Attack1Direction.y),ForceDirection.Up));
        }
        private void Attack1AnimationFininsh()
        {
            
        }
        private void Attack2AnimationStart()
        {
            SwordScarChange("Scar2",SwordSaintData.SwordScarRelativePos2);
            Owner.StartCoroutine(ComboBehaviour(_comboAnimName3,
                new Vector2(SwordSaintData.Attack2Direction.x *TransformOwner.localScale.x ,SwordSaintData.Attack2Direction.y),ForceDirection.Down));
        }
        private void Attack2AnimationFininsh()
        {
            
        }
        private void Attack3AnimationStart()
        {
            SwordScarChange("Scar3", SwordSaintData.SwordScarRelativePos3);
            Owner.StartCoroutine(ComboBehaviour(_comboAnimName4,
                new Vector2(SwordSaintData.Attack3Direction.x *TransformOwner.localScale.x ,SwordSaintData.Attack3Direction.y),ForceDirection.Forward));

        }
        private void Attack3AnimationFininsh()
        {
            _comboFinished4 = true;
        }
        
        private void SwordScarChange(string TriggerName,Vector3 RelativePos)
        {
            if (_skillButtonInternal == 1)
            {
                SwordScar.transform.position = TargetTrans.position + RelativePos;
                SwordScar.GetComponent<Animator>().SetTrigger(TriggerName);
            }
        }
    }
}