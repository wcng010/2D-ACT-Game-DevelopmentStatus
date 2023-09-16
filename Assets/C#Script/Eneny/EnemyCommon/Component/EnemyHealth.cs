using C_Script.BaseClass;
using C_Script.Common.Model.EventCentre;
using C_Script.Common.Model.ObjectPool;
using C_Script.Eneny.EnemyCommon.Model;
using C_Script.Eneny.EnemyCommon.View;
using C_Script.Manager;
using UnityEngine;

namespace C_Script.Eneny.EnemyCommon.Component
{
    public class EnemyHealth : CoreComponent
    {
        //private EnemyData _enemyData;
        private EnemyModel EnemyModel => Model as EnemyModel;
        private Transform TargetTrans => EnemyModel.TargetTrans;
        private EnemyView EnemyView => View as EnemyView;
        private Rigidbody2D Rb =>  EnemyModel.EnemyRigidbody2D;
        private EnemyData EnemyData => EnemyModel.EnemyData;
        private GameObject HitEffect1 => EnemyData.HitEffect1;
        private GameObject HitEffect2 => EnemyData.HitEffect2;
        private GameObject HitEffect3 => EnemyData.HitEffect3;
        protected override void Awake()
        {
            base.Awake();
            BigObjectPool.Instance.PushEmptyPool(ObjectType.HitEffect1,HitEffect1);
            BigObjectPool.Instance.PushEmptyPool(ObjectType.HitEffect2,HitEffect2);
            BigObjectPool.Instance.PushEmptyPool(ObjectType.HitEffect3,HitEffect3);
        }

        protected virtual void Start() => InitHealth();
        protected virtual void InitHealth() { 
            EnemyData.CurrentHealth = EnemyData.MaxHealth;
        }
        public void EnemyDamageWithoutPower(float amount)
        {
            if (EnemyData.AttackInvalid) return;
            CommonDamage(amount);
            EnemyView.EnemyHurtNoCrit.Invoke();
            BigObjectPool.Instance.SetOneActive(ObjectType.HitEffect1).transform.position = transform.position + transform.lossyScale.x*(Vector3)EnemyData.HitEffectOffSet1;
        }
        public void EnemyDamageWithPower(float amount, Vector2 forceVector2,bool isCritical = false)
        {
            if (EnemyData.AttackInvalid) return;
            CommonDamage(amount);
            if (isCritical)
            {
                CombatEventCentreManager.Instance.Publish(CombatEventType.CriticalStrike);
                AudioManager.Instance.PlayerCriticalAttackPlay();
                BigObjectPool.Instance.SetOneActive(ObjectType.HitEffect3).transform.position = transform.position - transform.lossyScale.x*(Vector3)EnemyData.HitEffectOffSet2;
                EnemyView.EnemyHurtCrit.Invoke();
            }
            else
            {
                BigObjectPool.Instance.SetOneActive(ObjectType.HitEffect2).transform.position = transform.position - transform.lossyScale.x*(Vector3)EnemyData.HitEffectOffSet2;
                EnemyView.EnemyHurtNoCrit.Invoke();
            }
            Rb.AddForce(new Vector2(forceVector2.x,0).normalized*EnemyData.HitForceForward,ForceMode2D.Impulse);
        }
        
        
        private void CommonDamage(float amount)
        {
            if (EnemyData.Defense >= amount)
            {
                EnemyData.CurrentHealth -= 1;
            }
            else
            {
                EnemyData.CurrentHealth += (EnemyData.Defense - amount);
            }
            if (EnemyData.CurrentHealth <= 0)
            {
                EnemyView.EnemyDeath?.Invoke();
            }
        }
    }
}