using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager
    {
        private static UIManager _instance;
        //搭建界面的配置路径表
        private Dictionary<string, string> pathDict;
        private Dictionary<string, GameObject>prefabDict;//预制件缓存字典
        public Dictionary<string, BasePanel> panelDict;//已打开界面缓存字典

        private  Transform _uiRoot;
        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UIManager();
                }
                return _instance;
            }
        }

        //UI最顶层的父节点
        public Transform UIRoot
        {
            get
            {
                if (_uiRoot == null)
                {
                    //最顶层的canvas画布
                    _uiRoot = GameObject.Find("Canvas").transform;
                }
                return _uiRoot;
            }

        }
        private UIManager()//对字典进行初始化
        {
            prefabDict = new Dictionary<string, GameObject>();
            panelDict = new Dictionary<string, BasePanel>();

            pathDict = new Dictionary<string, string>()
            {
                //{UIConst.BackpackCanvas,"OverworldScene/BackpackCanvas" }//把界面路径配置到字典中
                { UIConst.BackpackPanel,"Package/BackpackPanel"}
            };
        }


        //打开界面的逻辑
        public   BasePanel OpenPanel(string name)
        {
            BasePanel panel = null;
            //检查界面是否打开
            if (panelDict.TryGetValue(name,out panel))
            {
                return null;//若界面已经打开,就直接跳出函数
            }

            //检查路径是否有配置
            string path = " ";
            if (!pathDict.TryGetValue(name,out  path))
            {
                return null;//若路径不存在配置表中,也不打开该界面
            }

            //加载界面预制件
            //使用缓存预制件
            GameObject panelPrefab = null;

            //加载预制件之前先在缓存字典中查看一下
            if(!prefabDict.TryGetValue(name, out panelPrefab))
            {
                //若界面已经被加载过,就直接拿出来用，若已经加载过,就放在缓存字典中
                 string realPath = "Prefab/Panel/" + path;///可能需要更改路径
                //
                panelPrefab = Resources.Load<GameObject>(realPath) as GameObject;
                prefabDict.Add(name,panelPrefab);

            }
            //加载预制件并挂在uiroot上
            GameObject panelObject = GameObject.Instantiate(panelPrefab,UIRoot,false);
            panel = panelObject.GetComponent<BasePanel>();
            panelDict.Add(name, panel);//表示该界面已经打开

            return panel;
        }

        //关闭界面的逻辑
        public bool ClosePanel(string name)
        {
            //检查该界面是否在panelDict中
            BasePanel panel = null;
            //界面被打开才需要关闭它
            if (!panelDict.TryGetValue(name, out panel))
            {
                return false;
            }
            panel.ClosePanel();
            return true;

        }



    }

    public  class UIConst//存储菜单名称的常量表
    {
        //public const string BackpackCanvas = "BackpackCanvas";//背包界面的名称
        public const string BackpackPanel = "BackpackPanel";
    }


