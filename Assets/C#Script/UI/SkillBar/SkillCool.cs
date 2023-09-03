using System;
using System.Collections;
using C_Script.Player.Data;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace C_Script.UI.SkillBar
{
    public class SkillCool : MonoBehaviour
    {
        [SerializeField] private float coolDown;
        [SerializeField] private string skillName;
        [SerializeField] private KeyCode keyCode;
        private SkillData _data;
        private float _timer;
        private Image _image;
        private int _trigger=0;
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
                _data.skillBools[skillName] = true;
            }

            if (_data.skillBools[skillName])
            {
                if (_trigger == 0)
                {
                    _timer -= Time.deltaTime;
                    _image.fillAmount = _timer / coolDown;
                }
                if (_timer < 0)
                {
                    _trigger = 1;
                }
                if(_trigger == 1)
                {
                    _timer += Time.deltaTime;
                    _image.fillAmount = _timer / coolDown;
                    if (Math.Abs((_image.fillAmount = _timer / coolDown) - 1) < 0.01)
                    {
                        _data.skillBools[skillName] = false;
                        _trigger = 0;
                    }
                }
            }
        }
    }
}
