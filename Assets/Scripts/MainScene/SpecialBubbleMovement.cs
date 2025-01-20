using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBubbleMovement : MonoBehaviour

{
    public Vector2 moveDirection; // 移动方向
    public float speed; // 移动速度
    public bool usePhysics = false; // 是否使用物理效果
    
    private Rigidbody2D rb;

    void Start()
    {
        if (usePhysics && TryGetComponent(out rb))
        {
            rb.velocity = moveDirection * speed;
        }
    }

    void Update()
    {
        if (!usePhysics)
        {
            // 每帧更新位置
            transform.Translate(moveDirection * speed * Time.deltaTime);
        }
    }
}