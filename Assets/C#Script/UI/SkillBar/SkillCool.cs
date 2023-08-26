using System;
using C_Script.Player.Data;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace C_Script.UI.SkillBar
{
    public class SkillCool : MonoBehaviour
    {
        [SerializeField] private float coolDown;
        [SerializeField] private int skillIndex;
        [SerializeField] private KeyCode keyCode;
        private SkillData _data;
        private float _timer;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _data = GetComponentInParent<SkillManager>().skillData;
            _timer = coolDown;
        }

        private void Update()
        {
            if (Input.GetKeyDown(keyCode))
            {
                _data.skillBools[skillIndex] = true;
            }

            if (_data.skillBools[skillIndex])
            {
                _timer -= Time.deltaTime;
                _image.fillAmount = _timer / coolDown;
                if (_timer < 0)
                {
                    _data.skillBools[skillIndex] = false;
                }
            }
            else if (!_data.skillBools[skillIndex]&&_timer<coolDown)
            {
                _timer += Time.deltaTime;
                _image.fillAmount = _timer / coolDown;
            }
        }
    }
}
