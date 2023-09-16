using System.Collections;
using System.Collections.Generic;
using C_Script.BaseClass;
using C_Script.Common.Model.BehaviourModel;
using C_Script.Common.Model.EventCentre;
using C_Script.Model.BehaviourModel;
using C_Script.Player.Component;
using C_Script.Player.Data;
using C_Script.Player.MVC.Model;
using C_Script.Player.State;
using C_Script.Player.StateModel;
using C_Script.Player.StateModel.SupperState;
using UnityEngine;

namespace C_Script.Player.Base
{
    public enum PlayerStateType
    {
        OnGroundStatePlayer,
        LowSpeedStatePlayer,
        HighSpeedStatePlayer,
        AttackState1Player,
        AttackState2Player,
        HurtStatePlayer,
        JumpStatePlayer,
        RollStatePlayer,
        CrouchStatePlayer,
        CrouchMoveStatePlayer,
        DashStatePlayer,
        SlideStatePlayer,
        CrouchAttackStatePlayer,
        DeathStatePlayer,
        OnAirStatePlayer,
        HangingStatePlayer,
        BoardStatePlayer,
        LinkTimeStatePlayer,
        TurnAroundStatePlayer,
        StopStatePlayer
    }
    public class PlayerBase :PhysicObject<PlayerBase>
    {
        public PlayerModel PlayerModel => GetComponentInParent<PlayerModel>();
        [Header("角色状态字典")] public readonly Dictionary<PlayerStateType, State<PlayerBase>> PlayerStateDic=new Dictionary<PlayerStateType, State<PlayerBase>>();
        public PlayerData PlayerData => PlayerModel.PlayerData;
        public SkillData SkillBool => PlayerModel.SkillData;
        public RaycastHit2D HeadFrontHit2D { get; private set; }
        public RaycastHit2D BodyFrontHit2D { get; private set; }
        public bool BodyFront { get; private set; }
        public bool FootFront{ get; private set; }
        public bool HeadFront { get; private set; }
        public bool IsGroundThreeRays{ get; private set; }
        public bool IsGroundOneRay { get; private set; }
        
        private void Awake()
        {
            FindComponent();
            InitStateDictionary();
            InitOriginState();
            InitDataSetting();
        }

        private void Start()
        {
            StartCoroutine(CheckCoroutine());
        }

        private void OnEnable()
        {
            PlayerSubScribe();
        }
        
        private void FixedUpdate()
        {
            PhysicBehaviour();
        }


        private void Update()
        {
            SwitchState();
            LogicBehaviour();
        }


        private void OnDisable()
        {
           PlayerRemoveScribe();
        }

        protected virtual void FindComponent()
        {
            InitAnimator(PlayerModel.PlayerAnimator);
            InitRigidbody2D(PlayerModel.PlayerRigidbody2D);
            InitCollider2D(PlayerModel.PlayerCapCollider2D);
            InitCore(PlayerModel.PlayerCore);
            InitSpriteRenderer(PlayerModel.PlayerSprite);
            InitTransform(PlayerModel.PlayerTrans);
            InitData(PlayerData);
            InitModel(PlayerModel);
        }
        public void PlayerSubScribe()
        {
            CombatEventCentreManager.Instance.Subscribe(CombatEventType.PlayerHurt,HurtEvent);
            CombatEventCentreManager.Instance.Subscribe(CombatEventType.PlayerStop,PlayerStopEvent);
            CombatEventCentreManager.Instance.Subscribe(CombatEventType.PlayerStart,PlayerStartEvent);
            CombatEventCentreManager.Instance.Subscribe(CombatEventType.PlayerDeath,DeathEvent);
        }
        public void PlayerRemoveScribe()
        {
            CombatEventCentreManager.Instance.Unsubscribe(CombatEventType.PlayerHurt,HurtEvent);
            CombatEventCentreManager.Instance.Unsubscribe(CombatEventType.PlayerStop,PlayerStopEvent);
            CombatEventCentreManager.Instance.Unsubscribe(CombatEventType.PlayerStart,PlayerStartEvent);
            CombatEventCentreManager.Instance.Unsubscribe(CombatEventType.PlayerDeath,DeathEvent);
        }
        private void PlayerStopEvent()
        {
            StartCoroutine(PlayerStop());
        }
        private IEnumerator PlayerStop()
        {
            yield return new WaitUntil(() =>
                StateMachine.CurrentState == PlayerStateDic[PlayerStateType.OnGroundStatePlayer]);
            StateMachine.ChangeState(PlayerStateDic[PlayerStateType.StopStatePlayer]);
        }
        private void PlayerStartEvent()
        {
            StateMachine.RevertOrinalState();
        }
        public override void HurtEvent()
        {
            StateMachine.ChangeState(PlayerStateDic[PlayerStateType.HurtStatePlayer]);
        }

