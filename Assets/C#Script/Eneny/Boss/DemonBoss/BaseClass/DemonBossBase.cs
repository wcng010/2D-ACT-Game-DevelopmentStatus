using System.Collections.Generic;
using C_Script.BaseClass;
using C_Script.Common.Model.BehaviourModel;
using C_Script.Common.Model.EventCentre;
using C_Script.Eneny.Boss.DemonBoss.Data;
using C_Script.Eneny.Boss.DemonBoss.Model;
using C_Script.Eneny.Boss.DemonBoss.State;
using C_Script.Eneny.Boss.DemonBoss.View;
using C_Script.Eneny.EnemyCreator;
using C_Script.Eneny.Monster.Magician.Core;
using C_Script.Eneny.Monster.Magician.State.StateBase;
using C_Script.Model.BehaviourModel;
using UnityEngine;


namespace C_Script.Eneny.Boss.DemonBoss.BaseClass
{
    public class DemonBossBase : PhysicObject<DemonBossBase>
    {
        private DemonBossModel DemonBossModel => _model? _model : _model = GetComponentInParent<DemonBossModel>();
        
        private DemonBossModel _model;

        private DemonBossView DemonBossView => _view ? _view : _view = DemonBossModel.View as DemonBossView;
        
        private DemonBossView _view;

        private bool _isPlayerDeath;
        public BossFactory Factory => _factory? _factory: _factory = transform.GetComponentInParent<BossFactory>();
        
        private BossFactory _factory;
        private DemonBossData DemonBossData =>DemonBossModel.EnemyData as DemonBossData;
        
        public readonly Dictionary<EnemyStateType, State<DemonBossBase>> DemonBossDic = new();

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
            DemonBossView.EnemyHurt.AddListener(HurtEvent);
            DemonBossView.EnemyDeath.AddListener(DeathEvent);
        }

        private void Start()
        {
            
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
            DemonBossView.EnemyHurt.RemoveListener(HurtEvent);
            DemonBossView.EnemyDeath.RemoveListener(DeathEvent);
        }
        
        private void FindComponent()
        {
            InitAnimator(DemonBossModel.EnemyAnimator);
            InitRigidbody2D(DemonBossModel.EnemyRigidbody2D);
            InitCollider2D(DemonBossModel.EnemyCapCollider2D);
            InitCore(DemonBossModel.EnemyCore);
            InitTransform(DemonBossModel.EnemyTrans);
            InitData(DemonBossData);
            InitModel(DemonBossModel);
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
            StateMachine.SetOriginalState(DemonBossDic[EnemyStateType.IdleStateEnemy]);
            StateMachine.SetCurrentState(DemonBossDic[EnemyStateType.WaitStateEnemy]);
        }
        protected override void InitStateDictionary()
        {
            StateMachine = new StateMachine<DemonBossBase>(this);
            DemonBossDic.Add(EnemyStateType.IdleStateEnemy,new IdleStateDemonBoss(this,null,null));
            DemonBossDic.Add(EnemyStateType.MeleeAttackStateEnemy,new AttackStateDemonBoss(this,"Breath","demon_breath"));
            DemonBossDic.Add(EnemyStateType.WaitStateEnemy,new WaitStateDemonBoss(this,null,null));
        }
        protected override void InitDataSetting()
        {
        }

        public override void HurtEvent()
        {
            
        }

        public override void DeathEvent()
        {
           
        }

        private void DemonBossStop()
        {
            if(StateMachine.CurrentState!=DemonBossDic[EnemyStateType.WaitStateEnemy])
                StateMachine.ChangeState(DemonBossDic[EnemyStateType.WaitStateEnemy]);
        }
        private void DemonBossStart()
        { 
            if(StateMachine.CurrentState==DemonBossDic[EnemyStateType.WaitStateEnemy])
                StateMachine.RevertOrinalState();
        }
        protected override void SwitchState()
        {
        
        }
    }
}
