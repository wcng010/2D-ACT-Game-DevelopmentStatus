using System;
using C_Script.BaseClass;
using C_Script.Common.Model.Singleton;
using UnityEngine;


namespace C_Script.Eneny.EnemyCreator
{
    public class EnemyFactory : Singleton<EnemyFactory>
    {
        public ObjectListSo EnemySo => enemySo;
        [SerializeField]private ObjectListSo enemySo;

        public GameObject CreatEnemy(String enemyName)
        {
            foreach (var enemy in enemySo.objList)
            {
                if (enemy.name == enemyName)
                {
                    GameObject.Instantiate(enemy, transform);
                    return enemy;
                }
            }
            //Debug.LogError("No this enemyName to creat");
            return null;
        }

        public void AppendEnemy(GameObject addedEnemy)
        {
            try
            {
                enemySo.objList.Add(addedEnemy);
            }
            catch (Exception)
            {
                Console.WriteLine("Add Enemy Error");
                throw;
            }
        }
        
        public void DeleteEnemy(String enemyName) 
        {
            foreach (var enemy in enemySo.objList)
            {
                if (enemy.name == "enemyName")
                {
                    enemySo.objList.Remove(enemy);
                }
            }
        }

        private void Start()
        {
            CreatEnemy("Magician");
        }
    }


}
