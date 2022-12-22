using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerList : MonoBehaviour
{
    //singletone
    private static PlayerList instance = null;

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
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }

        }

    }

    public static PlayerList Instance
    {
        get
        {
            if (instance==null)
            {
                return null;
            }
            return instance;
        }
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
    public string GetWinner() //�����
    {
        int maxtile = 0,maxid = 0;
        for(int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                maxtile = presentInfo[i].TileCount;
            }
            else
            {
                if (presentInfo[i].TileCount > maxtile)
                {
                    maxtile = presentInfo[i].TileCount;
                    maxid = i;
                }
            }
        }
        return presentInfo[maxid].NickName;
    }


    
}
