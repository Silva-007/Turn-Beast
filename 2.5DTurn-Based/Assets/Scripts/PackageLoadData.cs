using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//将背包数据以json的形式存储在本地
public class PackageLoadData
{
    private static PackageLoadData _instance;

    public static PackageLoadData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PackageLoadData();
            }
            return _instance;
        }
    }

    public List<packageLoadItem> items;//缓存当前物品所有的动态信息


    //数据没有存储在本地？
    public void SavePackage()//存储数据
    {
        //把表格信息序列化为字符串
        string inventoryJson = JsonUtility.ToJson(this);
        //把文本数据存储到本地的文件中
        PlayerPrefs.SetString(" PackageLoadData",inventoryJson);
        PlayerPrefs.Save();

    }

    public  List<packageLoadItem> LoadPackage()//读取数据
    {
        //先判断缓存数据是否还存在,如果存在就直接返回存储信息
        if (items != null)//代表之前读取过文本信息
        {
            return items;
        }
        //否则在本地文件中读取
        if (PlayerPrefs.HasKey("PackageLoadData"))
        {
            //把本地文件读取到内存中使其成为字符串
            string inventoryJson = PlayerPrefs.GetString("PackageLoadData");
            //使用JsonUtility反序列化
            PackageLoadData packageLoadData = JsonUtility.FromJson<PackageLoadData>(inventoryJson);
            items = packageLoadData.items;
            //其实就是把文件变成字符串然后再变成类这个操作
            return items;
        
        }
        else
        {
            items = new List<packageLoadItem>();
            return items;
        }
    }



}


[System.Serializable]
public class packageLoadItem
{
    public string uid;//唯一标识符
    public int id;//表示道具类型
    public int num;//道具数量
    public int level;//道具等级
    public bool isNew;//是否为新获得道具

    public override string ToString()
    {
        return string.Format("id:{0}, num:{1}",id,num);
    }
}


