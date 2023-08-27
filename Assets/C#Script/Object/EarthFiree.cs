using System;
using System.Collections;
using C_Script.BaseClass;
using C_Script.Common.Model.EventCentre;
using C_Script.Player.Component;
using UnityEngine;
using UnityEngine.Serialization;


namespace C_Script.Object
{
    public class EarthFiree : MonoBehaviour
    {
        public float fireAmount;
        public float damageTimeInterval;
        private bool _inFire;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _inFire = true;
                StartCoroutine(Damge(other.transform));
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _inFire = false;
            }
        }

        private IEnumerator Damge(Transform player)
        {
            while (_inFire)
            {
              player.GetComponentInChildren<PlayerHealth>().FireDamage(fireAmount);
              CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerHurt);
              yield return new WaitForSeconds(damageTimeInterval);
            }
        }
    }
}
