using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    public string mainMenuSceneName = "OverworldScene";

    [SerializeField] private GameObject pauseUI; // 拖入PauseUI父物体
    private bool isPaused = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;

            // 初始化时强制隐藏
            if (pauseUI != null)
            {
                pauseUI.SetActive(false);
            }
        }
        else
        {
            Destroy(gameObject);
        }


    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 每次加载场景都强制关闭暂停状态
        //ForceResume();
        if (pauseUI != null)
        {
            pauseUI.SetActive(false);
        }
    }

    void Update()
    {
        //Debug.Log((SceneManager.GetActiveScene().name == mainMenuSceneName));
       // Debug.Log("按键:" + Input.GetKeyDown(KeyCode. R));
        //若当前的活动场景为xxx并且按下ESC按钮
        if (SceneManager.GetActiveScene().name != mainMenuSceneName && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseUI.SetActive(isPaused); // 根据状态显示/隐藏
    }

    // 其他方法保持原样...

    public void ResumeGame()
    {
        TogglePause();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1; // 恢复时间
        TogglePause();
        SceneManager.LoadSceneAsync("MenuScene"); // 替换为你的主菜单场景名
        
    }
}
