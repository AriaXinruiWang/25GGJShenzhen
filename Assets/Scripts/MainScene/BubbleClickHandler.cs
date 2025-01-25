using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleClickHandler : MonoBehaviour
{
    public AudioClip destroySound; // 销毁音效
    private bool isStopped = false;
    private Rigidbody2D rb; 
    public Sprite pressedImage; // 按下时的图片
    private SpriteRenderer SR;
    
    void Start()
    {
        // 获取 Rigidbody2D 组件
        rb = GetComponent<Rigidbody2D>();

        // 如果没有 Rigidbody2D 组件，输出错误日志
        if (rb == null)
        {
            Debug.Log("Rigidbody2D 组件未找到！请确保泡泡预制体上有 Rigidbody2D 组件。");
        }

        // 获取 SpriteRenderer 组件
        SR = GetComponent<SpriteRenderer>();

        // 如果没有 SpriteRenderer 组件，输出错误日志
        if (SR == null)
        {
            Debug.LogError("SpriteRenderer 组件未找到！请确保物体上有 SpriteRenderer 组件。");
        }
    }

    void OnMouseDown()
    {   
        Debug.Log("handler的OnMouseDown检测");
        // 播放音效
        
        if (destroySound != null)
        {   
            Debug.Log("音效已赋值，准备播放。");
            AudioSource.PlayClipAtPoint(destroySound, transform.position);
            Debug.Log("bubble破音效播放。");
        }

        else
        {
            Debug.Log("音效无赋值");
        }
            
        // 当鼠标点击时，切换为按下时的图片
        if ( pressedImage != null)
        {
            SR.sprite = pressedImage;
             Debug.Log("换图片泡泡");
            
        }
        else
        {
            Debug.Log("未赋值换图片泡泡");
        }
        // 销毁泡泡延迟
        Destroy(gameObject,0.5f);

         
    }

    void Update()
    {
        
    }

    bool IsOffScreen()
    {
        Vector2 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
        return screenPosition.x < 0 || screenPosition.x > 1 || screenPosition.y < 0 || screenPosition.y > 1;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果泡泡碰到屏幕边缘，停止移动
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // 停止移动
        }

         if (collision.gameObject.CompareTag("BarCollider") && !isStopped)
        {
        // 停止运动
        rb.velocity = Vector2.zero;

        // 启用重力，让泡泡下落
        rb.gravityScale = 1f;

        // 标记泡泡已停止
        isStopped = true;
        
        Debug.Log("泡泡碰到长条碰撞体，停止运动并开始下落");
        }
    }
        
}

