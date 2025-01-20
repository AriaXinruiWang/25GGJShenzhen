using UnityEngine;

public class DestroyOffscreen : MonoBehaviour
{
    private Camera mainCamera;
    private float screenWidth;
    private float screenHeight;

    void Start()
    {
        // 获取主摄像机
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera is not assigned!");
            return;
        }

        // 计算屏幕边界
        screenHeight = 2f * mainCamera.orthographicSize;
        screenWidth = screenHeight * mainCamera.aspect;
    }

    void Update()
    {
        // 检查物体是否超出屏幕
        if (IsOffscreen())
        {
            Destroy(gameObject); // 销毁物体
        }
    }

    // 检查物体是否超出屏幕
    bool IsOffscreen()
    {
        Vector2 position = transform.position;

        // 计算屏幕边界
        float leftBound = -screenWidth / 2;
        float rightBound = screenWidth / 2;
        float bottomBound = -screenHeight / 2;
        float topBound = screenHeight / 2;

        // 检查物体是否超出屏幕
        return position.x < leftBound || position.x > rightBound ||
               position.y < bottomBound || position.y > topBound;
    }
}