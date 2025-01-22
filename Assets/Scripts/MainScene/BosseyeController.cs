using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosseyeController : MonoBehaviour
{
    private void Start()
    {
        // 初始隐藏 Bosseye
        gameObject.SetActive(false);
        Debug.Log("Bosseye initialized and hidden.");
    }

    // 显示 Bosseye
    public void Show()
    {
        gameObject.SetActive(true);
        Debug.Log("Bosseye shown.");
    }

    // 隐藏 Bosseye
    public void Hide()
    {
        gameObject.SetActive(false);
        Debug.Log("Bosseye hidden.");
    }
}

