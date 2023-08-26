using C_Script.BaseClass;
using C_Script.Eneny.EnemyCommon.Model;
using UnityEngine;

namespace C_Script.Eneny.EnemyCommon.Component
{
    public class EnemyHealth : CoreComponent
    {
        //private EnemyData _enemyData;
        private EnemyModel EnemyModel => Model as EnemyModel;
        private Rigidbody2D Rb =>  EnemyModel.EnemyRigidbody2D;
        private EnemyData EnemyData => EnemyModel.EnemyData;
        private GameObject HitEffect1 => EnemyData.HitEffect1;
        private GameObject HitEffect2 => EnemyData.HitEffect2;
        private GameObject _effect1;
        private GameObject _effect2;
        protected virtual void Start() => InitHealth();
        protected virtual void InitHealth() { 
            EnemyData.CurrentHealth = EnemyData.MaxHealth;
            EnemyData.IsDeath = false;
            EnemyData.IsHurt = false;
        }
        public void EnemyDamageWithoutPower(float amount)
        {
            CommonDamage(amount);
            if(_effect1) Destroy(_effect1);
            _effect1 = Instantiate(HitEffect2, transform.parent.parent, true); 
            _effect1.transform.localPosition = new Vector3(0,EnemyData.HitEffectOffSetY,0);
        }
        public void EnemyDamageWithPower(float amount, Vector2 forceVector2)
        {
            EnemyData.IsHurt = true;
            CommonDamage(amount);
            Rb.AddForce(new Vector2(forceVector2.x,2.5f).normalized*EnemyData.HitForceForward,ForceMode2D.Impulse);
            if(_effect2) Destroy(_effect2);
            _effect2 = Instantiate(HitEffect1, transform.parent.parent, true);
            _effect2.transform.localPosition = new Vector3(0,EnemyData.HitEffectOffSetY,0);
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
                EnemyData.IsDeath = true;
            }
        }
    }
}