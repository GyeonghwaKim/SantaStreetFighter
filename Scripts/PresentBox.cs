using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PresentBox : MonoBehaviourPun, IItem
{
    public int score = 0; //인스펙터 내부에서 점수 할당
    //static AudioSource audioSource;
    //public AudioClip getClip;

    GainedItemSpawner gainedItemSpawner;

    bool touch;

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        //gainedItemSpawner = FindObjectOfType<GainedItemSpawner>();

        touch = false;
    }


    public void Use()
    {
        GameManager.instance.AddScore(score);
        SoundManager.instance.PlayGetSound();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            touch = true;//
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gainedItemSpawner = other.GetComponent<GainedItemSpawner>();
            
            if(gainedItemSpawner.gainedItemCount < gainedItemSpawner.gainedItemMax)
            {
                gainedItemSpawner.gainedItemSpawn(gameObject);
                Use();

                Destroy(gameObject);
            }
        }
    }
}