using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";
    public Text connectionInfoText;
    public Button joinButton;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    private void Start()
    { 
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
        joinButton.interactable = false;
        connectionInfoText.text = "�� �� �غ� �ϴ���...";
    }
    public void JoinRandomOrCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.JoinRandomOrCreateRoom(expectedMaxPlayers: roomOptions.MaxPlayers);

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                PhotonNetwork.LoadLevel("Demo 01");
            }

        }

    }

    public override void OnConnectedToMaster()
    {

        joinButton.interactable = true;

        connectionInfoText.text = "�� �� �غ� �Ϸ�!";
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        joinButton.interactable = false;

        connectionInfoText.text = "��������: ��Ÿ�� ������� ����\n��Ÿ�� ȣ�� �ϴ���...";

        PhotonNetwork.ConnectUsingSettings();

       //base.OnDisconnected(cause);

    }

    public void Connect()
    {
        joinButton.interactable = false;

        if (PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "���Ϳ� ���� ��..";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            connectionInfoText.text = "��������: ��Ÿ�� ������� ����\n��Ÿ�� ȣ�� �ϴ���...";

            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnJoinRandomFailed(short returnCode, string message)

    {
        //base.OnJoinRandomFailed(returnCode, message);
        connectionInfoText.text = "���Ͱ� ����, ��Ÿ�� ���ο� ���� ������...";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

}