using System.Collections.Generic;
using C_Script.BaseClass;
using C_Script.Common.Model.BehaviourModel;
using C_Script.Common.Model.EventCentre;
using C_Script.Eneny.Boss.SwordSaint.State;
using C_Script.Eneny.EnemyCreator;
using C_Script.Model.BehaviourModel;
using UnityEngine;

namespace C_Script.Eneny.Boss.SwordSaint
{
    public class SwordSaintBase : PhysicObject<SwordSaintBase>
    {
        public SwordSaintModel SwordSaintModel => _model? _model : _model = GetComponentInParent<SwordSaintModel>();
        
        private SwordSaintModel _model;

        public SwordSaintView SwordSaintView => _view ? _view : _view = SwordSaintModel.View as SwordSaintView;
        
        private SwordSaintView _view;
        
        private bool _isPlayerDeath;
        public BossFactory Factory => _factory? _factory: _factory = transform.GetComponentInParent<BossFactory>();
        
        private BossFactory _factory;
        private SwordSaintData SwordSaintData =>SwordSaintModel.EnemyData as SwordSaintData;
        
        public readonly Dictionary<EnemyStateType, State<SwordSaintBase>> SwordSaintStateDic = new();

        private void Awake()
        {
            FindComponent();
            InitDemonBoss();
            InitDataSetting();
        }

        private void OnEnable()
        {
            CombatEventCentreManager.Instance.Subscribe(CombatEventType.EnemyStart,DemonBossStart);
            CombatEventCentreManager.Instance.Subscribe(CombatEventType.EnemyStop,DemonBossStop);
            CombatEventCentreManager.Instance.Subscribe(CombatEventType.PlayerDeath,AfterPlayerDeath);
            SwordSaintView.EnemyHurt.AddListener(HurtEvent);
            SwordSaintView.EnemyDeath.AddListener(DeathEvent);
        }
        
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
            CombatEventCentreManager.Instance.Unsubscribe(CombatEventType.EnemyStart,DemonBossStart);
            CombatEventCentreManager.Instance.Unsubscribe(CombatEventType.EnemyStop,DemonBossStop);
            CombatEventCentreManager.Instance.Unsubscribe(CombatEventType.PlayerDeath,AfterPlayerDeath);
            SwordSaintView.EnemyHurt.RemoveListener(HurtEvent);
            SwordSaintView.EnemyDeath.RemoveListener(DeathEvent);
        }
        private void FindComponent()
        {
            InitAnimator(SwordSaintModel.EnemyAnimator);
            InitRigidbody2D(SwordSaintModel.EnemyRigidbody2D);
            InitCollider2D(SwordSaintModel.EnemyCapCollider2D);
            InitCore(SwordSaintModel.EnemyCore);
            InitTransform(SwordSaintModel.EnemyTrans);
            InitData(SwordSaintData);
            InitModel(SwordSaintModel);
        }
        private void InitDemonBoss()
        {
            InitStateDictionary();
            InitOriginState();
        }
        protected override void InitOriginState()
        {
            StateMachine.SetPreviousState(null);
            StateMachine.SetGlobalState(null);
            StateMachine.SetOriginalState(SwordSaintStateDic[SwordSaintData.OriginState]);
            StateMachine.SetCurrentState(SwordSaintStateDic[SwordSaintData.OriginState]);
        }
        protected override void InitStateDictionary()
        {
            StateMachine = new StateMachine<SwordSaintBase>(this);
            SwordSaintStateDic.Add(EnemyStateType.IdleStateEnemy,new IdleStateSwordSaint(this,null,null));
            SwordSaintStateDic.Add(EnemyStateType.PursuitStateEnemy,new PursuitStateSwordSaint(this,"Run","SwordSaint_run"));
            SwordSaintStateDic.Add(EnemyStateType.HurtStateEnemy,new HurtStateSwordSaint(this,"Hit","SwordSaint_hit"));
            SwordSaintStateDic.Add(EnemyStateType.MeleeAttack1StateEnemy,new Attack1StateSwordSaint(this,"Attack1","SwordSaint_attack1"));
            SwordSaintStateDic.Add(EnemyStateType.MeleeAttack2StateEnemy,new Attack2StateSwordSaint(this,"Attack2","SwordSaint_attack2"));
            SwordSaintStateDic.Add(EnemyStateType.MeleeAttack3StateEnemy,new Attack3StateSwordSaint(this,"Attack3","SwordSaint_attack3"));
            SwordSaintStateDic.Add(EnemyStateType.DeathStateEnemy,new DeathStateSwordSaint(this,"Death","SwordSaint_death"));
            SwordSaintStateDic.Add(EnemyStateType.DodgeStateEnemy,new DodgeStateSwordSaint(this,"Dodge","SwordSaint_dodge"));
            SwordSaintStateDic.Add(EnemyStateType.WaitStateEnemy, new WaitStateSwordSaint(this,"null","null"));
            SwordSaintStateDic.Add(EnemyStateType.ComboAttackStateEnemy,
                new ComboStateSwordSaint(this,"Combo","SwordSaint_ready","SwordSaint_combo1","SwordSaint_combo2","SwordSaint_combo3"));
            SwordSaintStateDic.Add(EnemyStateType.ReadyStateEnemy,new ReadyStateSwordSaint(this,null,null));
            SwordSaintStateDic.Add(EnemyStateType.WinStateEnemy,new WinStateSwordSaint(this,null,null));
        }

        protected override void SwitchState()
        { }

        protected override void InitDataSetting()
        { }

        private void AfterPlayerDeath()
        {
            StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.WinStateEnemy]);
        }

        public override void HurtEvent()
        {
            if(Random.value>SwordSaintData.DizzinessRate&&!SwordSaintData.SuperArmor)
                StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.HurtStateEnemy]);
        }

        public override void DeathEvent()=>
            StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.HurtStateEnemy]);
        private void DemonBossStop()
        {
            if(StateMachine.CurrentState!=SwordSaintStateDic[EnemyStateType.WaitStateEnemy])
                StateMachine.ChangeState(SwordSaintStateDic[EnemyStateType.WaitStateEnemy]);
        }
        private void DemonBossStart()
        { 
            if(StateMachine.CurrentState==SwordSaintStateDic[EnemyStateType.WaitStateEnemy])
                StateMachine.RevertOrinalState();
        }
    }
}
