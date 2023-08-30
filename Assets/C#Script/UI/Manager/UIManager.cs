using C_Script.Common.Model.EventCentre;
using C_Script.LevelSpecial.level_Start;
using UnityEngine;
using UnityEngine.Serialization;


namespace C_Script.UI.Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject endMenu;
        [SerializeField] private GameObject restartMenu;
        private void Awake()
        {
            ScenesEventCentreManager.Instance.Subscribe(ScenesEventType.GameOver,OpenExitMenu);
            ScenesEventCentreManager.Instance.Subscribe(ScenesEventType.ReStart,OpenReStartMenu);
        }
        private void OpenExitMenu() => endMenu.SetActive(true);
        private void OpenReStartMenu()
        {
            restartMenu.SetActive(true);
        }
    }
}
