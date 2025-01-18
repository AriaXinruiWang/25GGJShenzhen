using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLine : MonoBehaviour
{
    private bool canBePress = false;

    public KeyCode keyToPress;
    public float perfectAdjust;

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePress)
            {
               gameObject.SetActive(false);
               GameManager.instance.NoteHit();

               if (transform.position.y >= -2.92 - perfectAdjust && transform.position.y <= -2.92 + perfectAdjust)
               {
                Debug.Log("Perfect");
                GameManager.instance.PerfectHit();
               }
               else
               {
                Debug.Log("Normal");
                GameManager.instance.NormalHit();
               }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePress = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePress = false;
            GameManager.instance.NoteMiss();
        }
    }
}
