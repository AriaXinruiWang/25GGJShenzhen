using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpaceUIController : MonoBehaviour
{
    // 需要控制的UI对象
    public GameObject uiObject;

    void Start()
    {
        // 游戏开始时显示UI
        if (uiObject != null)
        {
            uiObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("UI Object is not assigned in the inspector!");
        }
    }

    void Update()
    {
        // 检测玩家是否按下空格键
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 切换UI的显示状态
            if (uiObject != null)
            {
                uiObject.SetActive(!uiObject.activeSelf);
            }
        }
    }
}