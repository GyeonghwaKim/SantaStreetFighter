using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInput : MonoBehaviourPun
{
    public string hAxis = "Horizontal";//���� Ű
    public string vAxis = "Vertical";//�յڷ� �����̴� ���Ʒ� Ű 
    public string jumpButtonName = "Jump";//�����ϴ� �����̽���
    public string attackButtonName = "Attack"; //������ zŰ 

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
        attack = Input.GetButtonDown(attackButtonName); //�� ������ ��� ����?���� �ִϸ��̼� �ʿ� ..? �� 

    }


}
