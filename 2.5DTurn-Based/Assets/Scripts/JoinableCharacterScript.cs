using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinableCharacterScript : MonoBehaviour
{
    public PartyMemberInfo MemberToJoin;//可添加的成员
    [SerializeField] private GameObject interactPrompt;//交互提示

    // Start is called before the first frame update
    void Start()
    {
        CheckIfJoined();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInteractPrompt(bool showPrompt)//显示交互提示
    {
        if (showPrompt == true)//若显示提示为true
        {
            interactPrompt.SetActive(true);//显示UI交互
        }
        else
        {
            interactPrompt.SetActive(false);
        }
    }
    public void CheckIfJoined()//检查当前角色是否已经加入队伍
    {
        //获取当前所有成员的名单
        List<PartyMember> currparty = GameObject.FindFirstObjectByType<PartyManager>().GetCurrentParty();
        
        for(int i = 0; i < currparty.Count; i++)
        {
            //如果待加入的成员名字与成员列表中的成员一致,说明该角色已经入队
            if (currparty[i].MemberName == MemberToJoin.MemberName)
            {
                gameObject.SetActive(false);//关闭该角色
            }
        }
    
    }
}
