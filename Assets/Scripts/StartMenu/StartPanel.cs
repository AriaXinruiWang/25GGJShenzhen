using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject optionsPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPlayHandler()
    {
        SceneManager.LoadScene("Scene1");
    } 

    public void OnOptionsHandler()
    {
        optionsPanel.SetActive(true);
    }
}
