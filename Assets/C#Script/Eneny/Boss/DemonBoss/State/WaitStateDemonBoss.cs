using C_Script.Eneny.Boss.DemonBoss.BaseClass;
using C_Script.Eneny.Boss.DemonBoss.State.StateBase;

namespace C_Script.Eneny.Boss.DemonBoss.State
{
    public class WaitStateDemonBoss:DemonBossState
    {
        public override void Enter()
        {
        }
        
        public override void PhysicExcute()
        {
        }
        
        public override void LogicExcute()
        {
            
        }

        public override void Exit()
        {
            
        }

        public WaitStateDemonBoss(DemonBossBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
        }
    }
}