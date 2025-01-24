using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultScreenController : MonoBehaviour
{
    void Update()
    {
        // 检测鼠标点击或触摸屏幕
        if (Input.GetMouseButtonDown(0)) // 0 表示鼠标左键
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
}