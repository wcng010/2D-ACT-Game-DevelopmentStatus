using System;
using System.Collections;
using System.Collections.Generic;
using C_Script.BaseClass;
using C_Script.Player.Component;
using UnityEngine;
using UnityEngine.Serialization;

public class SkullBase : MonoBehaviour
{
    enum SkullState
    {
        AnimationState,
        CommonState
    }

    private float _timer;
    [SerializeField] private float fireTime;
    [SerializeField] private SkullState state;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float amount;
    private int _trigger=0;
    private Transform _playerTrans;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Collider2D _collider2D;
    private Vector2 _playerOriPos;
    private static readonly int Move = Animator.StringToHash("Move");

    void Start()
    {
        _playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<Collider2D>();
        _collider2D.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (state == SkullState.AnimationState)
        {
            transform.localEulerAngles = new Vector3(0, 0, -transform.parent.localEulerAngles.z);
        }

        if (_timer > fireTime&&_trigger==0)
        {
            _collider2D.isTrigger = false;
            _trigger = 1;
            _animator.SetBool(Move,true);
            _playerOriPos = _playerTrans.position;
            Vector2 moveDir = (_playerOriPos - (Vector2)transform.position).normalized;
            _rigidbody2D.velocity = moveDir * moveSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player")&&_trigger==1)
        {
            col.transform.GetComponentInChildren<PlayerHealth>().PlayerDamage(amount,new Vector2(transform.eulerAngles.z>180? -1:1,0),ForceDirection.Forward);
        }
    }
}
