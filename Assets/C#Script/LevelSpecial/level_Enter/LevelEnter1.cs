using System.Collections;
using C_Script.Common.Model.EventCentre;
using UnityEngine;
using UnityEngine.Playables;


namespace C_Script.LevelSpecial.level_Enter
{
    public class LevelEnter1 : MonoBehaviour
    {
        private PlayableDirector _effect;

        private void Awake()
        {
            _effect = GetComponent<PlayableDirector>();
        }

        private void Start()
        {
            _effect.Play();
            CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerStop);
            StartCoroutine(WaitTimeline());
        }

        IEnumerator WaitTimeline()
        {
            yield return new WaitUntil(() => _effect.state == PlayState.Paused);
            CombatEventCentreManager.Instance.Publish(CombatEventType.PlayerStart);
        }
    }
}
