using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerate : MonoBehaviour
{
    public Sprite redLineSprite;
    public GameObject parent;

    public KeyCode keyToPress;
    public float perfectAdjust;  // 可以设置调整的值

    private string timeFileName = "beats_ts";  // 文本文件名
    private List<float> timePoints = new List<float>();  // 存储从文本文件读取的时间点

    private float timeElapsed = 0f;
    private int lineCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        ReadTimePointsFromFile();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timePoints.Count > 0 && timeElapsed >= timePoints[0])
        {
            GenerateLine();
            timePoints.RemoveAt(0);
        }
    }

    void GenerateLine()
    {
        float screenHeight = Camera.main.orthographicSize * 2;  // 计算屏幕的高度
        float spriteHeight = redLineSprite.bounds.size.y;  // 获取 Sprite 的高度

        // 创建一个新的对象来存放虚线
        GameObject lineObject = new GameObject("RedLine_" + lineCount);
        lineObject.transform.SetParent(parent.transform);  // 设置父对象

        // 添加 SpriteRenderer 组件来显示虚线
        SpriteRenderer spriteRenderer = lineObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = redLineSprite;
        spriteRenderer.sortingOrder = 1;  // 设置渲染顺序，确保它显示在其他物体上面

        // 设置虚线的尺寸和位置
        lineObject.transform.position = new Vector3(0, screenHeight + spriteHeight / 2, 0);  // 设置每条线的垂直位置，确保它完全在屏幕外

        BoxCollider2D collider = lineObject.AddComponent<BoxCollider2D>();  // 添加碰撞器

        DropLine dropLineScript = lineObject.AddComponent<DropLine>();  // 添加 DropLine 脚本
        dropLineScript.keyToPress = keyToPress;  // 设置按下的键
        dropLineScript.perfectAdjust = perfectAdjust;  // 设置完美调整值

        lineCount++;
    }

    void ReadTimePointsFromFile()
    {
        TextAsset timeFile = Resources.Load<TextAsset>(timeFileName);
        if (timeFile != null)
        {
            string[] lines = timeFile.text.Split('\n');
            foreach (string line in lines)
            {
                if (float.TryParse(line, out float timePoint))
                {
                    timePoints.Add(timePoint);
                }
            }
        }
        else
        {
            Debug.LogError("Time file not found: " + timeFileName);
        }
    }
}
