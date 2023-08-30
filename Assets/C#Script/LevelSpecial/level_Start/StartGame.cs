using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace C_Script.LevelSpecial.level_Start
{
    public class StartGame : MonoBehaviour
    {
        [SerializeField] private PlayableDirector passEffect;


        private void Start()
        {
        }

        public void Function()
        {
            StartCoroutine(ChangSence());
        }
        IEnumerator ChangSence()
        {
            passEffect.Play();
            yield return new WaitUntil(() => passEffect.state == PlayState.Paused);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}
