using System.Collections;
using C_Script.BaseClass;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace C_Script.Eneny.Boss.SwordSaint.State
{
    public class ComboStateSwordSaint: SwordSaintState
    {
        private string _comboAnimName1;
        private string _comboAnimName2;
        private string _comboAnimName3;
        private string _comboAnimName4;
        private bool _comboStart1;
        private bool _comboStart2;
        private bool _comboStart3;
        private bool _comboStart4;
        private bool _comboFinished1;
        private bool _comboFinished2;
        private bool _comboFinished3;
        private bool _comboFinished4;

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
            SwordScar.SetActive(true);
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
            _comboStart1 = false;
            _comboStart2 = false;
            _comboStart3 = false;
            _comboStart4 = false;
            _comboFinished1 = false;
            _comboFinished2 = false;
            _comboFinished3 = false;
            _comboFinished4 = false;
            SwordSaintData.IsHurt = false;
        }

        IEnumerator WaitAnimationFinish()
        {
            yield return new WaitUntil(() => AnimatorOwner.GetCurrentAnimatorStateInfo(0).IsName(_comboAnimName1));
            
            _comboStart1 = true;
            BossFactory.warningSign.SetActive(true);
            
            yield return new WaitUntil(() => AnimatorOwner.GetCurrentAnimatorStateInfo(0).normalizedTime>0.95f);
            
            _comboFinished1 = true;
            BossFactory.warningSign.SetActive(false);
            
            yield return new WaitUntil(() => AnimatorOwner.GetCurrentAnimatorStateInfo(0).IsName(_comboAnimName2));
            
            _comboStart2 = true;
            SwordScar.transform.position = TargetTrans.position + (Vector3)SwordSaintData.SwordScarRelativePos1; 
            SwordScar.GetComponent<Animator>().SetTrigger("Scar1");
            Owner.StartCoroutine(ComboBehaviour(_comboAnimName2,
                new Vector2(SwordSaintData.Attack1Direction.x *TransformOwner.localScale.x ,SwordSaintData.Attack1Direction.y),ForceDirection.Up));
            
            yield return new WaitUntil(() => AnimatorOwner.GetCurrentAnimatorStateInfo(0).normalizedTime>0.95f);
            
            _comboFinished2 = true;
            SwordScar.transform.position = TargetTrans.position + (Vector3)SwordSaintData.SwordScarRelativePos2; 
            
            yield return new WaitUntil(() => AnimatorOwner.GetCurrentAnimatorStateInfo(0).IsName(_comboAnimName3));
            
            _comboStart3 = true;
            Owner.StartCoroutine(ComboBehaviour(_comboAnimName3,
                new Vector2(SwordSaintData.Attack2Direction.x *TransformOwner.localScale.x ,SwordSaintData.Attack2Direction.y),ForceDirection.Down));
            SwordScar.GetComponent<Animator>().SetTrigger("Scar2");
            
            
            yield return new WaitUntil(() => AnimatorOwner.GetCurrentAnimatorStateInfo(0).normalizedTime>0.95f);
            
            _comboFinished3 = true;
            
            yield return new WaitUntil(() => AnimatorOwner.GetCurrentAnimatorStateInfo(0).IsName(_comboAnimName4));
            
            _comboStart4 = true;
            SwordScar.transform.position = TargetTrans.position + (Vector3)SwordSaintData.SwordScarRelativePos3; 
            Owner.StartCoroutine(ComboBehaviour(_comboAnimName4,
                new Vector2(SwordSaintData.Attack3Direction.x *TransformOwner.localScale.x ,SwordSaintData.Attack3Direction.y),ForceDirection.Forward));
            SwordScar.GetComponent<Animator>().SetTrigger("Scar3");

            yield return new WaitUntil(() => AnimatorOwner.GetCurrentAnimatorStateInfo(0).normalizedTime>0.95f);
            
            _comboFinished4 = true;
        }

        private void SwitchState()
        {
            if (SwordSaintData.IsDeath)
                StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.DeathStateEnemy]);
        }
    }
}