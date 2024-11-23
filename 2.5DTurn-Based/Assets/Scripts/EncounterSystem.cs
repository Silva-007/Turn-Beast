using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterSystem : MonoBehaviour
{
    [SerializeField] private Encounter[] enemiesInScene;//场景中的敌人
    [SerializeField] private int maxNumEnemies;//生成的敌人数量
    // Start is called before the first frame update

    private EnemyManager enemyManager;

    void Start()
    {
        //获取敌人管理器组件
        enemyManager = GameObject.FindAnyObjectByType<EnemyManager>();
        //通过遭遇生成敌人
        enemyManager.GenerateEnemiesByEncounter(enemiesInScene,maxNumEnemies);


    }


}

[System.Serializable]
public class Encounter
{
    public EnemyInfo Enemy;//敌人属性
    public int LevelMin;//最低等级
    public int LevelMax;//最高等级

}
