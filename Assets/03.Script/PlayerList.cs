using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerList : MonoBehaviour
{
    //singletone
    public static PlayerList instance = null;
    //playerinfo ����ü �����
    struct PlayerInfo
    {
        public int ID;
        public string NickName;
        public int TileCount;
        public Color TileColor;
    }

    PlayerInfo[] presentInfo = new PlayerInfo[4];
    public Color[] PlayerColor = new Color[4];

    // Start is called before the first frame update
    void Start()
    {
        PlayerInfo[] presentInfo = new PlayerInfo[4];
    }


    public void AddPlayerinfo(int id, string name)
    {
        presentInfo[id].ID = id;
        presentInfo[id].NickName = name;
        presentInfo[id].TileCount = 0;
        presentInfo[id].TileColor = PlayerColor[id];
    }
    public void TileCount(int id) //Ÿ�� ���� ����
    {
        presentInfo[id].TileCount++;
    }

    public Color GetPlayerColor(int id) //�÷� ��������
    {
        return presentInfo[id].TileColor;
    }

    
    public string GetPlaerNickname(int id)// �̸� ��������
    {
        return presentInfo[id].NickName;
    }

    public int GetTileCount(int id)//Ÿ�� ����
    {
        return presentInfo[id].TileCount;
    }
    public int GetWinner() //�����
    {

        return 0;
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = null;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
        
    }
}
