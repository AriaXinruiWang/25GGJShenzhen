using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    public GameObject optionsPrefab;
    private GameObject optionsPanel;
    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickOptionsButtonHandler()
    {
       if (!optionsPanel)
       {
        optionsPanel = Instantiate(optionsPrefab,new Vector3(0,0,0),Quaternion.identity);
        optionsPanel.transform.SetParent(parent.GetComponent<Transform>());
        optionsPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
       }
        optionsPanel.SetActive(true);
    }
}
