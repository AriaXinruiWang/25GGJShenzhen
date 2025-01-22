using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    private List<GameObject> activeMouths = new List<GameObject>(); // 当前活动的嘴巴对象
    public AudioSource myMusic;
    public bool startPlaying;

    public BeatScroller BS;
    public static GameManager instance;

    public float Score;
    public float ScorePerNote = 3;

    public Text scoreText;
    public Text multiplyText;

    public float multiply;
    public int multiplyTimes;
    public int baseNumber = 1;

    public BosseyeController bosseyeController;
    public chopboardController ChopboardController;
    public mouthController mouth;
    


    [Header("ScoreValue Setting")]
    public float BaseScoreValue;
    public float PerfectScoreValue = 9;

    [Header("Result")]
    public float totalmouths;
    public float normalHits;
    public float perfectHits;
    public float missHits;
    public int maxRemainBubbles = 15; // 设置允许的最大泡泡数量   

    public GameObject mouthPrefab;
    public GameObject Bosseye; // Bosseye 动画 Prefab
    // public RectTransform canvasRect; // Canvas 的 RectTransform
    private bool emergencyStop = false;
    public GameObject GoodResultScreen;
    public GameObject BadResultScreen;
    public Text mouthsCounter, perfectCounter, missedCounter;
    public BubbleSpawner bubbleSpawner; // 声明 bubbleSpawner 变量
     void Awake()
    {
        // 动态查找 BosseyeController
        if (bosseyeController == null)
        {
            bosseyeController = FindObjectOfType<BosseyeController>();

            // 检查是否找到
            if (bosseyeController == null)
            {
                Debug.LogError("BosseyeController not found in the scene!");
            }
            else
            {
                Debug.Log("BosseyeController found: " + bosseyeController.name);
            }
        }

        // 动态查找 chopboardController
        if (ChopboardController== null )
        {
            ChopboardController = FindObjectOfType<chopboardController>();

            // 检查是否找到
            if (ChopboardController == null)
            {
                Debug.LogError("chopboardController not found in the scene!");
            }
            else
            {
                Debug.Log("chopboardController found: " + ChopboardController.name);
            }
        }
    }

    void Start()
    {
        Debug.Log("GameManager Start() called.");
        instance = this;
        multiply = 1;

        // 确保 bosseyeController 已赋值
        if (bosseyeController == null)
        {
            Debug.LogError("BosseyeController is not assigned in GameManager!");
        }

        // 确保 chopboardController 已赋值
        if (ChopboardController == null)
        {
            Debug.LogError("ChopboardController is not assigned in GameManager!");
        }
        else
        {
            // 初始化时隐藏 Chopboard
            ChopboardController.Hide();
        }

        // 确保 bubbleSpawner 已赋值
        if (bubbleSpawner == null)
        {
            Debug.LogError("BubbleSpawner is not assigned in GameManager!");
        }

        // // 使嘴巴动画初始时隐藏
        // mouthsList.Clear(); // 清空列表
        // Debug.Log("Mouths list cleared.");

        // foreach (GameObject mouth in mouthsList)
        // {
        //     if (mouth != null)
        //         mouth.SetActive(false); // 隐藏已有的嘴巴动画
        // }

        // // 查找场景中所有的嘴巴动画
        // totalmouths = GameObject.FindGameObjectsWithTag("mouth").Length;
        // Debug.Log($"Total mouths found in scene: {totalmouths}");

        // // 隐藏 Prefab（初始化时不显示）
        // if (MouthChewAnimation != null)
        // {
        //     MouthChewAnimation.SetActive(false); // 隐藏嘴巴动画
        //     Debug.Log("MouthChewAnimation prefab hidden.");
        // }

        // if (Bosseye != null)
        // {
        //     Bosseye.SetActive(false); // 隐藏 Bosseye 动画
        //     Debug.Log("Bosseye prefab hidden.");
        // }
    }

    void Update()
    {
        multiplyText.text = "x" + multiply;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed. Toggling emergency stop.");
            emergencyStop = !emergencyStop;
            if (emergencyStop)
            {
                myMusic.Pause();
                Time.timeScale = 0;
                Debug.Log("Game paused.");
            }
            else
            {
                myMusic.UnPause();
                Time.timeScale = 1;
                Debug.Log("Game unpaused.");
            }
        }

        if (!startPlaying)
        {
            // Debug.Log("Game not started yet.");
            // if (Input.GetKeyDown(KeyCode.Space))
            // {
            //     startPlaying = true;
            //     myMusic.Play();
            //     BS.hasStart = true;
            // }
        }
        else
        {
            Debug.Log("Game is playing.");
            int remainBubbles = GameObject.FindGameObjectsWithTag("bubble").Length +
                    GameObject.FindGameObjectsWithTag("bossbubble").Length;
            Debug.Log($"Remaining bubbles: {remainBubbles}");

            if (remainBubbles >= maxRemainBubbles)
            {
                Debug.Log("Max bubbles reached. Showing bad result screen.");
                BadResultScreen.SetActive(true);
                PauseGame(); // 暂停游戏
            }

            else if (!myMusic.isPlaying && !BadResultScreen.activeInHierarchy && !GoodResultScreen.activeInHierarchy && !emergencyStop)
            {
                Debug.Log("Music stopped. Checking game result.");
                if (remainBubbles < maxRemainBubbles)
                {
                    Debug.Log("Good result. Showing good result screen.");
                    GoodResultScreen.SetActive(true);
                    mouthsCounter.text = " " + totalmouths;
                    perfectCounter.text = " " + perfectHits;
                    PauseGame();
                }
                else
                {
                    Debug.Log("Bad result. Showing bad result screen.");
                    BadResultScreen.SetActive(true);
                    missedCounter.text = " " + missHits;
                    PauseGame();
                }
            }
        }
    }

    void PauseGame()
    {
        Debug.Log("PauseGame() called.");
        myMusic.Pause(); // 暂停音乐
        Time.timeScale = 0; // 暂停游戏
        emergencyStop = true; // 设置紧急停止标志
    }

    void UnpauseGame()
    {
        Debug.Log("UnpauseGame() called.");
        myMusic.UnPause(); // 恢复音乐
        Time.timeScale = 1; // 恢复游戏
        emergencyStop = false; // 清除紧急停止标志
    }

    // 音符击中的处理
    public void NoteHit()
    {
        Debug.Log("NoteHit() called.");
        Debug.Log("Hit on time");

         // 隐藏 Bosseye
        if (bosseyeController != null)
        {
            bosseyeController.Hide();
            Debug.Log("BosseyeController is hidden");
        }
        else
        {
            Debug.Log("BosseyeController is null!");
        }
        
        // 显示 切完消失动画
        if (ChopboardController != null)
        {
            ChopboardController.Show();
            Debug.Log("ChopboardController is showen");
        }
        else
        {
            Debug.Log("ChopboardController is null!");
        }

        

        Score += ScorePerNote * multiply;
        Debug.Log($"Score increased by {ScorePerNote * multiply}. New Score: {Score}");

        multiplyTimes++;
        multiply = baseNumber + multiplyTimes * 0.5f;
        multiply = (float)Math.Round((decimal)multiply, 1);
        Debug.Log($"Multiply updated. New Multiply: {multiply}");

        scoreText.text = "Score: " + Score;
        Debug.Log($"Score text updated to: {scoreText.text}");

        // 计算嘴巴生成数量
        int mouthCount = Mathf.FloorToInt(Score / 3) * 3; // score / 3 的倍数取整后乘以 3
        Debug.Log($"Calculated mouth count: {mouthCount}"); // 输出计算后的嘴巴数量
        if (mouthCount > 0)
        {   
            Debug.Log($"Spawning {mouthCount} mouths..."); // 输出生成嘴巴的数量
            mouth.SpawnMouth(mouthCount); // 生成嘴巴
        }
        else
        {
            Debug.Log("No mouths to spawn."); // 输出无需生成嘴巴
        }   
    }


    //     // 每增加 3 分，生成三个新的嘴巴动画
    //     if (Score >= 1 * (totalmouths + 3))
    //     {
    //         Debug.Log($"Score {Score} >= {3 * (totalmouths + 1)}. Triggering mouth animations.");
    //         for (int i = 0; i < 3; i++)
    //         {
    //             TriggerMouthAnimation();
    //         }
    //         totalmouths+=3; // 更新总嘴巴数量
    //         Debug.Log($"Total mouths updated to: {totalmouths}");
    //     }
    // }
    // 生成嘴巴
    // private void SpawnMouths(int count)
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


    // 音符失误的处理
    public void NoteMiss()
    {
        Debug.Log("NoteMiss() called.");
        Debug.Log("Miss");

        // 显示 Bosseye
        if (bosseyeController != null)
        {
            bosseyeController.Show();
        }
        else
        {
            Debug.Log("BosseyeController is null!");
        }

        multiplyTimes = 0;
        multiply = 1;
        Debug.Log($"Multiply reset to: {multiply}");

        // 隐藏ChopboardController
        if (ChopboardController != null)
        {
            ChopboardController.Hide();
        }
        else
        {
            Debug.Log("chopboardController is null!");
        }

        multiplyTimes = 0;
        multiply = 1;
        Debug.Log($"Multiply reset to: {multiply}");

        // 生成 bossBubble
        if (bubbleSpawner != null)
        {
            bubbleSpawner.SpawnBossBubbleOnMiss(); // 调用 BubbleSpawner 的方法
            Debug.Log("SpawnBossBubbleOnMiss called from BubbleSpawner."); // 输出调试信息
        }
        else
        {
            Debug.LogError("BubbleSpawner is not assigned!");
        }

        Score = 0;
        Debug.Log("Score reset to 0.");
         // 清除所有嘴巴
        foreach (var mouth in activeMouths)
        {
            Destroy(mouth); // 销毁嘴巴对象
        }
        activeMouths.Clear(); // 清空活动列表

        // 重置分数
        Score = 0;
        Debug.Log("Score reset to 0.");

        // // Miss时清空所有嘴巴动画
        // ClearAllMouthAnimations();

        // // 创建 Bosseye 动画
        // TriggerBosseyeAnimation();
    }

    // 完美击中的处理
    public void PerfectHit()
    {
        Debug.Log("PerfectHit() called.");
        Score += PerfectScoreValue * multiply;
        Debug.Log($"Perfect hit! Score increased by {PerfectScoreValue * multiply}. New Score: {Score}");
        NoteHit();

        // // 显示 切完消失动画
        // if (ChopboardController != null)
        // {
        //     ChopboardController.Show();
        //     Debug.Log("ChopboardController is hidden");
        // }
        // else
        // {
        //     Debug.Log("ChopboardController is null!");
        // }
    }

    public void NormalHit()
    {
        Debug.Log("NormalHit() called.");
        Score += BaseScoreValue * multiply;
        Debug.Log($"Normal hit! Score increased by {BaseScoreValue * multiply}. New Score: {Score}");
        NoteHit();

        // // 显示 切完消失动画
        // if (ChopboardController != null)
        // {
        //     ChopboardController.Show();
        //     Debug.Log("ChopboardController is hidden");
        // }
        // else
        // {
        //     Debug.Log("ChopboardController is null!");
        // }
    }
}

    // // 触发嘴巴动画
    // private void TriggerMouthAnimation()
    // {
    //     Debug.Log("TriggerMouthAnimation() called.");
    //     // if (MouthChewAnimation != null && canvasRect != null)
        
    //     if (MouthChewAnimation != null)
    //     {
    //         Debug.Log("MouthChewAnimation is valid.");
    //         // 创建嘴巴对象
    //         // GameObject mouthInstance = Instantiate(MouthChewAnimation, canvasRect);
    //         GameObject mouthInstance = Instantiate(MouthChewAnimation);
            
    //         Debug.Log("Mouth instance created.");

    //         // 设置嘴巴的标签，用于后续清理
    //         mouthInstance.tag = "mouth";
    //         Debug.Log("Mouth instance tagged as 'mouth'.");

    //         // // 随机生成 X 和 Y 坐标，但限制在 Canvas 内
    //         // float randomX = UnityEngine.Random.Range(0, canvasRect.rect.width);
    //         // float randomY = UnityEngine.Random.Range(-2.71f, Mathf.Min(-28f, canvasRect.rect.height));
            
    //         // 随机生成 X 和 Y 坐标,固定Y坐标
    //         float randomX = UnityEngine.Random.Range(7.76f, 7.86f); // X 坐标在 7.76 到 7.86 之间
    //         float randomY = -3.88f; // Y 坐标固定为 -3.88
    //         Debug.Log($"Mouth position set to: X={randomX}, Y={randomY}");

    //         // // 设置嘴巴的位置
    //         // RectTransform mouthRect = mouthInstance.GetComponent<RectTransform>();
    //         // mouthRect.anchoredPosition = new Vector2(randomX, randomY);
    //         // Debug.Log("Mouth position applied.");

    //         // // 设置嘴巴的位置
    //         // RectTransform mouthRect = mouthInstance.GetComponent<RectTransform>();
    //         // if (mouthRect != null)
    //         // {
    //         //     mouthRect.anchoredPosition = new Vector2(randomX, randomY); // 使用新的 randomX 和 randomY
    //         //     Debug.Log("Mouth position applied.");
    //         // }
    //         // else
    //         // {
    //         //     Debug.LogError("RectTransform component not found on mouth instance!");
    //         // }

    //         // 设置嘴巴的位置
    //         if (mouthInstance != null)
    //         {
    //             mouthInstance.transform.position = new Vector3(randomX, randomY, 0); // 设置 GameObject 的位置
    //             Debug.Log("Mouth position applied.");
    //         }
    //         else
    //         {
    //             Debug.LogError("Mouth instance is null!");
    //         }

    //         // // 播放嘴巴动画
    //         // Animator animator = mouthInstance.GetComponent<Animator>();
    //         // if (animator != null)
    //         // {
    //         //     animator.SetTrigger("Chew"); // 触发嘴巴动画
    //         //     Debug.Log("Mouth animation triggered.");
    //         // }

    //         // 将实例化的嘴巴动画添加到列表，后续用来清理
    //         mouthsList.Add(mouthInstance);
    //         Debug.Log("Mouth instance added to mouths list.");
    //     }
    //     else
    //     {
    //         Debug.LogError("MouthChewAnimation ");
    //     }
    // }

    // // 触发 Bosseye 动画
    // private void TriggerBosseyeAnimation()
    // {
    //     Debug.Log("TriggerBosseyeAnimation() called.");
    //     // if (Bosseye != null && canvasRect != null)
    //     if (Bosseye != null)
    //     {
    //         Debug.Log("Bosseye is valid.");
    //         // 创建 Bosseye 对象
    //         // GameObject bosseyeInstance = Instantiate(Bosseye, canvasRect);
    //         GameObject bosseyeInstance = Instantiate(Bosseye);
    //         Debug.Log("Bosseye instance created.");

    //         // 设置 Bosseye 的标签，用于后续清理
    //         bosseyeInstance.tag = "bosseye";
    //         Debug.Log("Bosseye instance tagged as 'bosseye'.");

    //         // // 随机生成 X 和 Y 坐标，但限制在 Canvas 内
    //         // float randomX = UnityEngine.Random.Range(0, canvasRect.rect.width);
    //         // float randomY = UnityEngine.Random.Range(-2.71f, Mathf.Min(400f, canvasRect.rect.height));
    //         // Debug.Log($"Bosseye position set to: X={randomX}, Y={randomY}");

    //         // 固定X Y坐标
    //         float fixX = 0f;
    //         float fixY = -4.02f; 
    //         Debug.Log($"Bosseye position set to: X={fixX}, Y={fixY}");

    //         // 设置 Bosseye 的位置
    //         if (bosseyeInstance != null)
    //         {
    //             bosseyeInstance.transform.position = new Vector3(fixX, fixY, 0); // 设置 GameObject 的位置
    //             Debug.Log("Bosseye position applied.");
    //         }
    //         else
    //         {
    //             Debug.LogError("Bosseye instance is null!");
    //         }

    //         // // 设置 Bosseye 的位置
    //         // RectTransform bosseyeRect = bosseyeInstance.GetComponent<RectTransform>();
    //         // bosseyeRect.anchoredPosition = new Vector2(randomX, randomY);
    //         // Debug.Log("Bosseye position applied.");

    //         // // 播放 Bosseye 动画
    //         // Animator animator = bosseyeInstance.GetComponent<Animator>();
    //         // if (animator != null)
    //         // {
    //         //     animator.SetTrigger("Bosseye"); // 触发 Bosseye 动画
    //         //     Debug.Log("Bosseye animation triggered.");
    //         // }
    //     }
    //     else
    //     {
    //         Debug.LogError("Bosseye is null!");
    //     }
    

//     // 清空所有嘴巴动画
//     private void ClearAllMouthAnimations()
//     {
//         Debug.Log("ClearAllMouthAnimations() called.");
//         foreach (GameObject mouth in mouthsList)
//         {
//             if (mouth != null)
//             {
//                 mouth.SetActive(false); // 隐藏嘴巴动画
//                 Destroy(mouth); // 销毁嘴巴对象
//                 Debug.Log("Mouth instance destroyed.");
//             }
//         }
//         mouthsList.Clear(); // 清空列表
//         Score = 0; // 计分清零
//         totalmouths = 0; // 重置嘴巴计数
//         Debug.Log("All mouths cleared, score reset.");
//     }
// }