using System;
using System.Collections;
using C_Script.BaseClass;
using C_Script.Common.Model.EventCentre;
using C_Script.Player.Component;
using C_Script.Player.Data;
using C_Script.StaticWay;
using UnityEngine;
using UnityEngine.Serialization;



namespace C_Script.Object
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Meteorite : MonoBehaviour
    {
        private Rigidbody2D _meteoriteRb;
        private Vector2 _toTarget;
        [SerializeField]
        private uint fallPower;
        [SerializeField]
        private float aliveTime;
        [SerializeField] 
        private float damageAmount;
        private float _startTime;

        private void OnEnable()
        {
            Vector2 playerPos  = GameObject.FindWithTag(nameof(Player)).transform.position;
            _meteoriteRb = GetComponent<Rigidbody2D>();
            _meteoriteRb.AddForce((_toTarget = playerPos -(Vector2)transform.position)*fallPower,ForceMode2D.Impulse);
            _startTime = Time.unscaledTime;
            Invoke(nameof(Recovery), aliveTime);
        }
        
        private void Recovery() => Destroy(gameObject);
        
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerHurt);
                collision.GetComponentInChildren<PlayerHealth>()
                    .PlayerDamage(damageAmount,StaticFunction.CalculateSpecularDir(_toTarget),ForceDirection.Up);
                Destroy(gameObject);
            }
        }
    }

}
