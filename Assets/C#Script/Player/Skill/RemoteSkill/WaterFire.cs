using System;
using System.Collections;
using C_Script.Common.Model.ObjectPool;
using C_Script.Eneny.EnemyCommon.Component;
using C_Script.Eneny.Monster.Magician.Component;
using C_Script.Interface;
using C_Script.Manager;
using C_Script.Player.Component;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.Player.Skill.RemoteSkill
{
    public class WaterFire : MonoBehaviour,IFireObject
    {
        [FoldoutGroup("Setting")]
        public float liveTime;
        [FoldoutGroup("Setting")]
        public float fireForce;
        [FoldoutGroup("Setting")] 
        public float amount;
        [FoldoutGroup("Setting")] 
        public float stunRate;
        private Animator _animator;
        private Rigidbody2D _rigidbody2D;
        private Transform _ownerTrans;
        private Transform _myTrans;
        private bool _isBreak;
        private static readonly int IsFire = Animator.StringToHash("IsFire");
        private static readonly int IsBreak = Animator.StringToHash("IsBreak");
        public ObjectType FireObjectType()=> ObjectType.WaterWave;
        public void FireObject()
        {
            BigObjectPool.Instance.SetOneActive(FireObjectType());
        }

        private void Awake()
        {
            _myTrans = transform;
            _ownerTrans = GameObject.FindWithTag("Player").transform;
            _animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _animator.SetBool(IsFire,true);
            StartCoroutine(nameof(CheckFire));
            var position = _ownerTrans.position;
            var localScale = _ownerTrans.localScale;
            _myTrans.position = new Vector3(position.x+localScale.x*0.2f,position.y,position.z);
            _myTrans.eulerAngles = new Vector3(0, localScale.x>0? 0:180,0);
            Invoke(nameof(BreakDown),liveTime);
            _isBreak = false;
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Ground"))
            {
                BreakDown();
            }
            if (col.CompareTag("Enemy")&&!_isBreak)
            {
                col.transform.GetComponentInChildren<EnemyHealth>()?.EnemyDamageWithPower(amount,new Vector2(transform.rotation.y>0? -1:1,0));
                BreakDown();
            } 
        }

        IEnumerator CheckFire()
        {
            yield return new WaitUntil(()=>_animator.GetCurrentAnimatorStateInfo(0).IsName("WaterMove"));
            _rigidbody2D.AddForce(new Vector2(fireForce*(_ownerTrans.localScale.x>0?1:-1),0),ForceMode2D.Impulse);
            yield return null;
        }

        private void BreakDown()
        {
            if(_isBreak) return;
            _isBreak = true;
            _rigidbody2D.velocity = new Vector2(0, 0);
            _animator.SetTrigger(IsBreak);
            StartCoroutine(nameof(CheckBreak));
        }

        IEnumerator CheckBreak()
        {
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("WaterBreak"));
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f);
            gameObject.SetActive(false);
        }

    }
}
