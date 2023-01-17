using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;

public class ResultManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Text myPercentText, winnerText;

    int width = 48;
    int height = 24;
    int maxplayer = 4;

    

    void Start()
    {
        var id = NetworkManager._userid-1;
        var playerinfo = PlayerList.Instance;
        //for(int j = 0;j< maxplayer; j++) Debug.Log(playerinfo.GetTileCount(j));
        //Debug.Log(id);
        // 타일 퍼센트
        if (playerinfo.GetTileCount(id) == 0) myPercentText.text = "0%";
        else
        {
            float tilePercent = (float)playerinfo.GetTileCount(id) / (float)(width * height) * 100;
            myPercentText.text = string.Format("{0:0.##}", tilePercent)+"%"; //나의 타일 퍼센트
        }
        winnerText.text = playerinfo.GetWinner();
    }

}



