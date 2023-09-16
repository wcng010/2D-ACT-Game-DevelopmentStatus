using System.Collections.Generic;
using C_Script.BaseClass;
using C_Script.Common.Model.BehaviourModel;
using C_Script.Common.Model.EventCentre;
using C_Script.Eneny.Monster.Magician.Data;
using C_Script.Eneny.Monster.Magician.Model;
using C_Script.Eneny.Monster.Magician.State;
using C_Script.Eneny.Monster.Magician.View;
using C_Script.Model.BehaviourModel;
using UnityEngine;


namespace C_Script.Eneny.Monster.Magician.BaseClass
{
    
    public sealed class MagicianBase :PhysicObject<MagicianBase>
    {
        public MagicianModel MagicianModel => _model ? _model : _model = GetComponentInParent<MagicianModel>();
        
        private MagicianModel _model;
        private MagicianData MagicianData => MagicianModel.EnemyData as MagicianData;

        public MagicianView MagicianView => _view ? _view : _view = Model.View as MagicianView;
        private MagicianView _view;

        public readonly Dictionary<EnemyStateType, State<MagicianBase>> MagicianDic = new();
        private void Awake()
        {
            FindComponent(); InitMagician();
            InitDataSetting();
        }
        private void OnEnable()
        {
            CombatEventCentreManager.Instance.Subscribe(CombatEventType.PlayerDeath,AffterPlayerDeath);
            
            MagicianView.EnemyHurtCrit.AddListener(HurtEvent);
            MagicianView.EnemyDeath.AddListener(DeathEvent);
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
            MagicianView.EnemyHurtCrit.RemoveListener(HurtEvent);
            MagicianView.EnemyDeath.RemoveListener(DeathEvent);
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
        protected override void InitOriginState()
        {
            StateMachine.SetPreviousState(null);
            StateMachine.SetGlobalState(null);
            StateMachine.SetCurrentState(MagicianDic[MagicianData.OriginState]);
            StateMachine.SetOriginalState(MagicianDic[MagicianData.OriginState]);
        }
        protected sealed override void InitStateDictionary()
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
            MagicianDic.Add(EnemyStateType.WinStateEnemy,new WinStateMagician(this,null,null));
        }
        protected override void InitDataSetting()
        {
        }

        public override void HurtEvent()
        {
            /*if(Random.value>MagicianData.DizzinessRate&&StateMachine.CurrentState!=MagicianDic[EnemyStateType.DeathStateEnemy])
                StateMachine.ChangeState(MagicianDic[EnemyStateType.HurtStateEnemy]);*/
            if(StateMachine.CurrentState!=MagicianDic[EnemyStateType.DeathStateEnemy]) 
                StateMachine.ChangeState(MagicianDic[EnemyStateType.HurtStateEnemy]);
        }
        public override void DeathEvent()
        {
            StateMachine.ChangeState(MagicianDic[EnemyStateType.DeathStateEnemy]);
        }


        private void SetOriginPointX()
        {
            MagicianData.OriginPointX = transform.position.x;
        }

        #region Event
        private void AffterPlayerDeath()
        {
            StateMachine.ChangeState(MagicianDic[EnemyStateType.WinStateEnemy]);
        }
        #endregion
        protected override void SwitchState()
        {
        }
    }
}
