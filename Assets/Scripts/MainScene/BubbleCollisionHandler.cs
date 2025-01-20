using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BubbleCollisionHandler : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查碰撞对象是否带有 "BarCollider" 标签
        if (collision.gameObject.CompareTag("BarCollider"))
        {
            // 获取当前物体的 Rigidbody2D 组件
            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            // 如果 Rigidbody2D 组件存在，设置 mass 为 1
            if (rb != null)
            {
                rb.mass = 1f;
                Debug.Log("Mass set to 1 after collision with BarCollider.");
            }
            else
            {
                Debug.Log("No Rigidbody2D component found on this object.");
            }
        }
    }
}
