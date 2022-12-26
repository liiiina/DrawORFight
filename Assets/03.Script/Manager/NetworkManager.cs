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

    public static int _userid { get { return myid; } }

    private void Awake() { _inst = this; }


    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (!PhotonNetwork.IsConnected)
        {
            InfoText.text = "������ ������ �������Դϴ�...";
            PhotonNetwork.GameVersion = "1";
            PhotonNetwork.ConnectUsingSettings();
            joinButton.interactable = false;
        }
        else
        {
            InfoText.text = "���ӿ� �����Ͻðڽ��ϱ�?";
        }
        
    }

    public override void OnConnectedToMaster()
    {
        joinButton.interactable = true;
        InfoText.text = "������ ������ ���� �Ϸ�";
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
            InfoText.text = "������ ������ ������� �ʽ��ϴ�...\n���� ��õ� ��...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        InfoText.text = "�� ���� �����ϴ�. ���ο� ���� �������Դϴ�...";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
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