using System;
using UnityEngine;

namespace C_Script.Common
{
    public class AnimationEventTrigger : MonoBehaviour
    {
        public void DestroyThis()=> Destroy(gameObject);
        public void Inactive() => gameObject.SetActive(false);
    }
}
