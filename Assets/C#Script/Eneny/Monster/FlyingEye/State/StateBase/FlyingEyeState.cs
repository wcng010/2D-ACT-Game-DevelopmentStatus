
using System.Buffers;
using System.Collections.Generic;
using C_Script.BaseClass;
using C_Script.Common.Model.BehaviourModel;
using C_Script.Eneny.Monster.FlyingEye.Base;
using C_Script.Eneny.Monster.FlyingEye.Data;
using UnityEngine;

namespace C_Script.Eneny.Monster.FlyingEye.State.StateBase
{
    public abstract class FlyingEyeState : State<FlyingEyeBase>
    {
        protected FlyingEyeData FlyingEyeData => DataSo as FlyingEyeData;
        protected Dictionary<EnemyStateType, State<FlyingEyeBase>> FlyingEyeDic => Owner.FlyingEyeDic;

        protected Transform TargetTrans => Owner.FlyingEyeModel.TargetTrans;
        
        protected FlyingEyeState(FlyingEyeBase owner, string nameToTrigger, string animationName) : base(owner, nameToTrigger, animationName)
        {
            
        }
    }
}