using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPause : MonoBehaviour
{
    public GameObject pauseMenu; // 暂停菜单的 UI 面板
    private bool isPaused = false; // 当前是否处于暂停状态

    // 公开方法，供 Button 的 On Click 事件调用
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
    private void PauseGame()
    {
        Time.timeScale = 0f; // 设置时间缩放为 0，暂停游戏
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true); // 显示暂停菜单
        }
    }

    // 恢复游戏
    private void ResumeGame()
    {
        Time.timeScale = 1f; // 设置时间缩放为 1，恢复游戏
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true); // 显示暂停菜单
        }
    }
}