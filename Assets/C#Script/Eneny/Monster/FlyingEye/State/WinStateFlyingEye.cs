using C_Script.Eneny.Monster.FlyingEye.Base;
using C_Script.Eneny.Monster.FlyingEye.State.StateBase;

namespace C_Script.Eneny.Monster.FlyingEye.State
{
    public class WinStateFlyingEye:FlyingEyeState
    {
        public WinStateFlyingEye(FlyingEyeBase owner, string nameToTrigger, string animationName) : base(owner, nameToTrigger, animationName)
        {
        }
    }
}