using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MaintoStartBtn : MonoBehaviour
{
    public void OnExitHandler()
    {
        SceneManager.LoadScene("StartScene");
    } 

}
