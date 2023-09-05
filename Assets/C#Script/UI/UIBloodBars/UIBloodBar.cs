using System;
using C_Script.BaseClass;
using C_Script.Common.Model.EventCentre;
using C_Script.Player.MVC.Model;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.UI.UIBloodBars
{
    [RequireComponent(typeof(RectTransform))]
    public class UIBloodBar : MonoBehaviour
    {
        private AttackObjectDataSo AttackObjectDataSo => FindObjectOfType<PlayerModel>().PlayerData;
        private float _heathRate;
        private float _xsclae;
        private float _halfLength;
        private float _originX;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = this.GetComponent<RectTransform>();
            _xsclae = _rectTransform.localScale.x;
            _halfLength = _rectTransform.rect.width / 2*_xsclae;
            _originX = _rectTransform.localPosition.x;
            CombatEventCentreManager.Instance.Subscribe(CombatEventType.PlayerHurt,UpdateHealth);
        }
        
        private void UpdateHealth()
        {
            _heathRate =Mathf.Clamp(AttackObjectDataSo.CurrentHealth / AttackObjectDataSo.MaxHealth,0,1);
            _rectTransform.localScale = new Vector3(_xsclae*_heathRate,_rectTransform.localScale.y,1);
            _rectTransform.localPosition = new Vector3(_originX-(1-_heathRate)*_halfLength,transform.localPosition.y,0);
        }
    }
}
