using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class BadResultController : MonoBehaviour
{
    public float delayBeforeClick = 5f; // 点击事件生效的延迟时间（秒）
    private float timer = 0f; // 计时器
    private bool isClickEnabled = false; // 点击事件是否生效
    public AudioSource MainBGMneedtobeStopped; // 需要阻止播放的背景音乐
    private float originalVolume; // 保存原始音量


    void Update()
    {
        // 更新计时器（使用 Time.unscaledDeltaTime）
        if (!isClickEnabled)
        {
            timer += Time.unscaledDeltaTime;
            Debug.Log($"Timer: {timer}"); // 打印计时器的值

            // 如果计时器超过延迟时间，启用点击事件
            if (timer >= delayBeforeClick)
            {
                isClickEnabled = true;
                Debug.Log("Click event is now enabled.");
            }
        }

        // 检测鼠标点击或触摸屏幕
        if (isClickEnabled && Input.GetMouseButtonDown(0)) // 0 表示鼠标左键
        {
            Debug.Log("Screen clicked. Loading StartScene.");
            LoadStartScene();
        }
    }

    // 跳转到 StartScene
    void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene"); // 替换为你的 StartScene 名称
    }

     void OnEnable()
    {
        // 当 BadResultScreen 激活时，停止背景音乐
        if (MainBGMneedtobeStopped != null && MainBGMneedtobeStopped.isPlaying)
        {   
            originalVolume = MainBGMneedtobeStopped.volume; // 保存原始音量
            MainBGMneedtobeStopped.volume = 0f; // 将音量设置为 0
            MainBGMneedtobeStopped.Stop(); // 停止背景音乐
        }
    }

    void OnDisable()
    {
        // 当 BadResultScreen 禁用时，重新播放背景音乐
        if (MainBGMneedtobeStopped != null && !MainBGMneedtobeStopped.isPlaying)
        {
            MainBGMneedtobeStopped.volume = originalVolume; // 恢复原始音量
            MainBGMneedtobeStopped.Play(); // 播放背景音乐
        }
    }
}