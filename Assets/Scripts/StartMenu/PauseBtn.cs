using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseBtn : MonoBehaviour
{
    public GameObject pausePanel; // PausePanel 的 UI 面板
    public Button pauseButton;    // 暂停按钮
    public Button backToWorkButton; // 返回工作的按钮

    private bool isPaused = false; // 当前是否处于暂停状态

    public void Start()
    {
        // 绑定按钮点击事件
        pauseButton.onClick.AddListener(TogglePause);
        backToWorkButton.onClick.AddListener(ResumeGame);

        // 初始化 PausePanel 状态
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    // 切换暂停状态
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    // 暂停游戏
    public void PauseGame()
    {
        Time.timeScale = 0f; // 设置时间缩放为 0，暂停游戏
        if (pausePanel != null)
        {
            pausePanel.SetActive(true); // 显示 PausePanel
        }
    }

    // 恢复游戏
    public void ResumeGame()
    {
        Time.timeScale = 1f; // 设置时间缩放为 1，恢复游戏
        if (pausePanel != null)
        {
            pausePanel.SetActive(false); // 隐藏 PausePanel
        }
    }
}