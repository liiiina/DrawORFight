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

    public static int _userid { get { return myid; } }

    private void Awake() { _inst = this; }


    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (!PhotonNetwork.IsConnected)
        {
            InfoText.text = "마스터 서버에 접속중입니다...";
            PhotonNetwork.GameVersion = "1";
            PhotonNetwork.ConnectUsingSettings();
            joinButton.interactable = false;
        }
        else
        {
            InfoText.text = "게임에 참가하시겠습니까?";
        }
        
    }

    public override void OnConnectedToMaster()
    {
        joinButton.interactable = true;
        InfoText.text = "마스터 서버와 연결 완료";
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
            InfoText.text = "마스터 서버와 연결되지 않습니다...\n접속 재시도 중...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        InfoText.text = "빈 방이 없습니다. 새로운 방을 생성중입니다...";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
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
        if (roomConnect)
        {
            if (count == 0)
            {
                myid = PhotonNetwork.CurrentRoom.PlayerCount;
                count++;
                idtext.text = LoginManager._username;
                PlayerList.Instance.AddPlayerinfo(myid-1, idtext.text);
            }
            
            if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
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