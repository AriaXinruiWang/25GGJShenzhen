using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAudio : MonoBehaviour
{
    public AudioClip bubbleSound; // 用于存储音频剪辑
    private AudioSource audioSource; // 用于播放音频的AudioSource组件

    void Start()
    {
        // 确保物体有一个AudioSource组件
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 将音频剪辑赋值给AudioSource
        audioSource.clip = bubbleSound;
    }

    void OnMouseDown()
    {
        // 检查物体是否带有“Bubble”标签
        if (gameObject.CompareTag("bubble"))
        {
            // 播放音频
            if (bubbleSound != null)
            {
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("No audio clip assigned to BubbleAudio script.");
            }
        }
    }
}