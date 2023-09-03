using System;
using System.Collections;
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
        private bool _isFire;
        private bool _isBreak;
        private float _liveTime;
        private Vector3 _originalPos;
        private float _xLerp;
        private static readonly int IsFire = Animator.StringToHash("IsFire");
        private static readonly int IsBreak = Animator.StringToHash("IsBreak");
        public string ObjectNames() => gameObject.name;
        public GameObject GameObj() => gameObject;
        public void FireObject(Transform ownerTrans)
        {
            var position = ownerTrans.position;
            Instantiate(gameObject, new Vector3(position.x+ownerTrans.localScale.x*0.2f,position.y,position.z),new Quaternion(0, ownerTrans.localScale.x>0? 0:180,0,0),GameManager.Instance.ObjectPoolTrans);
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator.SetBool(IsFire,true);
            StartCoroutine(nameof(CheckFire));
            _originalPos = transform.position;
        }


        private void Update()
        {
            if (_isFire&&!_isBreak)
            {
                /*_xLerp = Mathf.Lerp(_originalPos.x, _originalPos.x + 5*transform.localScale.x, _xLerp+=Time.fixedDeltaTime*0.1f);
                Debug.Log(_xLerp);
                var pos = transform.position;
                pos = new Vector3(_xLerp, pos.y, pos.z);
                transform.position = pos;*/
                _rigidbody2D.AddForce(new Vector2(fireForce*(transform.rotation.y>0?-1:1),0),ForceMode2D.Impulse);
                _isFire = false;
            }

            if (_isBreak)
            {
                _rigidbody2D.velocity = new Vector2(0, 0);
            }

            if ((_liveTime+= Time.unscaledDeltaTime) >= liveTime)
            {
                BreakDown();   
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Ground"))
            {
                BreakDown();
            }
            if (col.CompareTag("Enemy")&&!_isBreak)
            {
                col.transform.GetComponentInChildren<EnemyHealth>()?.EnemyDamageWithPower(amount,new Vector2(transform.rotation.y>0? -1:1,0),stunRate);
                BreakDown();
            } 
        }

        IEnumerator CheckFire()
        {
            yield return new WaitUntil(()=>_animator.GetCurrentAnimatorStateInfo(0).IsName("WaterMove"));
            _isFire = true;
            yield return null;
        }

        private void BreakDown()
        {
            _isBreak = true;
            _animator.SetTrigger(IsBreak);
            StartCoroutine(nameof(CheckBreak));
        }

        IEnumerator CheckBreak()
        {
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("WaterBreak"));
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f);
            if(gameObject)Destroy(gameObject);
        }

    }
}
