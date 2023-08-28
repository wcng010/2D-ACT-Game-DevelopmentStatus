using System;
using System.Collections;
using C_Script.BaseClass;
using C_Script.Common.Model.EventCentre;
using C_Script.Player.Component;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.Object
{
    public class SwordScarBase : MonoBehaviour
    {
        private Animator _animator;
        private BoxCollider2D _boxCollider2D;
        private bool _isHit1;
        private bool _isHit2;
        private bool _isHit3;
        [SerializeField]private float damageAmount;
        
        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _isHit1 = false;
            _isHit2 = false;
            _isHit3 = false;
            //StartCoroutine(BoxChangeWithAnim());
        }

        IEnumerator BoxChangeWithAnim()
        {
            
            _boxCollider2D.enabled = false;
            yield return new WaitUntil(() =>_animator.GetCurrentAnimatorStateInfo(0).IsName("SwordScar1") 
                                            &&_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f);
            _boxCollider2D.enabled = true;
            Vector2 offset = new Vector2(0.14f,0.03f);
            Vector2 size = new Vector2(0.1f, 0.4f);
            SetBoxSizeAndOffset(offset,size);
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("SwordScar2"));
            _boxCollider2D.enabled = false;
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f);
            _boxCollider2D.enabled = true;
            offset.Set(0.24f,0);
            size.Set(0.165f,0.237f);
            SetBoxSizeAndOffset(offset,size);
            
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("SwordScar3"));
            _boxCollider2D.enabled = false;
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f);
            _boxCollider2D.enabled = true;
            offset.Set(-0.14f,-0.1f);
            size.Set(0.38f,0.115f);
            SetBoxSizeAndOffset(offset,size);
        }

        private void SetBoxSizeAndOffset(Vector2 offset,Vector2 size)
        {
            _boxCollider2D.size = size;
            _boxCollider2D.offset = offset;
        }

        
        
        
        private void OnTriggerStay2D(Collider2D col)
        {
            if (String.Compare(col.tag, "Player", StringComparison.Ordinal) == 0)
            {
                if (!_isHit1 && _animator.GetCurrentAnimatorStateInfo(0).IsName("SwordScar1"))
                {
                    col.transform.GetComponentInChildren<PlayerHealth>()
                        .PlayerDamage(damageAmount, Vector2.up, ForceDirection.Up);
                    CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerHurt);
                    _isHit1 = true;
                }
                else if (!_isHit2 && _animator.GetCurrentAnimatorStateInfo(0).IsName("SwordScar2"))
                {
                    col.transform.GetComponentInChildren<PlayerHealth>()
                        .PlayerDamage(damageAmount, Vector2.up, ForceDirection.Up);
                    CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerHurt);
                    _isHit2 = true;
                }
                else if(!_isHit3 && _animator.GetCurrentAnimatorStateInfo(0).IsName("SwordScar3"))
                {
                    col.transform.GetComponentInChildren<PlayerHealth>()
                        .PlayerDamage(damageAmount, Vector2.up, ForceDirection.Up);
                    CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerHurt);
                    _isHit3 = true;
                }
            }
        }
    }
}
