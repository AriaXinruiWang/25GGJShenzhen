using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpaceUIController : MonoBehaviour
{
    public GameObject spaceUI; // SpaceU 的 UI 元素
    public float fadeDuration = 1f; // 淡出效果持续时间（秒）

    void Start()
    {
        // 游戏开始时显示 SpaceU
        if (spaceUI != null)
        {
            spaceUI.SetActive(true);
        }
        else
        {
            Debug.LogError("SpaceU is not assigned in the inspector.");
        }
    }

    void Update()
    {
        // 检测玩家是否按下空格键
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 启动淡出效果
            StartCoroutine(FadeOutAndHide());
        }
    }

    // 淡出效果协程
    IEnumerator FadeOutAndHide()
    {
        if (spaceUI != null)
        {
            CanvasGroup canvasGroup = spaceUI.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = spaceUI.AddComponent<CanvasGroup>(); // 添加 CanvasGroup 组件
            }

            float alpha = 1f;
            while (alpha > 0f)
            {
                alpha -= Time.deltaTime / fadeDuration; // 更新透明度
                canvasGroup.alpha = alpha; // 设置 CanvasGroup 的透明度
                yield return null;
            }

            spaceUI.SetActive(false); // 隐藏 SpaceU
            Debug.Log("SpaceU hidden.");
        }
    }
}