using C_Script.Eneny.Monster.Magician.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.Eneny.Monster.Magician.RayDraw
{
    public enum RangeType
    {
        Empty,
        MeleeAttack,
        RemoteAttack,
        Pursuit,
    }
    
    public class RayDraw : MonoBehaviour
    {
        [SerializeField] private RangeType rangeType;
        [SerializeField] private Transform ownerTrans;
        [SerializeField] private float pursuitRange;
        [SerializeField] private float meleeAttackRange;
        [SerializeField] private float remoteAttackRange;
        
        private void Update()
        {
            if(rangeType==RangeType.Pursuit)
            {
                var position = ownerTrans.position;
                Debug.DrawLine(position,new Vector2(position.x-pursuitRange,position.y));
                Debug.DrawLine(position,new Vector2(position.x+pursuitRange,position.y));
            }

            else if(rangeType==RangeType.MeleeAttack)
            {
                var position = ownerTrans.position;
                Debug.DrawLine(position,new Vector2(position.x-meleeAttackRange,position.y));
                Debug.DrawLine(position,new Vector2(position.x+meleeAttackRange,position.y));
            }
            else if (rangeType == RangeType.RemoteAttack)
            {
                var position = ownerTrans.position;
                Debug.DrawLine(position,new Vector2(position.x-remoteAttackRange,position.y));
                Debug.DrawLine(position,new Vector2(position.x+remoteAttackRange,position.y));
            }
        }
    }
}
