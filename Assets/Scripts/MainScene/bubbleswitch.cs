using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class bubbleswitch : MonoBehaviour
{
    private SpriteRenderer SR;
    public Sprite pressedImage; // 按下时的图片

    void Start()
    {
        // 获取 SpriteRenderer 组件
        SR = GetComponent<SpriteRenderer>();

        // 如果没有 SpriteRenderer 组件，输出错误日志
        if (SR == null)
        {
            Debug.LogError("SpriteRenderer 组件未找到！请确保物体上有 SpriteRenderer 组件。");
        }
    }

    public void OnMouseDown()
    {
        Debug.Log("鼠标点击泡泡时切换为按下时的图片");

        // 当鼠标点击时，切换为按下时的图片
        if (SR != null && pressedImage != null)
        {
            SR.sprite = pressedImage;
        }
    }
}