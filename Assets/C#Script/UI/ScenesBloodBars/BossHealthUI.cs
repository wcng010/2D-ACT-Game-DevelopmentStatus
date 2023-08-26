using System.Collections;
using C_Script.BaseClass;
using C_Script.Common.Model.EventCentre;
using C_Script.Eneny.EnemyCommon.Model;
using C_Script.Eneny.EnemyCreator;
using C_Script.Eneny.Monster.Magician.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;


namespace C_Script.UI.ScenesBloodBars
{
    public class BossHealthUI : MonoBehaviour
    {
        private float _bloodLength;
        private BossData _bossDataSo;
        private float _localScaleX;
        private float _scaleX;
        private float _heathRate;
        private float _originX = 0;
        private BossFactory _bossFactory ;
        private bool _bossEnter2;
        private 

        void OnEnable()
        {
            _bossDataSo =GetComponentInParent<EnemyModel>().EnemyData as BossData;
            _originX = transform.localPosition.x;
        }
        
        private void Start()
        {
            var transformTemp = transform;
            _localScaleX = transformTemp.localScale.x;
            _scaleX = transformTemp.lossyScale.x;
            _bloodLength = _localScaleX;
            _bossFactory = transform.parent.parent.GetComponent<BossFactory>();
        }

        private void Update()
        {
            var transform1 = transform;
            _heathRate = Mathf.Clamp(_bossDataSo.CurrentHealth / _bossDataSo.MaxHealth, 0, 1);
            if (_heathRate == 0 && _bossDataSo.IsTwoLives)
            {
                if (!_bossDataSo.FirstDeath)
                {
                    BossFirstDeath();
                }
                else if (!_bossEnter2)
                {
                    BossSecondDeath();
                }
            }
            else if (_heathRate == 0&&!_bossEnter2)
            {
                BossSecondDeath();
            }
            transform1.localScale=new Vector3(_localScaleX * _heathRate, transform1.localScale.y,1);
            transform1.localPosition = new Vector3(_originX - (1 - _heathRate)/2 * _bloodLength,
                transform.localPosition.y, 0);
        }
        
        private IEnumerator ChangState()
        {
            _bossFactory.changeTimeline.Play();
            _bossFactory.effect2.SetActive(true);
            CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerStop);
            CombatEventCentreManager.Instance.Publish(CombatEventType.EnemyStop);
            yield return new WaitForSecondsRealtime(_bossFactory.bossChangeTime);
            _bossDataSo.CurrentHealth = _bossDataSo.MaxHealth;
            CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerStart);
            CombatEventCentreManager.Instance.Publish(CombatEventType.EnemyStart);
        }


        private void BossFirstDeath()
        {
            _bossDataSo.FirstDeath = true;
            _bossDataSo.CurrentHealth = _bossDataSo.MaxHealth;
            _bossFactory.StartCoroutine(ChangState());
        }

        private void BossSecondDeath()
        {
            _bossDataSo.IsDeath = true;
            _bossEnter2 = true;
            _bossFactory.DefeatBoss();
            if(SceneManager.GetActiveScene().buildIndex+1>SceneManager.sceneCountInBuildSettings)
                ScenesEventCentreManager.Instance.Publish(ScenesEventType.GameOver);
            enabled = false;
        }
    }
}
