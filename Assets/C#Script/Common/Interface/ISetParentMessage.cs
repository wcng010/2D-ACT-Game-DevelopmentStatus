using UnityEngine;

namespace C_Script.Common.Interface
{
    public interface ISetParentMessage
    {
        void SetOffSet(Vector2 offset);
        void SetScaleX_Follow();

        void SetScaleX_Unfollow();

    }
}
