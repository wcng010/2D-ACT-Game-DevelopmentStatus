using System;
using C_Script.Manager;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.Common.Audio
{
    public class PlayerAudioTrigger : AudioTrigger
    {
        public void Attack1Play() => AudioManager.Instance.PlayerAttack1Play();
        public void Attack2Play () => AudioManager.Instance.PlayerAttack2Play();
        public void JumpPlay () => AudioManager.Instance.PlayerJumpPlay();
        public void LandPlay () => AudioManager.Instance.PlayerLandPlay();
        public void DeathPlay() => AudioManager.Instance.PlayerDeathPlay();
        public void DashPlay() => AudioManager.Instance.PlayerDashPlay();
        public void HurtPlay() => AudioManager.Instance.PlayerHurtPlay();
        public void TurnAroundPlay() => AudioManager.Instance.PlayerTurnAroundPlay();
        public void RunPlay() => AudioManager.Instance.PlayerRunPlay();
        public void RunStop() => AudioManager.Instance.PlayerRunStop();
    }
}
