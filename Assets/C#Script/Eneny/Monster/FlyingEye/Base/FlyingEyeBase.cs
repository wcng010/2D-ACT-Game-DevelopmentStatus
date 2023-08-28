using System.Collections.Generic;
using C_Script.BaseClass;
using C_Script.Common.Model.BehaviourModel;
using C_Script.Common.Model.EventCentre;
using C_Script.Eneny.Monster.FlyingEye.Data;
using C_Script.Eneny.Monster.FlyingEye.Model;
using C_Script.Eneny.Monster.FlyingEye.State;
using C_Script.Model.BehaviourModel;

namespace C_Script.Eneny.Monster.FlyingEye.Base
{
    public class FlyingEyeBase : PhysicObject<FlyingEyeBase>
    {
        public readonly Dictionary<EnemyStateType, State<FlyingEyeBase>> FlyingEyeDic = new();
        public FlyingEyeModel FlyingEyeModel => _model? _model : _model = GetComponentInParent<FlyingEyeModel>();
        
        private FlyingEyeModel _model;
        private FlyingEyeData Data => FlyingEyeModel.EnemyData as FlyingEyeData;
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
            InitAnimator(FlyingEyeModel.EnemyAnimator);
            InitRigidbody2D(FlyingEyeModel.EnemyRigidbody2D);
            InitCollider2D(FlyingEyeModel.EnemyCapCollider2D);
            InitCore(FlyingEyeModel.EnemyCore);
            InitTransform(FlyingEyeModel.EnemyTrans);
            InitData(Data);
            InitModel(FlyingEyeModel);
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
            StateMachine.SetCurrentState(FlyingEyeDic[EnemyStateType.SleepStateEnemy]);
            StateMachine.SetOriginalState(FlyingEyeDic[FlyingEyeModel.EnemyData.OriginState]);
        }
        public sealed override void InitStateDictionary()
        {
            StateMachine = new StateMachine<FlyingEyeBase>(this);
            FlyingEyeDic.Add(EnemyStateType.SleepStateEnemy,new SleepStateFlyingEye(this,null,null));
            FlyingEyeDic.Add(EnemyStateType.LightAttackStateEnemy, new LightAttackStateFlyingEye(this, "Attack1", "FlyingEye_attack1"));
            FlyingEyeDic.Add(EnemyStateType.HeavyAttackStateEnemy, new HeavyAttackStateFlyingEye(this, "Attack2", "FlyingEye_attack2"));
            FlyingEyeDic.Add(EnemyStateType.IdleStateEnemy, new IdleStateFlyingEye(this, null, null));
            FlyingEyeDic.Add(EnemyStateType.PursuitStateEnemy, new FlyStateFlyingEye(this, null, null));
            FlyingEyeDic.Add(EnemyStateType.WakeStateEnemy, new WakeStateFlyingEye(this,"Wake","FlyingEye_wake"));
            FlyingEyeDic.Add(EnemyStateType.DeathStateEnemy, new DeathStateFlyingEye(this, "Death", "FlyingEye_death"));
            FlyingEyeDic.Add(EnemyStateType.HurtStateEnemy, new HurtStateFlyingEye(this, "Hit", "FlyingEye_hit"));
            FlyingEyeDic.Add(EnemyStateType.CoolDownStateEnemy,new CoolStateFlyingEye(this,"Cool","FlyingEye_coolDown"));
        }
        public override void InitDataSetting()
        {
        }
        private void SetOriginPointX()
        {
            Data.OriginPointX = transform.position.x;
        }
        #region Event
        private void AffterPlayerDeath()
        {
            Data.IsTargetDeath = true;
        }
        #endregion
        public override void SwitchState()
        {   
            
        }
    }
}
