using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="CaiDan/PackageTable",fileName ="packageTable")]
public class BackpackTable : ScriptableObject
{
    public List<PackageTableItem> DataList = new List<PackageTableItem>();
    
}

[System.Serializable]
//背包道具的配置文件
public class PackageTableItem
{
    //道具的静态数据
    public int id;//物品id
    public int type;//物品类型
   // public int star;

    public string name;//道具名称

    public string description;//简单描述
    public string skillDescription;//详细描述

    public string imagePath;//图片路径
}
