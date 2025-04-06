using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeCave : MonoBehaviour
{
    public static FadeCave Instance;

    public Image fadePanel;
    public float fadeDuration = 1f;

    public  string sceneName = "OverworldScene";

    void Awake()
    {
        if (Instance != null)
        {
            //先删掉之前的，再重新赋值，不要删除自己
            Destroy(Instance.gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        //返回菜单之后不为空，把自己删除了，导致空引用
    }

    public void StartTransition()
    {
        StartCoroutine(TransitionRoutine(sceneName));
    }

    private IEnumerator TransitionRoutine(string sceneName)
    {
        // 淡出效果
        fadePanel.raycastTarget = true; // 阻止点击穿透
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            fadePanel.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
        fadePanel.color = new Color(0f, 0f, 0f, 1f);

        // 异步加载场景
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        // 等待加载进度达到90%（Unity的异步加载特性）
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;

        // 等待场景加载完成
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 淡入效果
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            fadePanel.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
        Debug.Log("渐出");
        fadePanel.color = new Color(0f, 0f, 0f, 0f);
        fadePanel.raycastTarget = false; // 恢复点击
    }
}



