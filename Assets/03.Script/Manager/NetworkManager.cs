using Photon.Pun; // ����Ƽ�� ���� ������Ʈ��
using Photon.Realtime; // ���� ���� ���� ���̺귯��
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
    public Button joinButton; // �� ���� ��ư
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
            InfoText.text = "������ ������ �������Դϴ�...";
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
            InfoText.text = "������ ������ �������Դϴ�...";
            joinButton.interactable = false;
        }
    }

    public override void OnLeftRoom()
    {
        joinButton.interactable = true;
    }

    public override void OnConnectedToMaster() //ConnectUsingSettings�� leave room�� �ݹ� �Լ�
    {
        InfoText.text = "������ ������ ���� �Ϸ�!";
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        InfoText.text = "�κ� ���ӿϷ�!";
        joinButton.interactable = true;
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        InfoText.text = "�κ� ���� ���� �ٽ� �õ� �մϴ�.";
        PhotonNetwork.JoinLobby();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        InfoText.text = "������ ������ ������� �ʽ��ϴ�...\n���� ��õ� ��...";
        PhotonNetwork.ConnectUsingSettings();
    }
    public void Connect()
    {
        ConncetButton.SetActive(false);
        if (PhotonNetwork.IsConnected)
        {
            InfoText.text = "�뿡 ������...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            InfoText.text = "���� ��õ� ��...";
            PhotonNetwork.JoinLobby();
        }
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        InfoText.text = "�� ���� �����ϴ�. ���ο� ���� �������Դϴ�...";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxplayer });
    }
    public override void OnJoinedRoom()
    {
        InfoText.text = "�� ���� ����";
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
                InfoText.text = "�÷��̾ ��ٸ��� �� �Դϴ�...";
                var textObject = countOfPlayerText.GetComponent<Text>();
                countOfPlayerText.SetActive(true);
                textObject.text = "( " + PhotonNetwork.CurrentRoom.PlayerCount + " / 4 )";
                
            }
            else
            {
                if (Master())
                {
                    countOfPlayerText.SetActive(false);
                    InfoText.text = "�ο��� ���� á���ϴ�. �����ϼ���!";
                    StartButton.SetActive(true);
                }
                else
                {
                    countOfPlayerText.SetActive(false);
                    InfoText.text = "�ο��� ���� á���ϴ�. ������ ������ ������ ��ٸ�����!";
                }
            }
        }
   
    }
    
    public void Init()
    {
        
        PhotonNetwork.LoadLevel("Game");
    }
    


}