using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class networkchange : MonoBehaviourPunCallbacks
{
    [SerializeField] Text infotext;
    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        print("���� ���� �Ϸ�");
    }
    public void Disconnect() => PhotonNetwork.Disconnect();
    public override void OnDisconnected(DisconnectCause cause) => print("�������");

    public void JoinLobby() => PhotonNetwork.JoinLobby();
    public void LeaveLobby() => PhotonNetwork.LeaveLobby();
    public override void OnJoinedLobby() => print("�κ����ӿϷ�");
    public override void OnLeftLobby() => print("�κ� ������");
    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 3 });
    }
    public override void OnJoinedRoom() => print("�� ����");
    public void LeaveRoom() => PhotonNetwork.LeaveRoom();
    public override void OnLeftRoom() => print("�� ������");
}