        public override void DeathEvent()
        {
            StateMachine.ChangeState(PlayerStateDic[PlayerStateType.DeathStatePlayer]);
        }
        protected override void InitOriginState()
        {
            StateMachine.SetPreviousState(null);
            StateMachine.SetGlobalState(PlayerStateDic[PlayerStateType.LinkTimeStatePlayer]);
            StateMachine.SetCurrentState(PlayerStateDic[PlayerStateType.OnGroundStatePlayer]);
            StateMachine.SetOriginalState(PlayerStateDic[PlayerStateType.OnGroundStatePlayer]);
            StateMachine.ChangeState(PlayerStateDic[PlayerStateType.OnGroundStatePlayer]);
        }
        protected override void InitDataSetting()
        {
            PlayerData.WalkAshEffectTrriger = false;
            PlayerData.CurrentHealth = PlayerData.MaxHealth;
            PlayerData.IsLinkTime = false;
        }
        
        protected sealed override void InitStateDictionary()
        {
            StateMachine = new StateMachine<PlayerBase>(this);
            PlayerStateDic.Add(PlayerStateType.HighSpeedStatePlayer,new HighSpeedStatePlayer(this,"Move","player_Move"));
            PlayerStateDic.Add(PlayerStateType.LowSpeedStatePlayer,new LowerSpeedStatePlayer(this,"Move","player_Move"));
            PlayerStateDic.Add(PlayerStateType.AttackState1Player,new AttackState1Player(this,"Attack1","player_Attack1"));
            PlayerStateDic.Add(PlayerStateType.AttackState2Player,new AttackState2Player(this,"Attack2","player_Attack2"));
            PlayerStateDic.Add(PlayerStateType.HurtStatePlayer, new HurtStatePlayer(this,"Hurt","player_Hurt"));
            PlayerStateDic.Add(PlayerStateType.JumpStatePlayer,new JumpStatePlayer(this,"Jump","player_Jump"));
            PlayerStateDic.Add(PlayerStateType.RollStatePlayer,new RollStatePlayer(this,"Roll","player_Roll"));
            PlayerStateDic.Add(PlayerStateType.CrouchStatePlayer, new CrouchStatePlayer(this,"Crouch","player_Crouch"));
            PlayerStateDic.Add(PlayerStateType.CrouchMoveStatePlayer, new CrouchMoveStatePlayer(this,"CrouchMove","player_CrouchMove"));
            PlayerStateDic.Add(PlayerStateType.DashStatePlayer,new DashStatePlayer(this,"Dash","player_Dash"));
            PlayerStateDic.Add(PlayerStateType.SlideStatePlayer,new SlideStatePlayer(this,"Slide","player_Slide"));
            PlayerStateDic.Add(PlayerStateType.CrouchAttackStatePlayer,new CrouchAttackStatePlayer(this,"CrouchAttack","player_CrouchAttack"));
            PlayerStateDic.Add(PlayerStateType.DeathStatePlayer,new DeathStatePlayer(this,"Death","player_Death"));
            PlayerStateDic.Add(PlayerStateType.OnAirStatePlayer,new OnAirStatePlayer(this,"OnAir","player_OnAir"));
            PlayerStateDic.Add(PlayerStateType.HangingStatePlayer,new HangingStatePlayer(this,"Hang","player_Hang"));
            PlayerStateDic.Add(PlayerStateType.BoardStatePlayer,new BoardStatePlayer(this,"Board","player_Board"));
            PlayerStateDic.Add(PlayerStateType.OnGroundStatePlayer, new OnGroundStatePlayer(this,null,null));
            PlayerStateDic.Add(PlayerStateType.LinkTimeStatePlayer,new LinkTimeState(this,null,null));
            PlayerStateDic.Add(PlayerStateType.TurnAroundStatePlayer,new TurnAroundStatePlayer(this,"TurnAround","player_TurnAround"));
            PlayerStateDic.Add(PlayerStateType.StopStatePlayer,new StopStatePlayer(this,null,null));
        }
        // ReSharper disable Unity.PerformanceAnalysis
        IEnumerator CheckCoroutine()
        {
            while (true)
            {
                IsGroundOneRay = Core.GetCoreComponent<CollisionComponent>().RayGroundCheck;
                BodyFront = Core.GetCoreComponent<CollisionComponent>().BodyFront;
                HeadFront = Core.GetCoreComponent<CollisionComponent>().HeadFront;
                FootFront = Core.GetCoreComponent<CollisionComponent>().FootFront;
                IsGroundThreeRays = Core.GetCoreComponent<CollisionComponent>().ThreeRaysGroundCheck;
                HeadFrontHit2D = Core.GetCoreComponent<CollisionComponent>().HeadFrontHit;
                BodyFrontHit2D = Core.GetCoreComponent<CollisionComponent>().BodyFrontHit;
                yield return new WaitForSeconds(0.05f);
            }
        }
        // ReSharper disable Unity.PerformanceAnalysis
        /// <summary>
        /// Use StateMachine To SwitchState
        /// </summary>
        protected override void SwitchState()
        {
        }
    }
}
