using UnityEngine;

namespace C_Script.UI.ScenesBloodBars
{
    public class BloodBarEmpty : MonoBehaviour
    {
        [SerializeField]
        private Transform enemyTrans;
        [SerializeField] 
        private float height;

        private void Update()
        {
            //var position = enemyTrans.position;
            //transform.position = new Vector3(position.x,position.y+height,1);
            //transform.localScale = new Vector3(enemyTrans.localScale.x/Mathf.Abs(enemyTrans.localScale.x), 1, 1);
        }
    }
}
