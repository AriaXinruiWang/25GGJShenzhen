using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouthController : MonoBehaviour
{
    public GameObject mouthPrefab; // 嘴巴预制体

    // 初始化时隐藏嘴巴
    void Start()
    {
        Debug.Log("mouthController.Start() called.");
        //gameObject.SetActive(false);
        Debug.Log("Mouth is hidden on start.");
    }

    // 显示嘴巴
    public void Show(Vector2 position)
    {
        Debug.Log("mouthController.Show() called.");
        Debug.Log($"Setting position to: X={position.x}, Y={position.y}");

        transform.position = position;
        Debug.Log("Position set.");

        transform.localScale = Vector3.one; // 重置缩放
        Debug.Log("Scale reset to (1, 1, 1).");

        transform.rotation = Quaternion.identity; // 重置旋转
        Debug.Log("Rotation reset to identity.");

        gameObject.SetActive(true); // 激活对象
        Debug.Log("Mouth is shown.");
    }

    // 生成嘴巴
    public void SpawnMouth(int count)
    {
        if (mouthPrefab == null)
        {
            Debug.LogError("mouthPrefab is not assigned!");
            return;
        }

        for (int i = 0; i < count; i++)
        {

        // 随机生成 X 和 Y 坐标
        float randomX = UnityEngine.Random.Range(-7.76f, 7.86f); // X 坐标在 -7.76 到 7.86 之间
        float randomY = -3.88f; // Y 坐标固定为 -3.88
        Vector2 position = new Vector2(randomX, randomY);
        Debug.Log($"Generated position for mouth: X={randomX}, Y={randomY}"); // 输出生成的坐标

        // 实例化嘴巴对象
        GameObject mouth = Instantiate(mouthPrefab, position, Quaternion.identity);
        if (mouth == null)
        {
            Debug.LogError("Failed to instantiate mouth!");
            return;
        }
         GameManager.instance.activeMouths.Add(mouth);
            // 获取 mouthController 组件
            mouthController controller = mouth.GetComponent<mouthController>();
        if (controller != null)
        {
            controller.Show(position); // 显示嘴巴
        }
        else
        {
            Debug.LogError("mouthController component not found on mouthPrefab!");
        }

        Debug.Log($"嘴巴预制体是否激活======: {mouth.gameObject.activeSelf}");
      
    }
    // public GameObject[] mouthPrefabs; // 嘴巴预制体数组
    // // 初始化时隐藏嘴巴
    // void Start()
    // {
    //     gameObject.SetActive(false);
    // }

    //  private void SpawnMouths(int count)
    // {    Debug.Log($"Starting to spawn {count} mouths..."); // 输出开始生成嘴巴的数量

    //     for (int i = 0; i < count; i++)
    //     {
    //         // 随机生成 X 和 Y 坐标
    //         float randomX = UnityEngine.Random.Range(-7.76f, 7.86f); // X 坐标在 7.76 到 7.86 之间
    //         float randomY = -3.88f; // Y 坐标固定为 -3.88
    //         Vector2 position = new Vector2(randomX, randomY);
    //         Debug.Log($"Generated position for mouth {i + 1}: X={randomX}, Y={randomY}"); // 输出生成的坐标


    //         // 实例化嘴巴对象
    //         GameObject mouth = Instantiate(mouthPrefab, new Vector2(randomX, randomY), Quaternion.identity);
    //         mouth.GetComponent<mouthController>().Show(position); // 显示嘴巴
    //         activeMouths.Add(mouth); // 添加到活动列表
    //         Debug.Log($"Mouth{i+1} spawned at position: {position}");
    //     }

    // void SpawnMouths(int count)
    // {
    //     if (mouthPrefabs == null || mouthPrefabs.Length == 0)
    //     {
    //         return;
    //     }

    //     GameObject mouthPrefab = mouthPrefabs[UnityEngine.Random.Range(0, mouthPrefabs.Length)];

    // // 显示嘴巴
    // public void Show(Vector2 position)
    // {   
    //     transform.position = position;
    //     Debug.Log("Position set."); // 输出位置设置完成信息
    //     gameObject.SetActive(true);
    //     Debug.Log("Mouth is shown.");
    // }

    // // 隐藏嘴巴
    // public void Hide()
    // {
    //     gameObject.SetActive(false);
    //     Debug.Log("Mouth is hidden.");
    // }
 }
}