using C_Script.BaseClass;
using C_Script.Eneny.Monster.Magician.BaseClass;
using C_Script.Eneny.Monster.Magician.Model;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.Eneny.Monster.Magician.Component
{
    public class MeteoriteComponent : CoreComponent
    {
        [SerializeField]private GameObject meteoritePrefab;
        [SerializeField]private float meteoritePointY;
        [SerializeField]private Transform ownerTransform;
        
        protected override void Awake()
        {
        }
        private void Update()
        {
            transform.localScale = ownerTransform.localScale;
        }

        public GameObject FireMateorite()
        {
            var position = transform.position;
            var meteorite = Instantiate(meteoritePrefab, new Vector3(position.x, position.y + meteoritePointY),
                new Quaternion(0,0,0,0),transform);
            return meteorite;
        }
    }
}
