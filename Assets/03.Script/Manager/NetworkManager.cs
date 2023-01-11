using Photon.Pun; // 유니티용 포톤 컴포넌트들
using Photon.Realtime; // 포톤 서비스 관련 라이브러리
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    static NetworkManager _inst;
    public static NetworkManager Inst => _inst;

    [SerializeField] GameObject ConncetButton;
    [SerializeField] GameObject StartButton;
    [SerializeField] GameObject countOfPlayerText;
    [SerializeField] GameObject ID;
    [SerializeField] Text idtext;

    public Text InfoText; 
    public Button joinButton; // 룸 접속 버튼
    public GameObject waitPanel;

    private static int myid;
    private int count = 0; 

    bool roomConnect = false;
    byte maxplayer = 2;

    public static int _userid { get { return myid; } }

    private void Awake() { _inst = this; }


    public void Start()
    {
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.AutomaticallySyncScene = true;
        if (!PhotonNetwork.IsConnected)
        {
            InfoText.text = "마스터 서버에 접속중입니다...";
            PhotonNetwork.ConnectUsingSettings();
            joinButton.interactable = false;
        }
        else if (PhotonNetwork.InRoom)
        {
            Time.timeScale = 1;
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            InfoText.text = "마스터 서버에 접속중입니다...";
            joinButton.interactable = false;
        }
    }

    public override void OnLeftRoom()
    {
        joinButton.interactable = true;
    }

    public override void OnConnectedToMaster() //ConnectUsingSettings와 leave room의 콜백 함수
    {
        InfoText.text = "마스터 서버와 연결 완료!";
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        InfoText.text = "로비에 접속완료!";
        joinButton.interactable = true;
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        InfoText.text = "로비 접속 실패 다시 시도 합니다.";
        PhotonNetwork.JoinLobby();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        InfoText.text = "마스터 서버와 연결되지 않습니다...\n접속 재시도 중...";
        PhotonNetwork.ConnectUsingSettings();
    }
    public void Connect()
    {
        ConncetButton.SetActive(false);
        if (PhotonNetwork.IsConnected)
        {
            InfoText.text = "룸에 접속중...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            InfoText.text = "접속 재시도 중...";
            PhotonNetwork.JoinLobby();
        }
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        InfoText.text = "빈 방이 없습니다. 새로운 방을 생성중입니다...";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxplayer });
    }
    public override void OnJoinedRoom()
    {
        InfoText.text = "방 참가 성공";
        roomConnect = true;
    }
    bool Master()
    {
        return PhotonNetwork.LocalPlayer.IsMasterClient;
    }
    void Update()
    {
        idtext.text = PhotonNetwork.NetworkClientState.ToString();
        if (roomConnect)
        {
            if (count == 0)
            {
                myid = PhotonNetwork.CurrentRoom.PlayerCount;
                count++;
                PlayerList.Instance.AddPlayerinfo(myid-1, LoginManager._username);
            }
            
            if (PhotonNetwork.CurrentRoom.PlayerCount < maxplayer)
            {
                InfoText.text = "플레이어를 기다리는 중 입니다...";
                var textObject = countOfPlayerText.GetComponent<Text>();
                countOfPlayerText.SetActive(true);
                textObject.text = "( " + PhotonNetwork.CurrentRoom.PlayerCount + " / 4 )";
                
            }
            else
            {
                if (Master())
                {
                    countOfPlayerText.SetActive(false);
                    InfoText.text = "인원이 가득 찼습니다. 시작하세요!";
                    StartButton.SetActive(true);
                }
                else
                {
                    countOfPlayerText.SetActive(false);
                    InfoText.text = "인원이 가득 찼습니다. 방장이 시작할 때까지 기다리세요!";
                }
            }
        }
   
    }
    
    public void Init()
    {
        
        PhotonNetwork.LoadLevel("Game");
    }
    


}