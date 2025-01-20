using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;
    public bool hasStart = false;

    void Start()
    {
        beatTempo = beatTempo / 43f;
    }

    void Update()
    {
        if (!hasStart)
        {
            if (Input.anyKey)
             {
                 hasStart = true;
             }
        }
        else
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }
}
