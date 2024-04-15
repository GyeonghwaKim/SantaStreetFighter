using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setActive : MonoBehaviour
{
    // private bool state;

    public Text StartCountdown;
    public static float Countdown = 4f;
    public bool check = true;

    private GameObject itemSpawner;
    private GameObject playerMovement;
    private GameObject playerInput;

    // Start is called before the first frame update
    void Start()
    {
        //state = true;
        itemSpawner = GameObject.Find("Item Spawner");
        playerMovement = GameObject.Find("Player(Clone)");
        playerInput = GameObject.Find("Player(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        UIManager.instance.scoreText.gameObject.SetActive(false);
        GameManager.instance.timeText.gameObject.SetActive(false);
        itemSpawner.SetActive(false);
        playerMovement.GetComponent<PlayerMovement>().enabled = false;
        playerInput.GetComponent<PlayerInput>().enabled = false;

        Countdown -= Time.deltaTime;
        StartCountdown.text = " " + (int)Countdown;

        if (Countdown <= 1)
        {
            StartCountdown.text = "Go!";
            //Invoke("Update", 2f);
            //
            StartCoroutine(WaitForIt());

            UIManager.instance.scoreText.gameObject.SetActive(true);
            GameManager.instance.timeText.gameObject.SetActive(true);
            itemSpawner.SetActive(true);
            playerMovement.GetComponent<PlayerMovement>().enabled = true;
            playerInput.GetComponent<PlayerInput>().enabled = true;
        }
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(1f);
        check = true;
        gameObject.SetActive(false);
    }
}
