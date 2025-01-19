using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleClickHandler : MonoBehaviour
{
    public AudioClip destroySound; // 销毁音效
    private bool isStopped = false;
    private Rigidbody2D rb; 

    void Start()
    {
        // 获取 Rigidbody2D 组件
        rb = GetComponent<Rigidbody2D>();

        // 如果没有 Rigidbody2D 组件，输出错误日志
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D 组件未找到！请确保泡泡预制体上有 Rigidbody2D 组件。");
        }
    }

    void OnMouseDown()
    {
        // 播放音效
        if (destroySound != null)
        {
            AudioSource.PlayClipAtPoint(destroySound, transform.position);
        }

        // 销毁泡泡
        Destroy(gameObject);
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

