using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BombPresent : MonoBehaviourPun, IItem 
{ 
    GainedItemSpawner gainedItemSpawner;
    public ParticleSystem boomEffectPrefab;
    

    bool touch;
    PlayerMovement player;

    Vector3 hitPoint;
    Vector3 hitNormal;

    // Start is called before the first frame update
    void Start()
    {
        gainedItemSpawner = FindObjectOfType<GainedItemSpawner>();
        player = GameObject.Find("Player(Clone)").GetComponent< PlayerMovement>();

    }

    // Update is called once per frame
    public void Use()
    {
        SoundManager.instance.PlayBoomSound();
        PlayBoomEffect(hitPoint, hitNormal);
        
    }

    [PunRPC]
    public void PlayBoomEffect(Vector3 pos, Vector3 normal)
    {
        boomEffectPrefab.transform.position = pos;
        boomEffectPrefab.transform.rotation = Quaternion.LookRotation(normal);

        boomEffectPrefab.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && gainedItemSpawner.gainedItemCount < gainedItemSpawner.gainedItemMax)
        {
            touch = true;



            hitPoint = other.ClosestPoint(transform.position);
            hitNormal = transform.position - other.transform.position;

            Use();
            player.PlaeyrSpeedWait();
            
            gainedItemSpawner.LoseItem(); // 플레이어와 넘어졌을 때처럼 실행
            PhotonNetwork.Destroy(gameObject);
            Instantiate(boomEffectPrefab);
            player.anim.SetBool("isAttacked", true); ;
            player.anim.SetTrigger("Attacked"); //넘어지게
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
  
            player.anim.SetBool("isAttacked", false);
        }
    }
}
