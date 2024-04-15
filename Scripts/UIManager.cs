using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }
            return m_instance;
        }
    }

    private static UIManager m_instance;

    public Text scoreText;

    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score: " + (int)newScore;
        if (newScore <= 0)
        {
            scoreText.text = "Score: 0";
        }
    }

}
