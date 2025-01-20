using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float speed;
    void Update()
    {
        transform.position -= new Vector3(0f, speed * Time.deltaTime, 0f);
    }
}
