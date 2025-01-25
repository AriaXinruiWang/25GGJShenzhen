using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodSoundController : MonoBehaviour
{
    public AudioSource goodaudioSource; // 用于播放音效的AudioSource组件
    public AudioClip goodSound; // 击中音符时播放的音效

    // 播放音效的方法
    public void PlaygoodSound()
    {
        if (goodaudioSource != null && goodSound != null)
        {   
            Debug.Log("Playing good sound: " + goodSound.name); // 输出音效名称
            goodaudioSource.PlayOneShot(goodSound);
        }
        else
        {
            Debug.LogWarning("goodAudioSource or goodSound is not assigned in cutSoundController.");
        }
    }
}