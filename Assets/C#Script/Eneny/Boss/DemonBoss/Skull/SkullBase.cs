using C_Script.BaseClass;
using C_Script.Common.Model.ObjectPool;
using C_Script.Player.Component;
using UnityEngine;

namespace C_Script.Eneny.Boss.DemonBoss.Skull
{
    public class SkullBase : MonoBehaviour
    {
        [SerializeField] private float fireTime;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float amount;
        [SerializeField] private float DeathTime;
        private Vector2 _originPos;
        private int _trigger=0;
    
        private Transform PlayerTrans => _playerTrans? _playerTrans : _playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        private Transform _playerTrans;
        private Rigidbody2D Rb => _rb? _rb : _rb = GetComponent<Rigidbody2D>();
        private Rigidbody2D _rb;
        private Animator Antor => _animator ? _animator : _animator = GetComponent<Animator>();
        private Animator _animator;
        private int _count;
        private int _currentCount;
        private static readonly int Move = Animator.StringToHash("Move");

        private void Awake()
        {
            _originPos = transform.position;
            BigObjectPool.Instance.PushObject(ObjectType.Skull,gameObject);
        }

        private void OnEnable()
        {
            _trigger = 0;
            transform.position = _originPos;
            transform.rotation = Quaternion.identity;
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
            Invoke(nameof(Fire),fireTime);
            Invoke(nameof(SetActiveFalse),DeathTime);
            _currentCount++;
        }


        void Update()
        {
            //transform.localEulerAngles = new Vector3(0, 0, -transform.parent.localEulerAngles.z);
        }

        private void Fire()
        {
            if (++_count != _currentCount) return;
            _trigger = 1;
            Antor.SetBool(Move,true);
            Vector2 moveDir = (PlayerTrans.position -transform.position).normalized;
            if (moveDir.x <= 0)
            {
                var angle = Mathf.Atan2(-moveDir.y, -moveDir.x) * Mathf.Rad2Deg;
                var trailRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = trailRotation;
            }
            else
            {
                var angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
                transform.localScale = new Vector3(-0.5f,0.5f,1);
                var trailRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = trailRotation;
            }
            Rb.velocity = moveDir * moveSpeed;
        }

        private void SetActiveFalse()
        {
            if (_count != _currentCount) return;
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player")&&_trigger == 1)
            {
                col.transform.GetComponentInChildren<PlayerHealth>().PlayerDamage(amount,new Vector2(transform.eulerAngles.z<180? -1:1,0),ForceDirection.Forward);
                gameObject.SetActive(false);
            }
            else if (col.CompareTag("Ground"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
