using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyInfo[] allEnemies;//存有所有成员的数组
    [SerializeField] private List<Enemy> currentEnemies;//敌人列表

    //这个单例的作用是什么?
    private static GameObject instance;//用静态变量确保一个场景中一次只存在一个敌人
    private const float LEVEL_MODIFIER = 0.5f;

    private void Awake()
    {
        
        if (instance != null)//如果实例不为空
        {
            Destroy(this.gameObject);//销毁这个对象
        }
        else
        {
            instance = this.gameObject;//否则将该变量赋给实例
        }
//个人认为敌人完全没有必要保持现状,应该每次战斗结束后重新随机一次敌人数量

        DontDestroyOnLoad(gameObject);//保证敌人在场景切换时不被销毁
        /**/

    }
    //用于随机生成敌人
    public void GenerateEnemiesByEncounter(Encounter[] encounters,int maxNumEnemies)
    {
        currentEnemies.Clear();//清除敌人列表
        int numEnemies=Random.Range(1,maxNumEnemies+1);//随机敌人的数量,在1~3之间

        for(int i = 0; i < numEnemies; i++)
        {
            //用临时变量存储随机遭遇
            Encounter tempEncounter = encounters[Random.Range(0, encounters.Length)];//随机怪物数量
            //随机怪物等级
            int level = Random.Range(tempEncounter.LevelMin, tempEncounter.LevelMax+1);
            //生成怪物
            GeneratEnemyByName(tempEncounter.Enemy.EnemyName, level);
        }
        

    }
    private void GeneratEnemyByName(string enemyName,int level)
    {
        for(int i = 0; i < allEnemies.Length; i++)
        {
            if (enemyName == allEnemies[i].EnemyName)//可以考虑去掉,意义不明的判断条件
            {
                Enemy newEnemy = new Enemy();
                //敌人信息
                newEnemy.EnemyName = allEnemies[i].EnemyName;
                newEnemy.Level = level;
//怪物等级的成长曲线,怪物每加一级属性上升值为:属性*levelModifier
                float levelModifier = (LEVEL_MODIFIER * newEnemy.Level);
                //新敌人的最大生命=可编写脚本的对象基本生命值
                newEnemy.MaxHealth = Mathf.RoundToInt(allEnemies[i].BaseHealth+(allEnemies[i].BaseHealth*levelModifier));
                newEnemy.CurrHealth = newEnemy.MaxHealth;
                newEnemy.Strength = Mathf.RoundToInt(allEnemies[i].BaseStr+(allEnemies[i].BaseStr*levelModifier));
                newEnemy.Initiative = Mathf.RoundToInt(allEnemies[i].BaseInitiative + (allEnemies[i].BaseInitiative * levelModifier));
                //敌人视觉预制件
                newEnemy.EnemyVisualPrefab = allEnemies[i].EnemyVisualPrefab;

                currentEnemies.Add(newEnemy);

            }
        }
    }

    public List<Enemy> GetCurrentEnemies()
    {
        return currentEnemies;
    }
}

[System.Serializable]
public class Enemy
{
    public string EnemyName;
    public int Level;
    public int CurrHealth;
    public int MaxHealth;
    public int Strength;
    public int Initiative;
    public GameObject EnemyVisualPrefab;

}