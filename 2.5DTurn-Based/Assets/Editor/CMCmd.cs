using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CMCmd
{
    [MenuItem("CMCmd/读取表格")]
//带有Unity特性的Menu静态方法,可以保证编辑器顶部菜单栏中生成按钮,方便点击并执行相应逻辑
    public static void ReadTable()
    {
        //读取表格
            BackpackTable backpacktable = Resources.Load<BackpackTable>("DataTable/packageTable");
            foreach(PackageTableItem packageItem in backpacktable.DataList)
        {
            Debug.Log(string.Format("id:{0}, name:{1}",packageItem.id, packageItem.name));
        }
     
    }


    [MenuItem("CMCmd/创建背包测试数据")]
    public static void CreateLoadPackageData()
    {
        //保存数据
        PackageLoadData.Instance.items = new List<packageLoadItem>();
        for(int i = 0; i < 9; i++)
        {
            packageLoadItem packageLoadItem = new()
            {
                uid = Guid.NewGuid().ToString(),
                id = i,
                num = i,
                level = i,
                isNew = i % 2 == 1
            };

            PackageLoadData.Instance.items.Add(packageLoadItem);
        }
        PackageLoadData.Instance.SavePackage();
    }

    [MenuItem("CMCmd/读取背包测试数据")]
    public static  void ReadLoadPackageData()
    {
        //读取数据
        List<packageLoadItem> readItems = PackageLoadData.Instance.LoadPackage();
        foreach (packageLoadItem item in readItems)
        {
            Debug.Log(item);
        }
    }

    [MenuItem("CMCmd/打开背包主界面")]
    public static  void OpenPackagePanel()
    {
        UIManager.Instance.OpenPanel(UIConst.BackpackPanel);
    }
}
