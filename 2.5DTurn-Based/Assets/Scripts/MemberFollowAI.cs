using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberFollowAI : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private int speed;

    private float followDist;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Vector3 scale;
    private const string IS_WALK_PARAM = "IsWalk";
    // Start is called before the first frame update
    private void Awake()
    {
        scale = transform.localScale;
    }

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //设置跟随目标
        followTarget = GameObject.FindFirstObjectByType<PControl>().transform;

    }

    // Update is called once per frame
    void FixedUpdate()//固定更新
    {
        //判断角色之间的距离是否超过跟随距离
        if (Vector3.Distance(transform.position, followTarget.position) > followDist)
        {
            //范围修正,若跟随角色离角色太远,直接将跟随角色刷新到主角身边
            if (Vector3.Distance(transform.position, followTarget.position) > 3*followDist)
            {
                Vector3 vector = new Vector3(1, 0, 0);
                transform.position = followTarget.position - vector;
            }
            else
            {
                //超过了则走向玩家
                anim.SetBool(IS_WALK_PARAM, true);//执行行走动画
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, followTarget.position, step);//可能要改
            }
    

            if (followTarget.position.x - transform.position.x < 0)
            {
                spriteRenderer.transform.localScale=new Vector3(-scale.x,scale.y,scale.z);
            }
            else
            {
                spriteRenderer.transform.localScale = new Vector3(scale.x, scale.y, scale.z);
            }
        }

        else
        {
            //否则进入待机状态
            anim.SetBool(IS_WALK_PARAM, false);//关闭行走动画
        }
        
    }

    public void SetFollowDistance(float FollowDistance)//获取跟随距离
    {
        followDist = FollowDistance;
    }
}
