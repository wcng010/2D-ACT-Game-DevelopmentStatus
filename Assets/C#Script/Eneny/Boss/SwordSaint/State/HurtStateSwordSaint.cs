using UnityEngine;

namespace C_Script.Eneny.Boss.SwordSaint.State
{
    public class HurtStateSwordSaint : SwordSaintState
    {
        public HurtStateSwordSaint(SwordSaintBase owner, string nameToTrigger, string animationName) : base(owner, nameToTrigger, animationName)
        {
        }

        public override void Exit()
        {
            base.Exit();
            SwordSaintData.IsHurt = false;
        }
    }
}