using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BasePanel : MonoBehaviour
    {
            protected bool isRemove = false;//界面是否已经关闭
            protected new string name;
            public virtual  void OpenPanel(string name)//打开界面
        {
            this.name = name;
            gameObject.SetActive(true);
        }

        public virtual void ClosePanel()//关闭界面
        {
            isRemove = true;
            gameObject.SetActive(false);//关闭
            Destroy(gameObject);

            //移除缓存,表示界面没有打开
            if (UIManager.Instance.panelDict.ContainsKey(name))
            {
                UIManager.Instance.panelDict.Remove(name);

            }

        }
    }
