using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//资源菜单
[CreateAssetMenu(menuName="New Party Member")]
public class PartyMemberInfo : ScriptableObject
{
    public string MemberName;
    public int StartingLevel;
    public int BaseHealth;
    public int BaseStr;
    public int BaseInitiative;
    public GameObject MemberBattleVisualPrefab;//战斗显示内容
    public GameObject MemberOverworldVisualPrefab;//主世界场景中要展示的内容
}
