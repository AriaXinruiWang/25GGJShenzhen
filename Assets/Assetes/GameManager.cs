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
    public float PerfectScoreValue=7;

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

    private List<GameObject> mouthsList = new List<GameObject>(); // 用来存储嘴巴动画 Prefab 实例

    void Start()
    {
        instance = this;
        multiply = 1;

        // 使嘴巴动画初始时隐藏
        mouthsList.Clear(); // 清空列表
        foreach (GameObject mouth in mouthsList)
        {
            if (mouth != null)
                mouth.SetActive(false); // 隐藏已有的嘴巴动画
        }

        // 查找场景中所有的嘴巴动画
        totalmouths = GameObject.FindGameObjectsWithTag("mouth").Length;

        // 隐藏 Prefab（初始化时不显示）
        if (MouthChewAnimation != null)
        {
            MouthChewAnimation.SetActive(false); // 隐藏嘴巴动画
        }

        if (Bosseye != null)
        {
            Bosseye.SetActive(false); // 隐藏 Bosseye 动画
        }
    }

    void Update()
    {
        multiplyText.text = "x" + multiply;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            emergencyStop = !emergencyStop;
            if (emergencyStop)
            {
                myMusic.Pause();
                Time.timeScale = 0;
            }
            else
            {
                myMusic.UnPause();
                Time.timeScale = 1;
            }
        }

        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                myMusic.Play();
                BS.hasStart = true;
            }
        }
        else
        {
            int remainBubbles = GameObject.FindGameObjectsWithTag("bubble").Length;
            if (remainBubbles >= maxRemainBubbles)
            {
                BadResultScreen.SetActive(true);
                missedCounter.text = " " + missHits;
            }
            else if (!myMusic.isPlaying && !BadResultScreen.activeInHierarchy && !GoodResultScreen.activeInHierarchy && !emergencyStop)
            {
                if (remainBubbles < maxRemainBubbles)
                {
                    GoodResultScreen.SetActive(true);
                    mouthsCounter.text = " " + totalmouths;
                    perfectCounter.text = " " + perfectHits;
                }
                else
                {
                    BadResultScreen.SetActive(true);
                    missedCounter.text = " " + missHits;
                }
            }
        }
    }

    // 音符击中的处理
    public void NoteHit()
    {
        Debug.Log("Hit on time");

        Score += ScorePerNote * multiply;
        Debug.Log("Score: " + Score); // 输出分数变化

        multiplyTimes++;
        multiply = baseNumber + multiplyTimes * 0.1f;

        multiply = (float)Math.Round((decimal)multiply, 1);

        scoreText.text = "Score: " + Score;

        // Debug log 输出
        Debug.Log("Current Score: " + Score);

        // 每增加 10 分，生成一个新的嘴巴动画
        if (Score >= 10 * (totalmouths + 1))
        {
            TriggerMouthAnimation();
            totalmouths++;
        }
    }

    // 音符失误的处理
    public void NoteMiss()
    {
        Debug.Log("Miss");

        multiplyTimes = 0;
        multiply = 1;
        Debug.Log("multiply: " + multiply);

        // Miss时清空所有嘴巴动画
        ClearAllMouthAnimations();

        // 创建 Bosseye 动画
        TriggerBosseyeAnimation();
    }

    // 完美击中的处理
    public void PerfectHit()
    {
        Score += PerfectScoreValue * multiply;
        NoteHit();
        Debug.Log("Perfect Score: " + Score);
    }

    public void NormalHit()
    {
        Score += BaseScoreValue * multiply;
        NoteHit();
        Debug.Log("Normal Score: " + Score);
    }

    // 触发嘴巴动画
    private void TriggerMouthAnimation()
    {
        if (MouthChewAnimation != null && canvasRect != null)
        {
            // 创建嘴巴对象
            GameObject mouthInstance = Instantiate(MouthChewAnimation, canvasRect);

            // 设置嘴巴的标签，用于后续清理
            mouthInstance.tag = "mouth";

            // 随机生成 X 和 Y 坐标，但限制在 Canvas 内
            float randomX = UnityEngine.Random.Range(0, canvasRect.rect.width);
            // 限制 Y 坐标不能超过 -2.71
            float randomY = UnityEngine.Random.Range(-2.71f, Mathf.Min(400f, canvasRect.rect.height));

            // 设置嘴巴的位置
            RectTransform mouthRect = mouthInstance.GetComponent<RectTransform>();
            mouthRect.anchoredPosition = new Vector2(randomX, randomY);

            // 播放嘴巴动画
            Animator animator = mouthInstance.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("PlayAnimation"); // 触发嘴巴动画
            }

            // 将实例化的嘴巴动画添加到列表，后续用来清理
            mouthsList.Add(mouthInstance);
        }
    }

    // 触发 Bosseye 动画
    private void TriggerBosseyeAnimation()
    {
        if (Bosseye != null && canvasRect != null)
        {
            // 创建 Bosseye 对象
            GameObject bosseyeInstance = Instantiate(Bosseye, canvasRect);

            // 设置 Bosseye 的标签，用于后续清理
            bosseyeInstance.tag = "bosseye";

            // 随机生成 X 和 Y 坐标，但限制在 Canvas 内
            float randomX = UnityEngine.Random.Range(0, canvasRect.rect.width);
            // 限制 Y 坐标不能超过 -2.71
            float randomY = UnityEngine.Random.Range(-2.71f, Mathf.Min(400f, canvasRect.rect.height));

            // 设置 Bosseye 的位置
            RectTransform bosseyeRect = bosseyeInstance.GetComponent<RectTransform>();
            bosseyeRect.anchoredPosition = new Vector2(randomX, randomY);

            // 播放 Bosseye 动画
            Animator animator = bosseyeInstance.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("PlayAnimation"); // 触发 Bosseye 动画
            }
        }
    }

    // 清空所有嘴巴动画
    private void ClearAllMouthAnimations()
    {
        foreach (GameObject mouth in mouthsList)
        {
            if (mouth != null)
            {
                mouth.SetActive(false); // 隐藏嘴巴动画
                Destroy(mouth); // 销毁嘴巴对象
            }
        }
        mouthsList.Clear(); // 清空列表
        Score = 0; // 计分清零
        totalmouths = 0; // 重置嘴巴计数

        // Debug log 输出
        Debug.Log("All mouths cleared, score reset.");
    }
}
