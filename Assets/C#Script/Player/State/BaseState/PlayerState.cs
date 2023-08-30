using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using C_Script.Common.Model.BehaviourModel;
using C_Script.Common.Model.EventCentre;
using C_Script.Eneny.EnemyCommon.Component;
using C_Script.Eneny.Monster.Magician.Component;
using C_Script.Model.BehaviourModel;
using C_Script.Player.BaseClass;
using C_Script.Player.Data;
using C_Script.Player.MVC.Model;
using UnityEngine;


namespace C_Script.Player.StateModel.BaseState
{
    /// <summary>
    /// In order to compound PlayerState function and data
    /// </summary>
    public abstract class PlayerState:State<PlayerBase>
    {
        // ReSharper disable Unity.PerformanceAnalysis
        [Range(0, 1)] protected static uint PressJKeyCount = 0;
        protected PlayerData PlayerData => DataSo as PlayerData;
        protected SkillData SkillData => Owner.SkillBool;
        protected Dictionary<PlayerStateType, State<PlayerBase>> StateDictionary => Owner.PlayerStateDic;

        protected PlayerModel PlayerModel => Model as PlayerModel;
        
        // ReSharper disable Unity.PerformanceAnalysis
        protected virtual void MoveBehaviour(float maxSpeedX,float accelerationX)
        {
            //Rigidbody2DOwner.velocity = new Vector2(Owner.XAxis*speed ,0);
            if (Owner.XAxis<0)
            {
                TransformOwner.localScale = new Vector3(-1, 1, 1);
            }
            else if(Owner.XAxis>0)
            {
                TransformOwner.localScale = new Vector3(1 ,1, 1);
            }
            float inputX = Owner.XAxis;
                if (Owner.XAxis != 0) {
                    float velocityX = Mathf.Clamp(Rigidbody2DOwner.velocity.x + inputX * accelerationX * Time.fixedDeltaTime,-maxSpeedX,maxSpeedX);
                    Rigidbody2DOwner.velocity = new Vector2(velocityX,Rigidbody2DOwner.velocity.y);
                    //TransformOwner.position += new Vector3(velocityX*Time.fixedDeltaTime, 0, 0);
                }
        }
        
        public virtual void ChangeColliderYSize(CapsuleCollider2D collider2D,float ySize)
        {
            float sizeX = collider2D.size.x;
            collider2D.size = new Vector2(sizeX, ySize);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected virtual IEnumerator AttackRayTestWithPower(float attackRange,float animationTime,string animationName,float height)
        {
            //Current animation is Attack1
            yield return new WaitUntil(() => IsAnimationName);
            //Enter Circulation
            while (true)
            {
                //Ten Rayscast Circulation
                for (int i = 0; i <= 10; i++)
                {
                    RaycastHit2D[] hits = RaysTestAll(new Vector2(TransformOwner.position.x,TransformOwner.position.y+height*(5-i)/5),
                        new Vector2(TransformOwner.localScale.x, 0), attackRange, 1 << LayerMask.NameToLayer("Enemy"));
                    //Hit Enemys
                    if (hits != null&&hits.Length!=0)
                    {
                        if (!CheckCurrentState()) yield break;
                        //咋瓦鲁多
                        Time.timeScale = PlayerData.DecelerateRate60;
                        //Knock out conditions
                        yield return new WaitUntil(() =>
                            AnimatorOwner.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5);
                        CombatEventCentreManager.Instance.Publish(CombatEventType.CameraShake);
                        //Invoke Hurt Enemys Function
                        foreach (var hit in hits)
                        {
                            //Invoke EnemyDamage to add Force And reduce health
                            hit.transform.GetComponentInChildren<EnemyHealth>().EnemyDamageWithPower(
                                CalculateAttack(PlayerData.AttackPower, PlayerData.CriticalRate,
                                    PlayerData.CriticalDamage), new Vector2(TransformOwner.localScale.x, 0));
                        }
                        //Wait AnimationFinshed And Change to Other State
                        yield return new WaitUntil(() => IsAniamtionFinshed);
                        yield break;
                    }
                    if (i == 10) yield return new WaitUntil(() => IsAniamtionFinshed);
                    //Not in this state,End the Coroutine
                    if(!CheckCurrentState())yield break;
                }
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected virtual IEnumerator AttackRayTestWithoutPower(float attackRange,float animationTime,float height)
        { 
            yield return new WaitUntil(() => IsAnimationName);
            while (true)
            {
                for (int i = 0; i <= 10; i++)
                {
                    RaycastHit2D[] hits = RaysTestAll(new Vector2(TransformOwner.position.x,TransformOwner.position.y+height*(5-i)/5),
                        new Vector2(TransformOwner.localScale.x, 0), attackRange, 1 << LayerMask.NameToLayer("Enemy"));
                    if (hits != null&&hits.Length!=0)
                    {
                        if (!CheckCurrentState()) yield break;
                        Time.timeScale = PlayerData.DecelerateRate75;
                        //Invoke EnemyDamage to add Force And reduce health
                        foreach (var hit in hits)
                        {
                            hit.transform.GetComponentInChildren<EnemyHealth>().EnemyDamageWithoutPower(
                                CalculateAttack(PlayerData.AttackPower, PlayerData.CriticalRate,
                                    PlayerData.CriticalDamage));
                        }
                        yield return new WaitUntil(() => IsAniamtionFinshed);
                        yield break;
                    }
                    if (i == 10) yield return new WaitUntil(() => IsAniamtionFinshed);
                    if(!CheckCurrentState()) yield break;
                }
            }
        }

        private static RaycastHit2D[] RaysTestAll(Vector2 originVec2D, Vector2 dirVec2D, float length, LayerMask layerMask)
        {
            return Physics2D.RaycastAll(originVec2D, dirVec2D, length, layerMask);
        }

        protected PlayerState(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        { }
    }
}