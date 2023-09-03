using System;
using System.Collections;
using System.Collections.Generic;
using C_Script.BaseClass;
using C_Script.Common.Model.ObjectPool;
using C_Script.Player.Component;
using UnityEngine;
using UnityEngine.Serialization;

public class SkullBase : MonoBehaviour
{
    
    private float _timer;
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

    private static readonly int Move = Animator.StringToHash("Move");

    private void Awake()
    {
        _originPos = transform.position;
        BigObjectPool.Instance.PushObject(ObjectType.Skull,gameObject);
    }

    private void OnEnable()
    {
        _timer = 0;
        _trigger = 0;
        transform.position = _originPos;
    }


    void Update()
    {
        _timer += Time.deltaTime;
        transform.localEulerAngles = new Vector3(0, 0, -transform.parent.localEulerAngles.z);
        if (_timer > fireTime&&_trigger==0)
        {
            _trigger = 1;
            Antor.SetBool(Move,true);
            Vector2 moveDir = (PlayerTrans.position -transform.position).normalized;
            Rb.velocity = moveDir * moveSpeed;
        }
        if (_timer > DeathTime)
        {
            gameObject.SetActive(false);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player")&&_trigger == 1)
        {
            col.transform.GetComponentInChildren<PlayerHealth>().PlayerDamage(amount,new Vector2(transform.eulerAngles.z>180? -1:1,0),ForceDirection.Forward);
            gameObject.SetActive(false);
        }
    }
}
