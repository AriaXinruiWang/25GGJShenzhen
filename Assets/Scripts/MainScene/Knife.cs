using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife: MonoBehaviour
{
    private SpriteRenderer SR;
    public Sprite defaltImage;
    public Sprite pressedlmage;
    public KeyCode keyToPress;

    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    } 
    
    void Update()
    {
        if(Input.GetKeyDown(keyToPress))
        {
            SR.sprite = pressedlmage;
        }
        if(Input.GetKeyUp(keyToPress))
        {
            SR.sprite = defaltImage;
        }
    }
}
    
