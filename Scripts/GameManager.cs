using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;


public class GameManager : MonoBehaviourPun,IPunObservable
{
    //public GameObject gameoverText;
    public Text timeText;
    public Text scoreText;
    private float surviveTime = 64f; //SetActive Ȱ��ȭ ���η� 64�� ����

    public GameObject playerPrefab;
    

    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }
    private static GameManager m_instance;

    public int score = 0; //private -> public ����
    public bool isGameover { get; private set; }

    private void Awake()
    {
        if (instance != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    public void Start()
    {
        isGameover = false;

        Vector3 randomPos = Random.insideUnitSphere * 5f;
        randomPos.y = 0.18f;

        PhotonNetwork.Instantiate(playerPrefab.name, randomPos, Quaternion.identity);
    }

    public void AddScore(int newScore)
    {
        if (!isGameover)
        {
            score += newScore;
            UIManager.instance.UpdateScoreText(score);
            //���� �ʿ�
        }
    }

    public int CheckScore()
    {
        return score;
    }

    public float CheckTime()
    {
        return surviveTime;
    }

    public void EndGame()
    {
        isGameover = true;

        if (score >= 500)
        {
            SceneManager.LoadScene("Ending(Win)");
        }
        else if (score < 500)
        {
            SceneManager.LoadScene("Ending(Lose)");
        }
        //�Լ� ���� ���� �ؾ���
    }

    // Update is called once per frame
    void Update()
    {
        surviveTime -= Time.deltaTime;
        timeText.text = "Time: " + (int)surviveTime;


        if (surviveTime <= 0)
        {
            EndGame();
            timeText.text = "Time: 0";
            //SceneManager.LoadScene()
            //������ �ε�
        }



        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.LeaveRoom();
        }


    }
    /*public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Start");
    }*/

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(score);
        }
        /*else
        {
            score = (int)stream.ReceiveNext();
            UIManager.instance.UpdateScoreText(score);
        }*/
        //throw new System.NotImplementedException();
    }
}

