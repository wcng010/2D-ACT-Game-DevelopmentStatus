using C_Script.BaseClass;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.Player.Data
{
    [CreateAssetMenu(fileName = "Data",menuName = "Data/PlayerData")]
    public class PlayerData :AttackObjectDataSo
    {
        [FoldoutGroup("属性")] public  ushort rollForce = 600;
        [FoldoutGroup("属性")] public  ushort slideForce = 800;

        [field:FoldoutGroup("BoxCollider")] [field:SerializeField] public float CrouchSizeY { get; private set; }

        [field:FoldoutGroup("BoxCollider")] [field:SerializeField] public float IdleSizeY { get; private set; } 

        [field:FoldoutGroup("BoxCollider")] [field:SerializeField] public float DeathSizeY { get; private set; }

        [field:FoldoutGroup("BoxCollider")] [field:SerializeField] public float SlideSizeY { get; private set; }

        [field: FoldoutGroup("属性")] [field: SerializeField] public float Attack1Range { get; set; } 
        
        [field: FoldoutGroup("属性")] [field: SerializeField] public float Attack2Range { get; set; }
        [field: FoldoutGroup("属性")] [field: SerializeField] public float CrouchAttackRange { get; set; }
        [field: FoldoutGroup("RigidBody2D")] [field: SerializeField] public float GravityScale { get; set; }

        public readonly float DecelerateRate60 = 0.6f;
        
        public readonly float DecelerateRate75 = 0.75f;
        
        [field: FoldoutGroup("属性")] [field: SerializeField] [field: Range(0, 2)] public float DecelerateRateRate{ get; private set; }
        
        [field: FoldoutGroup("属性")] [field:SerializeField] [field:Range(1,2)] public float AccelerateRate { get; private set; }
        
        [field: FoldoutGroup("MoveInfo")] [field: SerializeField] public float MaxSpeedX { get; private set; }
        [field: FoldoutGroup("MoveInfo")] [field: SerializeField] public float AccelerationX{ get; private set; }//27.7
        
        [field: FoldoutGroup("MoveInfo")] [field: SerializeField] public float DeAcceleration{ get; private set; }
        [field: FoldoutGroup("MoveInfo")]  public bool SpeedUpbotton { get; set; }

        [field: FoldoutGroup("DashInfo")] [field: SerializeField] public bool CanDash { get; private set; }
        [field: FoldoutGroup("DashInfo")] [field:SerializeField] public float DashLength { get; private set; }
        [field: FoldoutGroup("DashInfo")] [field: SerializeField] public float DashSpeed { get; private set; }
        [field: FoldoutGroup("JumpInfo")] [field: SerializeField] public float YSpeedUpTime{ get; private set; }
        [field: FoldoutGroup("JumpInfo")] [field: SerializeField] public float AccelerationY { get; private set; }
        [field: FoldoutGroup("JumpInfo")] [field: SerializeField] public float MaxSpeedY { get; private set; }
        [field: FoldoutGroup("CrouchInfo")] [field: SerializeField] public float MaxSpeedCrouchX { get; private set; }
        [field: FoldoutGroup("CrouchInfo")] [field: SerializeField] public float AccelerationCrouchX { get; private set; }
        
        [field: FoldoutGroup("LinkTimeInfo")] [field: SerializeField] public float LinkTimeScale { get; private set; }
        [field: FoldoutGroup("LinkTimeInfo")] [field: SerializeField] public bool IsLinkTime { get; set; }
        [field: FoldoutGroup("TurnAroundInfo")] [field: SerializeField] public float DecelerationSpeed { get; private set; }
        [field: FoldoutGroup("TurnAroundInfo")] [field: SerializeField] public float TurnAroundSpeed { get; private set; }
        [field: FoldoutGroup("TurnAroundInfo")] [field: SerializeField] public float Deceleration { get; private set; }
        [field: FoldoutGroup("Effect")] [field: SerializeField] public GameObject PlayerWounded{ get; private set; }
        [field: FoldoutGroup("Effect")] [field: SerializeField] public float WoudedEffectOffSetY{ get; private set; }

        [field: FoldoutGroup("CombatMessage")] [field: SerializeField] [field:Range(0,1)] public float StunRate { get; private set; }
        
        [field: FoldoutGroup("Prefabs")] [field: SerializeField] public GameObject FallAsh { get; private set; }
        [field: FoldoutGroup("Prefabs")] [field: SerializeField] public GameObject JumpAsh { get; private set; }
        [field: FoldoutGroup("Prefabs")] [field: SerializeField] public GameObject TurnAroundAsh { get; private set; }
        [field: FoldoutGroup("Prefabs")] [field: SerializeField] public GameObject DashAsh { get; private set; }
        public bool WalkAshEffectTrriger { get; set; }

    }
}