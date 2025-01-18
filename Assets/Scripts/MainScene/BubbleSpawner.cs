using UnityEngine;
using System.Collections;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab; // 气泡预制体
    public float spawnInterval = 1f; // 生成间隔
    public float bubbleSpeed = 2f; // 气泡移动速度
    public float spawnHeightRange = 2f; // 生成高度范围
    public float bubbleLifetime = 5f; // 气泡存在时间（避免无限移动）

    private void Start()
    {
        StartCoroutine(SpawnBubbles());
    }

    IEnumerator SpawnBubbles()
    {
        while (true)
        {
            // 随机选择左边或右边生成
            bool spawnFromLeft = Random.Range(0, 2) == 0;

            // 计算生成位置
            float spawnY = Random.Range(-spawnHeightRange, spawnHeightRange);
            Vector3 spawnPosition = spawnFromLeft ? 
                new Vector3(-10f, spawnY, 0f) : 
                new Vector3(10f, spawnY, 0f);

            // 生成气泡
            GameObject bubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);

            // 设置气泡的移动方向
            Vector3 moveDirection = spawnFromLeft ? Vector3.right : Vector3.left;
            StartCoroutine(MoveBubble(bubble, moveDirection));

            // 设置气泡的自动销毁时间
            Destroy(bubble, bubbleLifetime);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator MoveBubble(GameObject bubble, Vector3 direction)
    {
        while (bubble != null)
        {
            bubble.transform.Translate(direction * bubbleSpeed * Time.deltaTime);

            // 如果气泡移出屏幕，销毁它
            if (Mathf.Abs(bubble.transform.position.x) > 10f)
            {
                Destroy(bubble);
            }

            yield return null;
        }
    }

    private void Update()
    {
        // 检测鼠标点击
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Bubble"))
            {
                // 获取气泡的 Animator 组件
                Animator bubbleAnimator = hit.collider.GetComponent<Animator>();

                if (bubbleAnimator != null)
                {
                    // 播放气泡破裂动画
                    bubbleAnimator.SetTrigger("Pop");

                    // 在动画播放完成后销毁气泡
                    StartCoroutine(DestroyAfterAnimation(hit.collider.gameObject, bubbleAnimator));
                }
                else
                {
                    // 如果没有 Animator，直接销毁气泡
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    IEnumerator DestroyAfterAnimation(GameObject bubble, Animator animator)
    {
        // 等待动画播放完成
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // 销毁气泡
        Destroy(bubble);
    }
}