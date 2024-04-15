using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    GameObject explain;
    // Start is called before the first frame update
    void Start()
    {
        explain = GameObject.Find("Explain");
        explain.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name =="Start" && Input.anyKey)
        {
            explain.SetActive(true);
        }
    }
   
}
