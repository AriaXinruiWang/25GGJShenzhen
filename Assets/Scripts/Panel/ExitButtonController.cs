using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitButtonController : MonoBehaviour
{
    public Button exitButton; // ExitButton 的 Button 组件

    void Start()
    {
        // 绑定按钮点击事件
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(OnExitButtonClicked);
        }
        else
        {
            Debug.LogError("ExitButton is not assigned in the inspector.");
        }
    }

    // 退出按钮点击事件
    public void OnExitButtonClicked()
    {
        Debug.Log("Exit button clicked. Loading StartScene.");
        LoadStartScene();
    }

    // 跳转到 StartScene
    public void LoadStartScene()
    {
        Time.timeScale = 1f; // 恢复时间缩放
        SceneManager.LoadScene("StartScene"); // 替换为你的 StartScene 名称
    }
}