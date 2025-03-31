using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public string NextSceneName = "OverworldScene";
    public void GameStart()//开始游戏按钮
    {
        SceneManager.LoadScene(NextSceneName,LoadSceneMode.Single);
    }

    public void AppExit()//退出游戏按钮
    {
      //  Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;//在测试中退出
        Debug.Log("ok");
    }
}
