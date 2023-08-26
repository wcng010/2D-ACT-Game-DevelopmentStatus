using System.Collections.Generic;
using C_Script.BaseClass;
using C_Script.Common.Model.BehaviourModel;
using C_Script.Common.Model.EventCentre;
using C_Script.Eneny.Monster.Magician.Core;
using C_Script.Eneny.Monster.Magician.Data;
using C_Script.Eneny.Monster.Magician.Model;
using C_Script.Eneny.Monster.Magician.State;
using C_Script.Eneny.Monster.Magician.State.StateBase;
using C_Script.Model.BehaviourModel;
using C_Script.Player.StateModel.BaseState;
using UnityEngine;
using UnityEngine.Serialization;


namespace C_Script.Eneny.Monster.Magician.BaseClass
{
    
    public sealed class MagicianBase :PhysicObject<MagicianBase>
    {
        public MagicianModel MagicianModel => _model ? _model : _model = GetComponentInParent<MagicianModel>();
        
        private MagicianModel _model;
        private MagicianData MagicianData => MagicianModel.EnemyData as MagicianData;

        public readonly Dictionary<EnemyStateType, State<MagicianBase>> MagicianDic = new();
        private void Awake()
        {
            FindComponent(); InitMagician();
            InitDataSetting();
        }
        private void OnEnable()
        {
            CombatEventCentreManager.Instance.Subscribe(CombatEventType.PlayerDeath,AffterPlayerDeath);
        }
        private void Start() {}
        private void FixedUpdate()
        {
            PhysicBehaviour();
        }
        private void Update()
        {
            SwitchState(); LogicBehaviour();
        }
        private void OnDisable()
        {
            CombatEventCentreManager.Instance.Unsubscribe(CombatEventType.PlayerDeath,AffterPlayerDeath);
        }
        private void FindComponent()
        {
            InitAnimator(MagicianModel.EnemyAnimator);
            InitRigidbody2D(MagicianModel.EnemyRigidbody2D);
            InitCollider2D(MagicianModel.EnemyCapCollider2D);
            InitCore(MagicianModel.EnemyCore);
            InitTransform(MagicianModel.EnemyTrans);
            InitData(MagicianData);
            InitModel(MagicianModel);
        }
        private void InitMagician()
        {
            SetOriginPointX();
            InitStateDictionary();
            InitOriginState();
        }
        public override void InitOriginState()
        {
            StateMachine.SetPreviousState(null);
            StateMachine.SetGlobalState(null);
            StateMachine.SetCurrentState(MagicianDic[MagicianData.OriginState]);
            StateMachine.SetOriginalState(MagicianDic[MagicianData.OriginState]);
        }
        public sealed override void InitStateDictionary()
        {
            StateMachine = new StateMachine<MagicianBase>(this);
            MagicianDic.Add(EnemyStateType.IdleStateEnemy,new IdleStateMagician(this,null,null));
            MagicianDic.Add(EnemyStateType.PatrolStateEnemy,new PatrolStateMagician(this,"Move","Magician_Move"));
            MagicianDic.Add(EnemyStateType.PursuitStateEnemy,new PursuitStateMagician(this,"Move","Magician_Move"));
            MagicianDic.Add(EnemyStateType.MeleeAttackStateEnemy,new MeleeAttackStateMagician(this,"MeleeAttack","Magician_MeleeAttack"));
            MagicianDic.Add(EnemyStateType.RemoteAttackStateEnemy,new RemoteAttackStateMagician(this,"RemoteAttack","Magician_RemoteAttack"));
            MagicianDic.Add(EnemyStateType.CoolDownStateEnemy,new CoolStateMagician(this,"Cool","Magician_CoolDown"));
            MagicianDic.Add(EnemyStateType.HurtStateEnemy,new HurtStateMagician(this,"Hurt","Magician_Hurt"));
            MagicianDic.Add(EnemyStateType.DeathStateEnemy,new DeathStateMagician(this,"Death","Magician_Death"));
        }
        public override void InitDataSetting()
        {
        }
        private void SetOriginPointX()
        {
            MagicianData.OriginPointX = transform.position.x;
        }

        #region Event
        private void AffterPlayerDeath()
        {
            MagicianData.IsTargetDeath = true;
        }
        #endregion
        public override void SwitchState()
        {
        }
    }
}
