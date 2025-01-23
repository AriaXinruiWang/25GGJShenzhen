using UnityEngine;

public class BossBubbleController : MonoBehaviour
{
    public GameObject bossBubblePrefab; // BossBubble 预制体
    public float minSpeed = 2f; // BossBubble 最小速度
    public float maxSpeed = 5f; // BossBubble 最大速度
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // 获取主摄像机
    }

    // 生成 BossBubble
    public void SpawnBossBubble()
    {
        if (bossBubblePrefab == null)
        {
            Debug.LogError("BossBubblePrefab is not assigned!");
            return;
        }

        // 生成 BossBubble
        GameObject bossBubble = Instantiate(bossBubblePrefab, GetRandomSpawnPosition(), Quaternion.identity);

        // 设置 BossBubble 的移动方向和速度
        SetBubbleMovement(bossBubble, GetMoveDirection(bossBubble.transform.position), Random.Range(minSpeed, maxSpeed));

        // 添加点击事件
        if (!bossBubble.TryGetComponent<BubbleClickHandler>(out _))
        {
            bossBubble.AddComponent<BubbleClickHandler>();
        }
    }

    // 设置气泡的移动逻辑
    void SetBubbleMovement(GameObject bubble, Vector2 moveDirection, float speed)
    {
        if (bubble.TryGetComponent(out Rigidbody2D rb))
        {
            rb.velocity = moveDirection * speed;
        }
        else
        {
            Debug.LogError("Bubble does not have Rigidbody2D component!");
        }
    }

    // 获取随机生成位置（屏幕范围内）
    Vector2 GetRandomSpawnPosition()
    {
        float screenHeight = 2f * mainCamera.orthographicSize;
        float screenWidth = screenHeight * mainCamera.aspect;

        // 随机生成位置
        float x = Random.Range(-screenWidth / 2, screenWidth / 2);
        float y = Random.Range(-screenHeight / 2, screenHeight / 2);

        return new Vector2(x, y);
    }

    // 获取移动方向
    Vector2 GetMoveDirection(Vector2 spawnPosition)
    {
        // 随机生成一个方向
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}