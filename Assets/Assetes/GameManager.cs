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
    public int maxRemainBubbles = 10;//设置允许的最大泡泡数量   

    
    private bool emergencyStop = false;
    public GameObject GoodResultScreen;
    public GameObject BadResultScreen;
    public Text mouthsCounter, perfectCounter, missedCounter;

    void Start()
    {
       instance = this;
       multiply = 1;

       //find how many mouths in the scene
       totalmouths = GameObject.FindGameObjectsWithTag("mouth").Length;
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

    public void NoteHit()
    {
        Debug.Log("Hit on time");

        Score += ScorePerNote * multiply;
         Debug.Log("Score: " + Score);

        multiplyTimes ++;
        multiply = baseNumber + multiplyTimes*0.1f;

        multiply = (float)Math.Round((decimal)multiply, 1);

        scoreText.text = "Score: " + Score;
    }

    public void NoteMiss()
    {
        Debug.Log("Miss");

        multiplyTimes = 0;
        multiply = 1;
         Debug.Log("multiply: " + multiply);
    }

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
}
