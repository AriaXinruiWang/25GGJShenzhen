using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chopboardController : MonoBehaviour
{
    // 用于控制 Chopboard 的显示和隐藏
    public GameObject chopboardObject;
    private Animation animationComponent;

    void Start()
    {
    //     // 初始化时隐藏 Chopboard
    //     if (chopboardObject != null)
    //     {
    //         chopboardObject.SetActive(false);
    //     }
    //     else
    //     {
    //         Debug.LogError("Chopboard object is not assigned!");
    //     }

    //     // 获取 Animation 组件
    //     animationComponent = GetComponent<Animation>();

    //     if (animationComponent == null)
    //     {
    //         Debug.LogError("Animation component is missing!");
    //     }

    //     // 确保动画片段已赋值
    //     if (disappearAnimation == null)
    //     {
    //         Debug.LogError("Disappear animation clip is not assigned!");
    //     }
    // }
    }
    // 显示 Chopboard
    public void Show()
    {
        if (chopboardObject != null)
        {
            chopboardObject.SetActive(true);
            Debug.Log("Chopboard is shown.");
        }
    }

    // 隐藏 Chopboard
    public void Hide()
    {
        if (chopboardObject != null)
        {
            chopboardObject.SetActive(false);
            Debug.Log("Chopboard is hidden.");
        }
    }

    // // 显示对象并播放动画
    // public void ShowAndPlayAnimation()
    // {
    //     if (animationComponent != null && disappearAnimation != null)
    //     {
    //         // 确保对象激活
    //         gameObject.SetActive(true);

    //         // 播放动画
    //         animationComponent.Play(disappearAnimation.name);

    //         // 在动画播放完后隐藏对象
    //         StartCoroutine(DisableAfterAnimation(disappearAnimation.length));
    //     }
    //     else
    //     {
    //         Debug.LogError("Animation component or clip is not assigned!");
    //     }
    // }

    // // 协程：在动画播放完后隐藏对象
    // private System.Collections.IEnumerator DisableAfterAnimation(float animationLength)
    // {
    //     // 等待动画播放完成
    //     yield return new WaitForSeconds(animationLength);

    //     // 隐藏对象
    //     gameObject.SetActive(false);
    //     Debug.Log("Chopboard is hidden.");
    // }
}