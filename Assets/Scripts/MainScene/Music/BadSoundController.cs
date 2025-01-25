using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadSoundController : MonoBehaviour
{
    public AudioSource badaudioSource; // 用于播放音效的AudioSource组件
    public AudioClip badSound; // 击中音符时播放的音效

    // 播放音效的方法
    public void PlayBadSound()
    {
        if (badaudioSource != null && badSound != null)
        {   
            Debug.Log("Playing bad sound: " + badSound.name); // 输出音效名称
            badaudioSource.PlayOneShot(badSound);
        }
        else
        {
            Debug.LogWarning("badAudioSource or badSound is not assigned in cutSoundController.");
        }
    }
}