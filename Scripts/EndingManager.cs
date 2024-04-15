using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public void BackToMain()
    {
        SceneManager.LoadScene("Start");
    }
}
