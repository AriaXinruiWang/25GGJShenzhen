using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayBtn : MonoBehaviour
{
    public void startPlay()
    {
        SceneManager.LoadScene("Scene1");
    }
}
