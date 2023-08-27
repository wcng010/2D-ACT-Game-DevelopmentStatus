using System.Collections.Generic;
using C_Script.BaseClass;
using C_Script.Common.Model.BehaviourModel;
using C_Script.Eneny.Monster.Magician.BaseClass;
using C_Script.Eneny.Monster.Magician.Data;
using UnityEngine;

namespace C_Script.Eneny.Monster.Magician.State.StateBase
{
    public abstract class MagicianState:State<MagicianBase>
    {
        protected MagicianData MagicianData=>DataSo as MagicianData;
        protected Dictionary<EnemyStateType, State<MagicianBase>> MagicianDic => Owner.MagicianDic;
        protected Transform TargetTrans => Owner.MagicianModel.TargetTrans;

        protected Vector2 OriginPoint;
        
        // ReSharper disable Unity.PerformanceAnalysis

        protected void SetColliderOffset(CapsuleCollider2D collider2D,Vector2 offset)
        {
            collider2D.offset = offset;
        }
        protected void SetColliderSize(BoxCollider2D boxCollider2D,Vector2 size)
        {
            boxCollider2D.size= size;
        }

        protected MagicianState(MagicianBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
           OriginPoint = owner.MagicianModel.EnemyTrans.position;
        }
    }
}