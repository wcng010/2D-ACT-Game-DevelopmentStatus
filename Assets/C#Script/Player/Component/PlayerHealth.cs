using C_Script.BaseClass;
using C_Script.Player.Core;
using C_Script.Player.Data;
using C_Script.Player.MVC.Model;
using UnityEngine;

namespace C_Script.Player.Component
{
    public sealed class PlayerHealth: CoreComponent
    {
        private PlayerModel PlayerModel => Model as PlayerModel;
        private PlayerData PlayerData => PlayerModel.PlayerData;
        private Rigidbody2D Rb => PlayerModel.PlayerRigidbody2D;
        private Transform PlayerTrans => PlayerModel.PlayerTrans;
        
        private GameObject _hitEffect;
        protected override void Awake()
        {
            base.Awake();
        }
        
        private void Start()
        {
            InitHealth();
        }

        public void InitHealth()
        {

        }

        public void PlayerDamage(float amount,Vector2 forceVector2,ForceDirection forceDir)=>Damage(PlayerData,amount,forceVector2,forceDir);

        private void Damage(AttackObjectDataSo attackObjectDataSo, float amount,
            Vector2 forceVector2,ForceDirection forceDir)
        {
            if (attackObjectDataSo.Defense >= amount)
            {
                attackObjectDataSo.CurrentHealth -= 1;
            }
            else
            {
                attackObjectDataSo.CurrentHealth += (attackObjectDataSo.Defense - amount);
            }
            if (attackObjectDataSo.CurrentHealth <= 0)
            {
                attackObjectDataSo.IsDeath = true;
            }
            switch (forceDir)
            {
                case ForceDirection.None : 
                    break;
                case ForceDirection.Up : Rb.AddForce(forceVector2.normalized * attackObjectDataSo.HitForceUp, ForceMode2D.Impulse);
                    break;
                case ForceDirection.Down : Rb.AddForce(forceVector2.normalized * attackObjectDataSo.HitForceDown, ForceMode2D.Impulse);
                    break;
                case ForceDirection.Forward: Rb.AddForce(forceVector2.normalized * attackObjectDataSo.HitForceForward, ForceMode2D.Impulse);
                    break;
                default: Debug.unityLogger.LogError("LogicError","No ForceDir Setting");
                    break;
            }
            if(_hitEffect) 
                Destroy(_hitEffect);
            _hitEffect = Instantiate(PlayerData.PlayerWounded, transform.parent, true);
            _hitEffect.transform.localPosition = new Vector3(0,PlayerData.WoudedEffectOffSetY,0);
        }
        
        public void FireDamage(float amount)
        {
            if ((PlayerData.CurrentHealth -= amount) < 0)
            {
                PlayerData.IsDeath = true;
            }
            Rb.AddForce(new Vector2(-PlayerTrans.localScale.x,1).normalized*PlayerData.HitForceUp,ForceMode2D.Impulse);
        }

        public void FatalBlow()
        {
            PlayerData.CurrentHealth = 0;
            PlayerData.IsDeath = true;
        }
    }
}