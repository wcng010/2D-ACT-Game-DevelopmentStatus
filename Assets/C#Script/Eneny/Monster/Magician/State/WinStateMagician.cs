using C_Script.Eneny.Monster.Magician.BaseClass;
using C_Script.Eneny.Monster.Magician.State.StateBase;

namespace C_Script.Eneny.Monster.Magician.State
{
    public class WinStateMagician: MagicianState
    {
        public WinStateMagician(MagicianBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            
        }
    }
}