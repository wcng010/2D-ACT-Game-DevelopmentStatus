using C_Script.Model.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.Manager
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] [FoldoutGroup("BGM")] private AudioSource bgm1;
        [SerializeField] [FoldoutGroup("PlayerAudio")] private AudioSource attack1Audio;
        [SerializeField] [FoldoutGroup("PlayerAudio")] private AudioSource attack2Audio;
        [SerializeField] [FoldoutGroup("PlayerAudio")] private AudioSource jumpAudio;
        [SerializeField] [FoldoutGroup("PlayerAudio")] private AudioSource landAudio;
        [SerializeField] [FoldoutGroup("PlayerAudio")] private AudioSource deathAudio;
        [SerializeField] [FoldoutGroup("PlayerAudio")] private AudioSource dashAudio;
        [SerializeField] [FoldoutGroup("PlayerAudio")] private AudioSource hurtAudio;
        [SerializeField] [FoldoutGroup("PlayerAudio")] private AudioSource turnAroundAudio;
        [SerializeField] [FoldoutGroup("PlayerAudio")] private AudioSource runAudio;

        protected override void Awake()
        {
            base.Awake();
            bgm1.Play();
        }


        public void PlayerAttack1Play () => attack1Audio.Play();
        public void PlayerAttack2Play () => attack2Audio.Play();
        public void PlayerJumpPlay () => jumpAudio.Play();
        public void PlayerLandPlay () => landAudio.Play();
        public void PlayerDeathPlay() => deathAudio.Play();
        public void PlayerDashPlay() => dashAudio.Play();
        public void PlayerHurtPlay() => hurtAudio.Play();
        public void PlayerTurnAroundPlay() => turnAroundAudio.Play();
        public void PlayerRunPlay() => runAudio.Play();
        public void PlayerRunStop() => runAudio.Stop();
    }
}
