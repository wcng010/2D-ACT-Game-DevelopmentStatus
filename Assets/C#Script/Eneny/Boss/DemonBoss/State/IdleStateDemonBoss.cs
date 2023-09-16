using C_Script.BaseClass;
using C_Script.Common.Model.ObjectPool;
using C_Script.Eneny.Boss.DemonBoss.BaseClass;
using C_Script.Eneny.Boss.DemonBoss.Model;
using C_Script.Eneny.Boss.DemonBoss.State.StateBase;
using C_Script.Eneny.Monster.Magician.State.StateBase;
using UnityEngine;

namespace C_Script.Eneny.Boss.DemonBoss.State
{
    public class IdleStateDemonBoss:DemonBossState
    {
        private int _isFirstEnter = 0;
        private Transform EnergyBar=>Owner.Factory.bossEnergyBar.transform;
        private float _timer;
        private float _readyTime;
        private Vector3 _barPosOri;
        private Vector2 _bossPosOri;
        
        public override void Enter()
        {
            if (_isFirstEnter == 0)
            {
                GameObject.Instantiate(DemonBossData.SkullCircle, TransformOwner);
                BigObjectPool.Instance.SetAllActive(ObjectType.Skull);
                _isFirstEnter = 1;
            }
            else
            {
                BigObjectPool.Instance.SetAllActive(ObjectType.Skull);
            }
            EnergyBar.parent.gameObject.SetActive(true);
            _readyTime = DemonBossData.ReadyTime;
            _timer = 0;
            _barPosOri = EnergyBar.localPosition;
            _bossPosOri = DemonBossData.OriginPos;
            TransformOwner.position = new Vector3(_bossPosOri.x, _bossPosOri.y, 0);
        }

        public override void PhysicExcute()
        {
            
        }
        
        public override void LogicExcute()
        {
            _timer += Time.unscaledDeltaTime;
            BarIncrease();
        }

        public override void Exit()
        {
            EnergyBar.parent.gameObject.SetActive(false);
            EnergyBar.localPosition = _barPosOri;
        }
        
        private void BarIncrease()
        {
            var rate = _timer / _readyTime;
            EnergyBar.localScale = new Vector3(4.9f * rate,EnergyBar.localScale.y,1);
            EnergyBar.localPosition = _barPosOri + new Vector3(_timer / _readyTime / 2/ 2 * 4.9f, 0, 0);
            if(rate >0.75f)
                Owner.Factory.effect1.SetActive(true);
            if (rate > 1)
                StateMachine.ChangeState(Owner.DemonBossDic[EnemyStateType.MeleeAttackStateEnemy]);
        }

        public IdleStateDemonBoss(DemonBossBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            
        }
    }
}