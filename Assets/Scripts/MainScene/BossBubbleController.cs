using UnityEngine;

public class BossBubbleController : MonoBehaviour
{
    public GameObject bossBubblePrefab; // BossBubble Ԥ����
    public float minSpeed = 2f; // BossBubble ��С�ٶ�
    public float maxSpeed = 5f; // BossBubble ����ٶ�
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // ��ȡ�������
    }

    // ���� BossBubble
    public void SpawnBossBubble()
    {
        if (bossBubblePrefab == null)
        {
            Debug.LogError("BossBubblePrefab is not assigned!");
            return;
        }

        // ���� BossBubble
        GameObject bossBubble = Instantiate(bossBubblePrefab, GetRandomSpawnPosition(), Quaternion.identity);

        // ���� BossBubble ���ƶ�������ٶ�
        SetBubbleMovement(bossBubble, GetMoveDirection(bossBubble.transform.position), Random.Range(minSpeed, maxSpeed));

        // ��ӵ���¼�
        if (!bossBubble.TryGetComponent<BubbleClickHandler>(out _))
        {
            bossBubble.AddComponent<BubbleClickHandler>();
        }
    }

    // �������ݵ��ƶ��߼�
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

    // ��ȡ�������λ�ã���Ļ��Χ�ڣ�
    Vector2 GetRandomSpawnPosition()
    {
        float screenHeight = 2f * mainCamera.orthographicSize;
        float screenWidth = screenHeight * mainCamera.aspect;

        // �������λ��
        float x = Random.Range(-screenWidth / 2, screenWidth / 2);
        float y = Random.Range(-screenHeight / 2, screenHeight / 2);

        return new Vector2(x, y);
    }

    // ��ȡ�ƶ�����
    Vector2 GetMoveDirection(Vector2 spawnPosition)
    {
        // �������һ������
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}