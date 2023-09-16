using C_Script.Common.Interface;
using UnityEngine;

namespace C_Script.Common.Model.ObjectPool
{
    public class PoolPart : MonoBehaviour,ISetParentMessage
    {
        private Transform _followTransform;
        private Transform FollowTransform => _followTransform ? _followTransform : _followTransform = GetComponent<IObjectPool>().SetFollower();
        public void SetOffSet(Vector2 offset)
        {
            transform.position = FollowTransform.position + (Vector3)offset;
        }

        public void SetScaleX_Follow()
        {
            transform.localScale = new Vector3(FollowTransform.localScale.x, 1, 1);
        }

        public void SetScaleX_Unfollow()
        {
            transform.localScale = new Vector3(-FollowTransform.localScale.x, 1, 1);
        }
    }
}
