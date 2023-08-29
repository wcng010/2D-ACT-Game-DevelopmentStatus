using System;
using System.Collections;
using System.Collections.Generic;
using C_Script.BaseClass;
using C_Script.Common.Model.EventCentre;
using C_Script.Player.Component;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


namespace C_Script.Object
{
    public class EarthFire : MonoBehaviour
    {
        [SerializeField]private float hitAmount;
        [SerializeField]private float damageTimeInterval;
        [SerializeField]private uint fireNums;
        private float _intervalTimeTemp;
        private float _timer;
        private bool _inFire;
        private bool _isPlayerInFire;
        private Transform _player;
        [SerializeField]private GameObject[] _warningFire;
        [SerializeField] private new int[] tag;
        public void SetPlayerInFire(Transform player)
        {
            _isPlayerInFire = true;
            _player = player;
        }

        public void OutOfTheFire()
        {
            _isPlayerInFire = false;
        }

        public void Warning()
        {
            tag = new int[fireNums];
            _warningFire=new GameObject[21];
            StartCoroutine(Damage());
            Debug.Log(transform.childCount);
            for (int i = 0; i < transform.childCount; i++)
            {
                _warningFire[i] = transform.GetChild(i).gameObject;
            }
            for (int i = 0; i < fireNums; ++i)
            {
                int randomValue = (int)(Random.value * 20);
                tag[i] = randomValue;
                _warningFire[randomValue].SetActive(true);
            }
        }

        public void OnFire()
        {
            for (int i = 0; i < fireNums; i++)
            {
                _warningFire[tag[i]].transform.GetChild(0).gameObject.SetActive(true);
                _warningFire[tag[i]].GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        public void FightFire()
        {
            for (int i = 0; i < fireNums; i++)
            {
                _warningFire[tag[i]].transform.GetChild(0).gameObject.SetActive(false);
                _warningFire[tag[i]].GetComponent<SpriteRenderer>().enabled = true;
            }

            foreach (var warnSign in _warningFire)
            {
                warnSign.SetActive(false);
            }
        }

        private void OnEnable()
        {
            tag = new int[fireNums];
            _warningFire=new GameObject[21];
            StartCoroutine(Damage());
            for (int i = 0; i < transform.childCount; i++)
            {
                _warningFire[i] = transform.GetChild(i).gameObject;
            }
            for (int i = 0; i < fireNums; ++i)
            {
                int randomValue = (int)(Random.value * 20);
                tag[i] = randomValue;
                _warningFire[randomValue].SetActive(true);
            }
        }


        private IEnumerator Damage()
        {
            while (true)
            {
                if (_isPlayerInFire)
                {
                    _player.GetComponentInChildren<PlayerHealth>().FireDamage(hitAmount);
                    CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerHurt);
                    yield return new WaitForSeconds(damageTimeInterval);
                }
                yield return null;
            }
        }

        private void OnDisable()
        {
            foreach (var obj in _warningFire)
            {
                obj.transform.GetChild(0).gameObject.SetActive(false);
                obj.SetActive(false);
            }
        }
    }
}
