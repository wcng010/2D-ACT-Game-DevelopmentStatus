using C_Script.Common.Model.BehaviourModel;
using C_Script.Eneny.Boss.DemonBoss.BaseClass;
using C_Script.Eneny.Boss.DemonBoss.Data;
using C_Script.Model.BehaviourModel;

namespace C_Script.Eneny.Boss.DemonBoss.State.StateBase
{
    public abstract class DemonBossState : State<DemonBossBase>
    {
        protected DemonBossData DemonBossData => DataSo as DemonBossData;

        protected DemonBossState(DemonBossBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            
        }
    }
}