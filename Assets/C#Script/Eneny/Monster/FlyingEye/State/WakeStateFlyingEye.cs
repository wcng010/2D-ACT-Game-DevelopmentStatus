using System;
using System.Numerics;
using C_Script.Eneny.Monster.FlyingEye.Base;
using C_Script.Eneny.Monster.FlyingEye.State.StateBase;
using Vector2 = UnityEngine.Vector2;

namespace C_Script.Eneny.Monster.FlyingEye.State
{
    public class WakeStateFlyingEye:FlyingEyeState
    {
        public WakeStateFlyingEye(FlyingEyeBase owner, string nameToTrigger, string animationName) : base(owner, nameToTrigger, animationName)
        {
        }
    }
}