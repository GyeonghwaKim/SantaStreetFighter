using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInput : MonoBehaviourPun
{
    public string hAxis = "Horizontal";//오왼 키
    public string vAxis = "Vertical";//앞뒤로 움직이는 위아래 키 
    public string jumpButtonName = "Jump";//점프하는 스페이스바
    public string attackButtonName = "Attack"; //때리는 z키 

    public static float hMove;
    public static float vMove;
    public static bool jump;
    public static bool attack;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        hMove = Input.GetAxisRaw(hAxis);
        vMove = Input.GetAxisRaw(vAxis);
        jump = Input.GetButtonDown(jumpButtonName);
        attack = Input.GetButtonDown(attackButtonName); //꾹 누르면 계속 공격?막는 애니메이션 필요 ..? 흠 

    }


}
