using System.Collections;
using C_Script.Common.Model.EventCentre;
using C_Script.Player.BaseClass;
using C_Script.Player.StateModel.BaseState;
using UnityEngine;

namespace C_Script.Player.State
{
    public class DeathStatePlayer:PlayerState
    {
        public override void Enter()
        {
            base.Enter();
            Owner.gameObject.layer = LayerMask.NameToLayer("Default");
            Owner.gameObject.tag = "Untagged";
            Rigidbody2DOwner.velocity = Vector2.zero;
            ChangeColliderYSize(Collider2DOwner,PlayerData.DeathSizeY);
            //CombatEventCentreManager.Instance.Publish(EventType.PlayerDeath);
        }
        public override void LogicExcute()
        {
            if (IsAniamtionFinshed)
                Owner.StartCoroutine(DeathIEnu());
        }
        public override void Exit()
        {
            base.Exit();
        }
        IEnumerator DeathIEnu()
        {
            yield return new WaitForSeconds(1f);
            ScenesEventCentreManager.Instance.Publish(ScenesEventType.ReStart);
        }

        public DeathStatePlayer(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
        }
    }
}