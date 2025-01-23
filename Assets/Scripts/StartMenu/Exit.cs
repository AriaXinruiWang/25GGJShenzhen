using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{   // 添加一个可序列化的字段
    public bool isEnabled = true;

    public void OnExitGame()
    {
        if (!isEnabled) return; // 如果未启用，直接返回

        // 检查是否在 Unity 编辑器中运行
        if (Application.isEditor)
        {
            // 如果在编辑器中，停止播放模式
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
        else
        {
            // 如果在构建的游戏版本中，退出应用程序
            Application.Quit();
        }
    }
}