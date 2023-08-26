using System;
using System.Collections;
using System.Collections.Generic;
using C_Script.Common.Model.EventCentre;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace C_Script.Eneny.EnemyCreator
{
    public class BossFactory : MonoBehaviour
    {
        [FoldoutGroup("Border")] public GameObject borderLeft;
        [FoldoutGroup("Border")] public GameObject borderRight;
        [FoldoutGroup("Timeline")] public PlayableDirector startTimeline;
        [FoldoutGroup("Timeline")] public PlayableDirector changeTimeline;
        [FoldoutGroup("外部信息")] public GameObject bossObj;
        [FoldoutGroup("外部信息")] public float playerEnterTime;
        [FoldoutGroup("外部信息")] public float playerStopTime;
        [FoldoutGroup("外部信息")] public float bossChangeTime;
        [FoldoutGroup("外部信息")] public Vector2 bossPos;
        [FoldoutGroup("外部信息")] public GameObject bossViewCamera;
        [FoldoutGroup("外部信息")] public GameObject passWay;
        [FoldoutGroup("Bar")] public GameObject bossHealthBar;
        [FoldoutGroup("Bar")] public GameObject bossEnergyBar;
        [FoldoutGroup("AttackEffect")] public GameObject effect1;
        [FoldoutGroup("AttackEffect")] public GameObject effect2;

        private void OnEnable()
        {
            CombatEventCentreManager.Instance.Subscribe(CombatEventType.EnterBossRoom,EnterBossRoom);
        }

        private void OnDisable()
        {
            CombatEventCentreManager.Instance.Unsubscribe(CombatEventType.EnterBossRoom,EnterBossRoom);
        }

        private void Update()
        {

        }

        private void EnterBossRoom()
        {
            bossViewCamera.SetActive(true);
            bossObj.SetActive(true);
            borderLeft.SetActive(true);
            borderRight.SetActive(true);
            bossHealthBar.SetActive(true);
            if (startTimeline)
            {
                startTimeline.Play();
                StartCoroutine(StopPlayer());
            }
        }


        private IEnumerator StopPlayer()
        {
            yield return new WaitForSecondsRealtime(playerEnterTime);
            CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerStop);
            yield return new WaitForSecondsRealtime(playerStopTime);
            startTimeline.Stop();
            CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerStart);
            CombatEventCentreManager.Instance.Publish(CombatEventType.EnemyStart);
        }

        public void DefeatBoss()
        {
            bossObj.SetActive(false);
            borderLeft.SetActive(false);
            borderRight.SetActive(false);
            bossHealthBar.SetActive(false);
            if(effect1)
                effect1.SetActive(false);
            if(effect2)
                effect2.SetActive(false);
            bossViewCamera.SetActive(false);
            if(passWay)
                passWay.SetActive(true);
        }
    }
}
