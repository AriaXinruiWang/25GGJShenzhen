using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressedImageLayer : MonoBehaviour
{
    // 需要控制的PressedImage
    public Image pressedImage;

    // 默认的Sorting Layer和Order in Layer
    private string defaultSortingLayer;
    private int defaultOrderInLayer;

    void Start()
    {
        if (pressedImage == null)
        {
            Debug.LogError("PressedImage is not assigned!");
            return;
        }

        // 获取PressedImage的Canvas组件
        Canvas canvas = pressedImage.GetComponent<Canvas>();
        if (canvas == null)
        {
            // 如果没有Canvas组件，则动态添加一个
            canvas = pressedImage.gameObject.AddComponent<Canvas>();
        }

        // 启用Canvas的Override Sorting
        canvas.overrideSorting = true;

        // 保存默认的Sorting Layer和Order in Layer
        defaultSortingLayer = canvas.sortingLayerName;
        defaultOrderInLayer = canvas.sortingOrder;

        // 初始时隐藏PressedImage
        pressedImage.gameObject.SetActive(false);
    }

    void Update()
    {
        // 检测按下空格键
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 显示PressedImage并设置到第1层
            ShowPressedImage();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            // 隐藏PressedImage并恢复默认层级
            HidePressedImage();
        }
    }

    void ShowPressedImage()
    {
        if (pressedImage != null)
        {
            // 显示PressedImage
            pressedImage.gameObject.SetActive(true);

            // 获取Canvas组件
            Canvas canvas = pressedImage.GetComponent<Canvas>();
            if (canvas != null)
            {
                // 设置Sorting Layer为UI（或其他自定义层）
                canvas.sortingLayerName = "UI";
                // 设置Order in Layer为1（确保在最上层）
                canvas.sortingOrder = 1;
            }
        }
    }

    void HidePressedImage()
    {
        if (pressedImage != null)
        {
            // 隐藏PressedImage
            pressedImage.gameObject.SetActive(false);

            // 获取Canvas组件
            Canvas canvas = pressedImage.GetComponent<Canvas>();
            if (canvas != null)
            {
                // 恢复默认的Sorting Layer和Order in Layer
                canvas.sortingLayerName = defaultSortingLayer;
                canvas.sortingOrder = defaultOrderInLayer;
            }
        }
    }
}