using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutSoundController : MonoBehaviour
{
    public AudioSource audioSource; // 用于播放音效的AudioSource组件
    public AudioClip hitSound; // 击中音符时播放的音效

    // 播放音效的方法
    public void PlayHitSound()
    {
        if (audioSource != null && hitSound != null)
        {   
            Debug.Log("Playing hit sound: " + hitSound.name); // 输出音效名称
            audioSource.PlayOneShot(hitSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or HitSound is not assigned in cutSoundController.");
        }
    }
}