using C_Script.Model.BehaviourModel;
using UnityEngine;

namespace C_Script.BaseClass
{
    public abstract class PhysicObject<T> :MonoBehaviour where T :PhysicObject<T>
    {
        public Transform Transform { get; protected set; }
        public Rigidbody2D Rigidbody2D { get;protected set; }
        public Animator Animator { get; protected set; }
        public SpriteRenderer SpriteRenderer { get; protected set; }
        public CapsuleCollider2D Collider2D { get; protected set; }
        public StateMachine<T> StateMachine{ get; protected set; }
        public AttackObjectDataSo DataSo { get; protected set; }
        public Model Model { get; private set; }

        public Core Core{ get; private set; }
        //初始化起始状态
        public abstract void InitOriginState();
        //状态字典
        public abstract void InitStateDictionary();
        //选择状态
        public abstract void SwitchState();
        //初始化数据内容
        public abstract void InitDataSetting();
        //物理步
        public virtual void PhysicBehaviour()
        {
            StateMachine.StatePhysicBehaviour();
        }
        //逻辑步
        public virtual void LogicBehaviour()
        {
            StateMachine.StateLogicBehaviour();
        }

        #region InitFunction

        protected void InitTransform(Transform transform) => Transform = transform;
        protected void InitAnimator(Animator animator)=>Animator = animator;
        protected void InitRigidbody2D(Rigidbody2D rigidbody2D) => Rigidbody2D = rigidbody2D;
        protected void InitCollider2D(CapsuleCollider2D collider2D) => Collider2D = collider2D;
        protected void InitSpriteRenderer(SpriteRenderer spriteRenderer) => SpriteRenderer = spriteRenderer;
        protected void InitData(AttackObjectDataSo dataSo) => DataSo = dataSo;
        protected void InitCore(Core core) => Core = core;
        protected void InitModel(Model model) => Model = model;

        #endregion


    }
}