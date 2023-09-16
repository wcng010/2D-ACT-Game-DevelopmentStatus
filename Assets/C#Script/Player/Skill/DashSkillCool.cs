using System.Collections;
using C_Script.Manager;
using C_Script.Player.Data;
using C_Script.UI.SkillBar;
using UnityEngine;
using UnityEngine.UI;

namespace C_Script.Player.Skill
{
    public class DashSkillCool : SkillCool
    {
        [SerializeField] private float coolDown;
        [SerializeField] private string skillName;

        private SkillData _data;
        private float _timer;
        private Image _image;
        private int _clock;
        private void Awake()
        {
            _image = GetComponent<Image>();
            _data = GetComponentInParent<SkillManager>().skillData;
            _data.skillBools[skillName] = true;
            _timer = coolDown;
        }

        private void OnEnable()
        {
            InputManager.Instance.KeyEventQ.AddListener(UpdateSkillCool);
        }

        private void OnDisable()
        {
            InputManager.Instance.KeyEventQ.RemoveListener(UpdateSkillCool);
        }


        public override void UpdateSkillCool()
        {
            if (_clock == 1) return;
            StartCoroutine(UpdateSkill());
        }

        IEnumerator UpdateSkill()
        {
            _timer = coolDown;
            _clock = 1;
            while (_timer > 0)
            {
                _timer -= Time.deltaTime;
                _image.fillAmount = _timer / coolDown;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            while (_timer < coolDown)
            {
                _timer += Time.deltaTime;
                _image.fillAmount = _timer / coolDown;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            _data.skillBools[skillName] = true;
                _clock = 0;
        }
    }
}
