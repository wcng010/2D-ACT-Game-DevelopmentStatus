using C_Script.BaseClass;
using C_Script.Eneny.EnemyCommon.Model;
using C_Script.Eneny.Monster.FlyingEye.Controller;
using C_Script.Eneny.Monster.FlyingEye.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace C_Script.Eneny.Monster.FlyingEye.Model
{
    public class FlyingEyeModel:EnemyModel
    {
        [field:FoldoutGroup("Custom")] [field: SerializeField] public FlyingEyeView FlyingEyeView { get; private set; }
        [field:FoldoutGroup("Custom")] [field: SerializeField] public FlyingEyeController FlyingEyeController { get; private set; }
        [field:FoldoutGroup("Custom")] [field: SerializeField] public BaseClass.Core FlyingEyeCore { get; private set; }
    }
}
