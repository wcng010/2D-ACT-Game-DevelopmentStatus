using System.Collections;
using C_Script.Player.BaseClass;
using C_Script.Player.Component;
using C_Script.Player.StateModel.BaseState;
using UnityEditor;
using UnityEngine;

namespace C_Script.Player.StateModel
{
    public class AttackState1Player :PlayerState
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void Enter()
        {
            base.Enter();
            //While enter this state Invoke this IEnumator and auto colsed
            var vec = Rigidbody2DOwner.velocity;
            Rigidbody2DOwner.velocity = new Vector2(0, vec.y/2);
            Owner.StartCoroutine(AttackRayTestWithoutPower(PlayerData.Attack1Range, AnimationTime,Collider2DOwner.size.y/2));
            if(SkillData.skillBools["WaterWave"])
                OwnerCore.GetCoreComponent<ObjectComponent>().FireObject("WaterFire",TransformOwner);
        }
        public override void LogicExcute()
        {
            //if (!IsAnimationName) return;
            base.LogicExcute();
            //Press Down J Key,change the Count to Combo
            SwitchState();
        }
        public override void Exit()
        {
            base.Exit();
            PressJKeyCount = 0;
            Time.timeScale = 1;
        }
        private void SwitchState()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                PressJKeyCount = 1;
            }
            //Press Down J Key And Current Animation Rate>80%,Enter Combo
            if (PressJKeyCount==1
                &&AnimatorOwner.GetCurrentAnimatorStateInfo(0).normalizedTime>0.8)
            {
                StateMachine.ChangeState(Owner.PlayerStateDic[PlayerStateType.AttackState2Player]);
            }
        }
        public AttackState1Player(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            
        }
    }
}