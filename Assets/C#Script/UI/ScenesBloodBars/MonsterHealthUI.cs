using C_Script.BaseClass;
using C_Script.Eneny.EnemyCommon.Model;
using C_Script.Eneny.EnemyCreator;
using C_Script.Eneny.Monster.Magician.Core;
using UnityEngine;

namespace C_Script.UI.ScenesBloodBars
{
    public class MonsterHealthUI : MonoBehaviour
    {
        private float _bloodLength;
        private EnemyData _data;
        private float _localScaleX;
        private float _heathRate;
        private float _originX;

        private void OnEnable()
        {
            _data = GetComponentInParent<EnemyModel>().EnemyData;
            _originX = transform.localPosition.x;
        }

        private void Start()
        {
            var transformTemp = transform;
            _localScaleX = transformTemp.localScale.x;
            _bloodLength = _localScaleX;
        }

        private void Update()
        {
            var transform1 = transform;
            _heathRate = Mathf.Clamp(_data.CurrentHealth / _data.MaxHealth, 0, 1);
            if (_heathRate == 0)
            {
                transform1.parent.gameObject.SetActive(false);   
            }
            transform1.localScale = new Vector3(_localScaleX * _heathRate, transform1.localScale.y, 1);
            transform1.localPosition = new Vector3(_originX - (1 - _heathRate) / 4 * _bloodLength,
                transform.localPosition.y, 0);
        }
    }
}
