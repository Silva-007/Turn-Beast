using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//场景管理

public class PControl : MonoBehaviour
{
    //序列化,让私有变量也可以在引擎上调整
    [SerializeField] private int speed;
    [SerializeField] private Animator anim;
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] private LayerMask GressLayer;//草地图层
    [SerializeField] private int stepsInGrass;//计算在草地上走了多少步
    [SerializeField] private int minStepsToEncounter;//最小随机数
    [SerializeField] private int maxStepsToEncounter;//最大随机数


    //私人播放器控件
    private PlayerControls playerControls;
    private Rigidbody rb;
    private Vector3 movement;
    private Vector3 scale;
    private bool movingInGrass;//是否在草地上移动
    private float stepTimer;//阈值
    private int stepToEncounter;//随机数
    private PartyManager partyManager;

    private const string IS_WALK_PARAM="IsWalk";//引用isWalk参数
    private const string BATTLE_SCENE = "BattleScene";//战斗场景
    private const float TIME_PER_STEP = 0.5f;//在草丛中行走时需要花的时间
    private const string AI_NAME = "NPC";

    
    private void Awake()//唤醒函数
    {
        playerControls = new PlayerControls();
        CalculateStepsToNextEncounter();
    }
    private void OnEnable()
    {
        playerControls.Enable();//启动玩家控制器
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();//获取刚体组件
        partyManager = GameObject.FindFirstObjectByType<PartyManager>();

        //如果保存了一个位置,就去移动玩家
        if(partyManager.GetPosition() != Vector3.zero)//若战斗角色坐标不为空
        {
            //将战斗时角色的坐标赋给现实中角色的坐标
            transform.position = partyManager.GetPosition();
        }
       // GameObject obj = GameObject.FindWithTag(AI_NAME);
        //Debug.Log("AI坐标:"+obj.transform.position);


    }
    // Update is called once per frame
    void Update()//每帧执行一次
    {
        //左正右负
        float x = playerControls.Player.Move.ReadValue<Vector2>().x;
        float z = playerControls.Player.Move.ReadValue<Vector2>().y;

        movement = new Vector3(x, 0, z).normalized;//归一化
        //设置动画 动画的问题，看动画设置
        anim.SetBool(IS_WALK_PARAM, movement != Vector3.zero);//当三维坐标不为0时,播放行走动画

        //角色翻转
        if (x!=0 && x < 0)
        {
            playerSprite.transform.localScale=new Vector3(-scale.x,scale.y,scale.z);//flipX:x轴翻转
        }
        if(x!=0 && x >= 0)
        {
            playerSprite.transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
    }

//FixeUpdate:用于处理和物理移动,时间相关的逻辑,固定更新时间为0.02s
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);
        //返回球体内所有的碰撞器
        Collider[] colliders = Physics.OverlapSphere(transform.position,1, GressLayer);
        //如果移动过程中与草曾触发器发生了碰撞
        movingInGrass = colliders.Length!=0 && movement!=Vector3.zero;

        if (movingInGrass)//如果角色在草地上移动,增加步数计时器
        {
            stepTimer+=Time.fixedDeltaTime;//启动计时器
            //如果步数计时器到了指定的阈值,添加角色步数并重置步数计时器
            if (stepTimer>TIME_PER_STEP)
            {
                stepsInGrass++;//角色步数添加
                stepTimer = 0;

                if (stepsInGrass>= stepToEncounter)//如果在草丛中行走的步数大于遭遇的步数
                {
                    partyManager.SetPosition(transform.position);//固定角色当前的战斗坐标
                    SceneManager.LoadScene(BATTLE_SCENE);//加载战斗场景
                }

            }
        }
    }

    //随机点范围函数,用于分配步数以及遇到最小步数之间的值和遭遇的最大步数
    private void CalculateStepsToNextEncounter()
    {
        //随机数,表示遭遇战的步数在最大随机数和最小随机数的范围内随机一个值
        stepToEncounter = Random.Range(minStepsToEncounter,maxStepsToEncounter);
        Debug.Log(stepToEncounter);
    }

    public void SetOverwordVisuals(Animator animator,SpriteRenderer spriteRenderer,Vector3 playerScale)
    {
        anim = animator;
        playerSprite = spriteRenderer;
        scale = playerScale;
    }
}
