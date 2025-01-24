using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickUIController : MonoBehaviour
{
    public GameObject clickUI; // ClickUI 的 UI 元素
    public BubbleSpawner bubbleSpawner; // 气泡生成器的引用
    public float fadeDuration = 1f; // 淡出效果持续时间（秒）

    void Start()
    {   
        // 初始状态隐藏 ClickUI
        clickUI.SetActive(false);
        if (clickUI != null)
        {
            clickUI.SetActive(false);
        }
        else
        {
            Debug.LogError("ClickUI is not assigned in the inspector.");
        }

        // 监听气泡生成事件
        if (bubbleSpawner != null)
        {
            bubbleSpawner.OnBubbleSpawningStarted += ShowClickUI;
        }
        else
        {
            Debug.LogError("BubbleSpawner is not assigned in the inspector.");
        }
    }

    void Update()
    {
        // 检测玩家是否点击鼠标
        if (Input.GetMouseButtonDown(0)) // 0 表示鼠标左键
        {
            // 启动淡出效果
            if (clickUI != null && clickUI.activeSelf)
            {
                StartCoroutine(FadeOutAndHide());
            }
        }
    }

    // 显示 ClickUI
    void ShowClickUI()
    {
        if (clickUI != null)
        {
            clickUI.SetActive(true);
            Debug.Log("ClickUI shown.");
        }
    }

    // 淡出效果协程
    IEnumerator FadeOutAndHide()
    {
        CanvasGroup canvasGroup = clickUI.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = clickUI.AddComponent<CanvasGroup>(); // 添加 CanvasGroup 组件
        }

        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime / fadeDuration; // 更新透明度
            canvasGroup.alpha = alpha; // 设置 CanvasGroup 的透明度
            yield return null;
        }

        clickUI.SetActive(false); // 隐藏 ClickUI
        Debug.Log("ClickUI hidden.");
    }

    void OnDestroy()
    {
        // 取消事件监听
        if (bubbleSpawner != null)
        {
            bubbleSpawner.OnBubbleSpawningStarted -= ShowClickUI;
        }
    }
}