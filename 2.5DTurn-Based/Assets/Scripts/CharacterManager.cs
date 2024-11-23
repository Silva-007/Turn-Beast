using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//处理角色加入的逻辑
public class CharacterManager : MonoBehaviour
{
    [SerializeField] private GameObject joinPopup;//弹窗对象
    [SerializeField] private TextMeshProUGUI joinPopupText;//弹窗文本
    private bool infrontOfPartyMember;
    private GameObject joinableMember;
    private PlayerControls playerControls;
    private List<GameObject> overworldCharacter = new List<GameObject>();
    // Start is called before the first frame update

    private const string PARTY_JOINED_MESSAGE = "joined The Party!";
    private const string NPC_JOINABLE_TAG = "NPC Joinable";
    private void Awake()
    {
        playerControls = new PlayerControls();//初始化
    }
    void Start()
    {
        playerControls.Player.Interact.performed += _ => Interct();
        SpawnOberworldMembers();
    }

    private void OnEnable()//启用
    {
        playerControls.Enable();//启用播放器
    }
    private void OnDisable()//禁用
    {
        playerControls.Disable();//禁用播放器
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void Interct()
    {
        //若可加入角色不为空且可以交互
        if(infrontOfPartyMember == true && joinableMember != null)
        {
            //将该队员加入队伍
            Debug.Log("加入:"+joinableMember.GetComponent<JoinableCharacterScript>().MemberToJoin);
            MemberJoined(joinableMember.GetComponent<JoinableCharacterScript>().MemberToJoin);
            infrontOfPartyMember = false;
            joinableMember = null;

        }
    }

    private void MemberJoined(PartyMemberInfo partyMember)//添加成员的函数
    {
        //添加成员,输入成员姓名将派对成员添加到派对中
        GameObject.FindFirstObjectByType<PartyManager>().AddMemberToPartyByName(partyMember.MemberName);
        joinableMember.GetComponent<JoinableCharacterScript>().CheckIfJoined();

        joinPopup.SetActive(true);//开启弹窗
        joinPopupText.text = partyMember.MemberName + PARTY_JOINED_MESSAGE;//弹窗文本
        SpawnOberworldMembers();//添加成员时再次刷新主场景
    }

    private void SpawnOberworldMembers()//生成主世界成员
    {
        for(int i = 0; i < overworldCharacter.Count; i++)
        {
            Destroy(overworldCharacter[i]);//摧毁主世界的角色
        }
        overworldCharacter.Clear();//清空列表

        //获取场景中的第一个成员
        List<PartyMember> currentParty = GameObject.FindFirstObjectByType<PartyManager>().GetCurrentParty();

        for(int i = 0; i < currentParty.Count; i++)
        {
            if (i == 0)//第一个成员为玩家
            {
                GameObject player = gameObject;//获取这个预制体
                //玩家视觉对象
                GameObject playerVisual = Instantiate(currentParty[i].MemberOverworVisualPrefab, player.transform.position, Quaternion.identity);
                //Debug.Log("坐标:" + transform.position);

                playerVisual.transform.SetParent(player.transform);//

                player.GetComponent<PControl>().SetOverwordVisuals(playerVisual.GetComponent<Animator>(),playerVisual.GetComponent<SpriteRenderer>(),playerVisual.transform.localScale);//设置视觉效果
                //禁用跟随AI
                playerVisual.GetComponent<MemberFollowAI>().enabled = false;
                overworldCharacter.Add(playerVisual);//添加主世界角色
      
            }
            else//否则其他成员为跟随者
            {
                Vector3 posistionToSpawn = transform.position;//角色坐标
                posistionToSpawn.x = -i;
                //临时跟随者
                GameObject tempFollower= Instantiate(currentParty[i].MemberOverworVisualPrefab, posistionToSpawn, Quaternion.identity);

                //获取跟随者组件跟随
                tempFollower.GetComponent<MemberFollowAI>().SetFollowDistance(i);
                overworldCharacter.Add(tempFollower);

            }
        }
    }

    //碰撞检测开启
    private void OnTriggerEnter(Collider other)
    {
        //若游戏对象标签为NPC可连接标签
        if (other.gameObject.tag == NPC_JOINABLE_TAG)
        {
            infrontOfPartyMember = true;
            joinableMember = other.gameObject;//将当前碰到的角色设置为可加入队员
            //获取UI界面的组件并激活互动面板
            joinableMember.GetComponent<JoinableCharacterScript>().ShowInteractPrompt(true);
        }
    }

    //碰撞检测退出
    private void OnTriggerExit(Collider other)
    {
        //若游戏对象标签为NPC可连接标签
        if (other.gameObject.tag == NPC_JOINABLE_TAG)
        {
            infrontOfPartyMember = false;

            //获取UI界面的组件并激活互动面板
            joinableMember.GetComponent<JoinableCharacterScript>().ShowInteractPrompt(false);
            joinableMember = null;
        }
    }
}
