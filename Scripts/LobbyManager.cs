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
        connectionInfoText.text = "일 할 준비 하는중...";
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

        connectionInfoText.text = "일 할 준비 완료!";
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        joinButton.interactable = false;

        connectionInfoText.text = "오프라인: 산타가 출근하지 않음\n산타를 호출 하는중...";

        PhotonNetwork.ConnectUsingSettings();

       //base.OnDisconnected(cause);

    }

    public void Connect()
    {
        joinButton.interactable = false;

        if (PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "일터에 들어가는 중..";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            connectionInfoText.text = "오프라인: 산타가 출근하지 않음\n산타를 호출 하는중...";

            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnJoinRandomFailed(short returnCode, string message)

    {
        //base.OnJoinRandomFailed(returnCode, message);
        connectionInfoText.text = "일터가 없음, 산타가 새로운 일터 생성중...";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

}