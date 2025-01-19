using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject[] bubblePrefabs; // 气泡预制体数组
    public float spawnInterval = 1f; // 生成气泡的时间间隔
    public float minSpeed = 1f; // 气泡最小移动速度
    public float maxSpeed = 3f; // 气泡最大移动速度

    private Camera mainCamera;
    public AudioClip destroySound;
    public Image MouseUI;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(StartSpawningAfterDelay(1f)); // 10秒后开始生成气泡
    }

    IEnumerator StartSpawningAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 等待10秒
        StartCoroutine(SpawnBubbles()); // 开始生成气泡
    }

    IEnumerator SpawnBubbles()
    {
        while (true)
        {
            SpawnBubble();
            yield return new WaitForSeconds(spawnInterval); // 每隔一段时间生成一个气泡
        }
    }

    
    void SpawnBubble()
    {    if (bubblePrefabs == null || bubblePrefabs.Length == 0)
        {
            Debug.LogError("bubblePrefabs 数组为空！无法生成气泡。");
            return;
        }
        // 随机选择气泡种类
        GameObject bubblePrefab = bubblePrefabs[Random.Range(0, bubblePrefabs.Length)];

        // 随机选择生成位置（屏幕左侧或右侧）
        Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject bubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);

        // 设置气泡的移动方向和速度
        Vector2 moveDirection = GetMoveDirection(spawnPosition);
        float speed = Random.Range(minSpeed, maxSpeed);
        bubble.GetComponent<Rigidbody2D>().velocity = moveDirection * speed;

        // 添加点击事件
        bubble.AddComponent<BubbleClickHandler>();
    }

    Vector2 GetRandomSpawnPosition()
    {   
        float screenHeight = 2f * mainCamera.orthographicSize;
        float screenWidth = screenHeight * mainCamera.aspect;

        // 随机选择左侧或右侧
        float x = Random.Range(0, 2) == 0 ? -screenWidth / 3 : screenWidth / 3;
        float y = Random.Range(-screenHeight / 5, screenHeight / 5);

        return new Vector2(x, y);
    }

    Vector2 GetMoveDirection(Vector2 spawnPosition)
    {
        // 如果气泡从左侧生成，则向右移动；如果从右侧生成，则向左移动
        return spawnPosition.x < 0 ? Vector2.right : Vector2.left;
    }
}