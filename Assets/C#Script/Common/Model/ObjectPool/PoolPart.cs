using C_Script.Common.Interface;
using UnityEngine;

namespace C_Script.Common.Model.ObjectPool
{
    public class PoolPart : MonoBehaviour,ISetParentMessage
    {
        [SerializeField] private Transform followTransform;
        public void SetParentTrans(Vector2 offset)=> transform.position = followTransform.position + (Vector3)offset;


        public void SetScaleX_Follow()=>transform.localScale = new Vector3(followTransform.localScale.x,1,1);


        public void SetScaleX_Unfollow()=>transform.localScale = new Vector3(-followTransform.localScale.x,1,1);

    }
}
