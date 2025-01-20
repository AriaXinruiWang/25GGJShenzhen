using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
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

    [Header("ScoreValue Setting")]
    public float BaseScoreValue;
    public float PerfectScoreValue = 9;

    [Header("Result")]
    public float totalmouths;
    public float normalHits;
    public float perfectHits;
    public float missHits;
    public int maxRemainBubbles = 10; // 设置允许的最大泡泡数量   

    // 嘴巴动画 prefab 和 Canvas
    public GameObject MouthChewAnimation; // 嘴巴动画 Prefab
    public GameObject Bosseye; // Bosseye 动画 Prefab
    public RectTransform canvasRect; // Canvas 的 RectTransform

    private bool emergencyStop = false;
    public GameObject GoodResultScreen;
    public GameObject BadResultScreen;
    public Text mouthsCounter, perfectCounter, missedCounter;
    public BubbleSpawner bubbleSpawner; // 声明 bubbleSpawner 变量

    private List<GameObject> mouthsList = new List<GameObject>(); // 用来存储嘴巴动画 Prefab 实例

    void Start()
    {
        Debug.Log("GameManager Start() called.");
        instance = this;
        multiply = 1;

        // 使嘴巴动画初始时隐藏
        mouthsList.Clear(); // 清空列表
        Debug.Log("Mouths list cleared.");

        foreach (GameObject mouth in mouthsList)
        {
            if (mouth != null)
                mouth.SetActive(false); // 隐藏已有的嘴巴动画
        }

        // 查找场景中所有的嘴巴动画
        totalmouths = GameObject.FindGameObjectsWithTag("mouth").Length;
        Debug.Log($"Total mouths found in scene: {totalmouths}");

        // 隐藏 Prefab（初始化时不显示）
        if (MouthChewAnimation != null)
        {
            MouthChewAnimation.SetActive(false); // 隐藏嘴巴动画
            Debug.Log("MouthChewAnimation prefab hidden.");
        }

        if (Bosseye != null)
        {
            Bosseye.SetActive(false); // 隐藏 Bosseye 动画
            Debug.Log("Bosseye prefab hidden.");
        }
    }

    void Update()
    {
        Debug.Log("GameManager Update() called.");
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

        Score += ScorePerNote * multiply;
        Debug.Log($"Score increased by {ScorePerNote * multiply}. New Score: {Score}");

        multiplyTimes++;
        multiply = baseNumber + multiplyTimes * 0.1f;
        multiply = (float)Math.Round((decimal)multiply, 1);
        Debug.Log($"Multiply updated. New Multiply: {multiply}");

        scoreText.text = "Score: " + Score;
        Debug.Log($"Score text updated to: {scoreText.text}");

        // 每增加 3 分，生成三个新的嘴巴动画
        if (Score >= 3 * (totalmouths + 1))
        {
            Debug.Log($"Score {Score} >= {3 * (totalmouths + 1)}. Triggering mouth animations.");
            for (int i = 0; i < 3; i++)
            {
                TriggerMouthAnimation();
            }
            totalmouths++; // 更新总嘴巴数量
            Debug.Log($"Total mouths updated to: {totalmouths}");
        }
    }

    // 音符失误的处理
    public void NoteMiss()
    {
        Debug.Log("NoteMiss() called.");
        Debug.Log("Miss");

        multiplyTimes = 0;
        multiply = 1;
        Debug.Log($"Multiply reset to: {multiply}");

        // Miss时清空所有嘴巴动画
        ClearAllMouthAnimations();

        // 创建 Bosseye 动画
        TriggerBosseyeAnimation();
    }

    // 完美击中的处理
    public void PerfectHit()
    {
        Debug.Log("PerfectHit() called.");
        Score += PerfectScoreValue * multiply;
        Debug.Log($"Perfect hit! Score increased by {PerfectScoreValue * multiply}. New Score: {Score}");
        NoteHit();
    }

    public void NormalHit()
    {
        Debug.Log("NormalHit() called.");
        Score += BaseScoreValue * multiply;
        Debug.Log($"Normal hit! Score increased by {BaseScoreValue * multiply}. New Score: {Score}");
        NoteHit();
    }

    // 触发嘴巴动画
    private void TriggerMouthAnimation()
    {
        Debug.Log("TriggerMouthAnimation() called.");
        if (MouthChewAnimation != null && canvasRect != null)
        {
            Debug.Log("MouthChewAnimation and canvasRect are valid.");
            // 创建嘴巴对象
            GameObject mouthInstance = Instantiate(MouthChewAnimation, canvasRect);
            Debug.Log("Mouth instance created.");

            // 设置嘴巴的标签，用于后续清理
            mouthInstance.tag = "mouth";
            Debug.Log("Mouth instance tagged as 'mouth'.");

            // 随机生成 X 和 Y 坐标，但限制在 Canvas 内
            float randomX = UnityEngine.Random.Range(0, canvasRect.rect.width);
            float randomY = UnityEngine.Random.Range(-2.71f, Mathf.Min(400f, canvasRect.rect.height));
            Debug.Log($"Mouth position set to: X={randomX}, Y={randomY}");

            // 设置嘴巴的位置
            RectTransform mouthRect = mouthInstance.GetComponent<RectTransform>();
            mouthRect.anchoredPosition = new Vector2(randomX, randomY);
            Debug.Log("Mouth position applied.");

            // 播放嘴巴动画
            Animator animator = mouthInstance.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("PlayAnimation"); // 触发嘴巴动画
                Debug.Log("Mouth animation triggered.");
            }

            // 将实例化的嘴巴动画添加到列表，后续用来清理
            mouthsList.Add(mouthInstance);
            Debug.Log("Mouth instance added to mouths list.");
        }
        else
        {
            Debug.LogError("MouthChewAnimation or canvasRect is null!");
        }
    }

    // 触发 Bosseye 动画
    private void TriggerBosseyeAnimation()
    {
        Debug.Log("TriggerBosseyeAnimation() called.");
        if (Bosseye != null && canvasRect != null)
        {
            Debug.Log("Bosseye and canvasRect are valid.");
            // 创建 Bosseye 对象
            GameObject bosseyeInstance = Instantiate(Bosseye, canvasRect);
            Debug.Log("Bosseye instance created.");

            // 设置 Bosseye 的标签，用于后续清理
            bosseyeInstance.tag = "bosseye";
            Debug.Log("Bosseye instance tagged as 'bosseye'.");

            // 随机生成 X 和 Y 坐标，但限制在 Canvas 内
            float randomX = UnityEngine.Random.Range(0, canvasRect.rect.width);
            float randomY = UnityEngine.Random.Range(-2.71f, Mathf.Min(400f, canvasRect.rect.height));
            Debug.Log($"Bosseye position set to: X={randomX}, Y={randomY}");

            // 设置 Bosseye 的位置
            RectTransform bosseyeRect = bosseyeInstance.GetComponent<RectTransform>();
            bosseyeRect.anchoredPosition = new Vector2(randomX, randomY);
            Debug.Log("Bosseye position applied.");

            // 播放 Bosseye 动画
            Animator animator = bosseyeInstance.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("PlayAnimation"); // 触发 Bosseye 动画
                Debug.Log("Bosseye animation triggered.");
            }
        }
        else
        {
            Debug.LogError("Bosseye or canvasRect is null!");
        }
    }

    // 清空所有嘴巴动画
    private void ClearAllMouthAnimations()
    {
        Debug.Log("ClearAllMouthAnimations() called.");
        foreach (GameObject mouth in mouthsList)
        {
            if (mouth != null)
            {
                mouth.SetActive(false); // 隐藏嘴巴动画
                Destroy(mouth); // 销毁嘴巴对象
                Debug.Log("Mouth instance destroyed.");
            }
        }
        mouthsList.Clear(); // 清空列表
        Score = 0; // 计分清零
        totalmouths = 0; // 重置嘴巴计数
        Debug.Log("All mouths cleared, score reset.");
    }
}