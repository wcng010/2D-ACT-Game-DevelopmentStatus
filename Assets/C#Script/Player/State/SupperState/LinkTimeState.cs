using C_Script.Player.BaseClass;
using C_Script.Player.Data;
using C_Script.Player.StateModel.BaseState;
using UnityEditor;
using UnityEngine;

namespace C_Script.Player.StateModel.SupperState
{
    public class LinkTimeState : PlayerState
    {
        private float _timeScale;
        private float _time;
        public override void Enter()
        {
            
        }

        public override void PhysicExcute()
        {

        }

        public override void LogicExcute()
        {
            if (Input.GetKeyDown(KeyCode.I)&&!PlayerData.IsLinkTime)
            {
                PlayerData.IsLinkTime = true;
                _timeScale = Time.timeScale;
                Time.timeScale = 0.1f;
                _time = 0;
            }
            _time += Time.unscaledDeltaTime;
            if (Input.GetKeyDown(KeyCode.I)&&PlayerData.IsLinkTime&&_time>=1f)
            {
                PlayerData.IsLinkTime = false;
                Time.timeScale = _timeScale;
            }
        }
        public override void Exit()
        {

        }
        
        public LinkTimeState(PlayerBase owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            
        }
    }
}