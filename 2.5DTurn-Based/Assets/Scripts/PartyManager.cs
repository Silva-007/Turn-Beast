using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [SerializeField] private PartyMemberInfo[] allMembers;//存有所有成员的数组
    [SerializeField] private List<PartyMember> currentParty;
    [SerializeField] private PartyMemberInfo defaultPartMember;

    private Vector3 playerPosition;//玩家的三维坐标
    private static GameObject instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this.gameObject;
            AddMemberToPartyByName(defaultPartMember.MemberName);//添加角色信息
        }
        DontDestroyOnLoad(gameObject);
    }
    public void AddMemberToPartyByName(string menberName)
    {
        for (int i=0;i< allMembers.Length; i++)
        {
            if (allMembers[i].MemberName == menberName)
            {
                PartyMember newPartyMember = new PartyMember();
                newPartyMember.MemberName = allMembers[i].MemberName;
                newPartyMember.Level = allMembers[i].StartingLevel;
                newPartyMember.CurrHealth = allMembers[i].BaseHealth;
                newPartyMember.MaxHealth = newPartyMember.CurrHealth;
                newPartyMember.Strength = allMembers[i].BaseStr;
                newPartyMember.Initiative = allMembers[i].BaseInitiative;
                newPartyMember.MemberBattleVisualPrefab = allMembers[i].MemberBattleVisualPrefab;
                newPartyMember.MemberOverworVisualPrefab = allMembers[i].MemberOverworldVisualPrefab;

                currentParty.Add(newPartyMember);//添加新成员



            }
        }
    }
    public List<PartyMember> GetAliveParty()
    {
        //存储所有玩家角色的列表
        List<PartyMember> aliveParty = new List<PartyMember>();
        aliveParty = currentParty;
        for(int i = 0; i < aliveParty.Count; i++)
        {
            //若当前角色血量为0
            if (aliveParty[i].CurrHealth <= 0)
            {
                aliveParty.RemoveAt(i);//将当前角色从列表中移除
            }
        }
        return aliveParty;
    }

    public List<PartyMember> GetCurrentParty()
    {
        return currentParty;
    }

    //同步角色的当前状态
    public void SaveHealth(int partyMember,int health)
    {
        currentParty[partyMember].CurrHealth = health; 
    }
    public void SetPosition(Vector3 posistion)//获取角色当前位置
    {
        playerPosition = posistion;
    }

    public Vector3 GetPosition()
    {
        return playerPosition;//返回玩家坐标
    }
}
[System.Serializable]
public class PartyMember
{
    public string MemberName;
    public int Level;
    public int CurrHealth;//当前血量
    public int MaxHealth;//最大血量
    public int Strength;
    public int Initiative;
    public int CurrExp;//当前经验
    public int MaxExp;//最大经验
    public GameObject MemberBattleVisualPrefab;//公共游戏对象
    public GameObject MemberOverworVisualPrefab;//视觉预制件
    

}