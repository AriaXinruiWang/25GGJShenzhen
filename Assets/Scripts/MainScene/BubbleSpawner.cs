using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BubbleSpawner : MonoBehaviour

{   
    public GameObject[] bubblePrefabs; // 气泡预制体数组
    public GameObject BossBubblePrefab; // BossBubble 预制体
    public float spawnInterval = 1f; // 生成气泡的时间间隔
    public float minSpeed = 1f; // 气泡最小移动速度
    public float maxSpeed = 3f; // 气泡最大移动速度
    // private float timeElapsed = 0.0f; // 游戏时间累计
    private float decreaseRate = 0.01f; // 生成间隔减少速率
    private float minSpawnInterval = 0.2f; // 最小生成间隔
    private float timeSinceLastSpawn = 0.0f; // 距离上次生成的时间、
    public event Action OnBubbleSpawningStarted; // 气泡生成开始事件

    private Camera mainCamera;
    public AudioClip destroySound;
    public Image MouseUI;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(StartSpawningAfterDelay(8f)); // 10秒后开始生成气泡
    }


    IEnumerator StartSpawningAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 等待 delay 秒
        StartCoroutine(SpawnBubbles()); // 开始生成气泡
        // 触发气泡生成开始事件
         OnBubbleSpawningStarted?.Invoke();
    }

    IEnumerator SpawnBubbles()
    {
        while (true)
        {
            SpawnBubble();
            yield return new WaitForSeconds(spawnInterval); // 每隔一段时间生成一个气泡 
        }
    }

    void Update()
    {
        // 更新距离上次生成的时间
        timeSinceLastSpawn += Time.deltaTime;

        // 逐渐减少生成间隔
        if (spawnInterval > minSpawnInterval)
        {
            spawnInterval -= decreaseRate * Time.deltaTime;
            spawnInterval = Mathf.Max(spawnInterval, minSpawnInterval); // 确保不低于最小值
        }

        // 检查是否到达生成时间
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnBubble();
            timeSinceLastSpawn = 0.0f; // 重置计时器
        }
    }

    // 生成普通气泡
    void SpawnBubble()
    {
        if (bubblePrefabs == null || bubblePrefabs.Length == 0)
        {
            return;
        }

        // 随机选择气泡种类
        GameObject bubblePrefab = bubblePrefabs[UnityEngine.Random.Range(0, bubblePrefabs.Length)];

        // 随机选择生成位置（屏幕左侧或右侧）
        Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject bubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);

        // 设置气泡的移动方向和速度
        SetBubbleMovement(bubble, GetMoveDirection(spawnPosition), UnityEngine.Random.Range(minSpeed, maxSpeed));

        // 设置初始 mass 为 0
        if (bubble.TryGetComponent(out Rigidbody2D rb))
        {
            rb.mass = 0f; // 初始 mass 为 0
            rb.gravityScale = 0f; // 初始重力为 0

            // BubbleCollisionHandler bubbleCollisionHandler = bubble.AddComponent<BubbleCollisionHandler>();
            // bubbleCollisionHandler.OnCollisionEnter2D+= HandleBubbleCollision;
        }


        // 添加点击事件
        if (!bubble.TryGetComponent<BubbleClickHandler>(out _))
        {
            bubble.AddComponent<BubbleClickHandler>();
        }
    }

    private void HandleBubbleCollision(GameObject bubblePrefab)
    {
        if (bubblePrefab.TryGetComponent(out Rigidbody2D rb) && rb.mass == 0f)
        {
            rb.mass = 1f;
            rb.gravityScale = 1f;

            // 当 mass 不为 0 时，添加 DestroyOffscreen 脚本
            if (!bubblePrefab.TryGetComponent<DestroyOffscreen>(out _))
            {
                bubblePrefab.AddComponent<DestroyOffscreen>();
            }
        }
    }

    // 生成 BossBubble
    public void SpawnBossBubbleOnMiss()
    {
        if (BossBubblePrefab == null)
        {
            Debug.Log("BossBubblePrefab is not assigned!");
            return;
        }

        // 生成 BossBubble
        GameObject bossBubble = Instantiate(BossBubblePrefab, GetRandomSpawnPosition(), Quaternion.identity);

        // // 设置 BossBubble 的 tag
        // bossBubble.tag = "BossBubble";

        // 设置 BossBubble 的移动方向和速度
        SetBubbleMovement(bossBubble, GetMoveDirection(bossBubble.transform.position),UnityEngine.Random.Range(minSpeed, maxSpeed));

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

    // 获取随机生成位置（屏幕左侧或右侧）
    Vector2 GetRandomSpawnPosition()
    {
        float screenHeight = 2f * mainCamera.orthographicSize;
        float screenWidth = screenHeight * mainCamera.aspect;

        float padding = 1f;

        // 随机选择左侧或右侧
        float x =UnityEngine. Random.Range(0, 2) == 0 ? -screenWidth / 5 : screenWidth /5;
        float y =UnityEngine. Random.Range(-screenHeight / 4, screenHeight / 3);

        float minx = -screenWidth / 5 + padding;
        float maxx = screenWidth / 5 - padding;
        float miny = -screenHeight / 4 + padding;
        float maxy = screenHeight / 3 - padding;


        return new Vector2(x, y);
    }

    // 获取移动方向
    Vector2 GetMoveDirection(Vector2 spawnPosition)
    {
        // 如果气泡从左侧生成，则向右移动；如果从右侧生成，则向左移动
        return spawnPosition.x < 0 ? Vector2.right : Vector2.left;
    }
}