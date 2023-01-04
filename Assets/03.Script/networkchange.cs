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
        print("서버 접속 완료");
    }
    public void Disconnect() => PhotonNetwork.Disconnect();
    public override void OnDisconnected(DisconnectCause cause) => print("연결끊김");

    public void JoinLobby() => PhotonNetwork.JoinLobby();
    public void LeaveLobby() => PhotonNetwork.LeaveLobby();
    public override void OnJoinedLobby() => print("로비접속완료");
    public override void OnLeftLobby() => print("로비 나가기");
    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 3 });
    }
    public override void OnJoinedRoom() => print("룸 접속");
    public void LeaveRoom() => PhotonNetwork.LeaveRoom();
    public override void OnLeftRoom() => print("룸 나가기");
}
