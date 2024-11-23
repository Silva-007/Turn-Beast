using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleVisual : MonoBehaviour
{
    [SerializeField] private Slider healthBar;//血条
    [SerializeField] private TextMeshProUGUI levelText;//等级文本

    private int currHealth;//当前血量
    private int maxHealth;//最大血量
    private int level;//等级
    private Animator anim;

    //一些常用的常量
    private const string LEVEL_ABB = "Lv ";
    private const string IS_ATTACK_PARAM = "IsAttack";
    private const string IS_HIT_PARAM = "IsHit";
    private const string IS_DEAD_PARAM = "IsDead";

    // Start is called before the first frame update
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        
    }

    //更新角色信息
    public void SetStartingValues(int currHealth,int maxHealth,int level)
    {
        //对角色数据进行初始化
        this.currHealth = currHealth;
        this.maxHealth = maxHealth;
        this.level = level;

        levelText.text = LEVEL_ABB + this.level.ToString();//将角色当前等级写入文本内

        UpdataHealthBar();

    }

    public void ChangeHealth(int currHealth)
    {
        this.currHealth = currHealth;//更新当前血条
        //若当前生命值为0,播放死亡动画
        if (this.currHealth <= 0)
        {
            PlayDeadAnimation();
            Destroy(gameObject,1f);//销毁角色预制体
        }
        //更新状态栏
        UpdataHealthBar();
    }

    //更新角色状态
    public void UpdataHealthBar()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currHealth;

        //更新当前血条
        //ChangeHealth(currHealth);
    }

    public void PlayAttackAnimation()//攻击动画触发器
    {
        anim.SetTrigger(IS_ATTACK_PARAM);
    }
    public void PlayHitAnimation()//受击动画触发器
    {
        anim.SetTrigger(IS_HIT_PARAM);//莫名起码丢失动画
    }
    public void PlayDeadAnimation()//死亡动画触发器
    {
        anim.SetTrigger(IS_DEAD_PARAM);
    }
}
