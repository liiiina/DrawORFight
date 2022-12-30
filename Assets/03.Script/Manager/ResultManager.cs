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

    void Start()
    {
        var id = NetworkManager._userid-1;
        var playerinfo = PlayerList.Instance;
        for(int j = 0;j<2;j++) Debug.Log(playerinfo.GetTileCount(j));
    // 타일 퍼센트
    if (playerinfo.GetTileCount(id) == 0)
        {
            print("zero tile");
            myPercentText.text = "0%";
        }
       
        else myPercentText.text = ((width * height)/playerinfo.GetTileCount(id)).ToString()+"%";//나의 타일 퍼센트
        winnerText.text = playerinfo.GetWinner();
    }

}
