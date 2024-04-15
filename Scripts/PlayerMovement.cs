using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
    float speed = 4.5f;

    bool isJump; //스크립트 어디에 넣지 ..
    bool isAttack;
    public bool isAttacked;
    public bool touchSled = false;

    Vector3 moveVec;

    PlayerInput playerInput;
    Rigidbody rigid;
    public Animator anim;
    //GameObject player;

    GainedItemSpawner gainedItemSpawner;

    // Start is called before the first frame update
    void Start()
    {
        playerInput =GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        //player = GameObject.Find("Player");

        gainedItemSpawner = gameObject.GetComponent<GainedItemSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        
        Move();
        Turn();
        Attack();
        

        if (gameObject.transform.position.y < -5.5f)
        {
            gameObject.transform.position = new Vector3(0.19f, 0.18f, -0.038f);
        }
    }

    void Move()
    {
        moveVec = new Vector3(PlayerInput.hMove, 0, PlayerInput.vMove);
        gameObject.transform.position += moveVec* speed * Time.deltaTime;
        anim.SetBool("isRun", moveVec != Vector3.zero);
    }

    void OnCollisionEnter(Collision collision) //private
    {
        if (collision.gameObject.tag == "Sled")
        {
            touchSled = true;
            gainedItemSpawner.LoseItem();
            UIManager.instance.UpdateScoreText(GameManager.instance.CheckScore());
            SoundManager.instance.PlaySledSound();
        }

        if (collision.gameObject.tag == "Player" && !isAttack && !isAttacked)
        {
            anim.SetBool("isAttacked", true);
            anim.SetTrigger("Attacked");
            gainedItemSpawner.LoseItem();

            PlaeyrSpeedWait();

            isAttacked = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            anim.SetBool("isAttacked", false);
            isAttacked = false;
        }
        if (other.gameObject.tag == "Sled")
            touchSled = false;
    }

    //onDamage 
    void Attack()//ondamage 고려
    {
        if (PlayerInput.attack&&!isJump&&!isAttack)//점프하면서 공격- 동시에 눌러야지만 가능
        {
            anim.SetTrigger("doAttack");
            isAttack = true;
            StartCoroutine(waitForit());

            SoundManager.instance.PlayPunchSound();
        }
        

    }

    public bool isAttackedCheck()
    {
        return isAttacked;
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec); //직각 움직임
    }
    IEnumerator waitForit()
    {
        yield return new WaitForSeconds(3f);
        isAttack = false;
    }

    public IEnumerator PlaeyrSpeedWait()
    {
        speed = 0f;

        yield return new WaitForSeconds(2f);

        speed = 4.5f;
    }

}
