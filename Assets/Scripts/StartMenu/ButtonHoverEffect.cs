using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image backgroundImage; // 背景的 Image 组件
    public Sprite hoverBackground; // 鼠标进入时显示的背景图片
    public Sprite defaultBackground; // 鼠标离开时显示的背景图片

    private void Start()
    {
        // 设置默认背景图片
        if (backgroundImage != null && defaultBackground != null)
        {
            backgroundImage.sprite = defaultBackground;
        }
    }

    // 当鼠标进入按钮范围时调用
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (backgroundImage != null && hoverBackground != null)
        {
            backgroundImage.sprite = hoverBackground;
        }
    }

    // 当鼠标离开按钮范围时调用
    public void OnPointerExit(PointerEventData eventData)
    {
        if (backgroundImage != null && defaultBackground != null)
        {
            backgroundImage.sprite = defaultBackground;
        }
    }
}