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


    int width = 49;
    int height = 25;

    void Start()
    {
        var id = NetworkManager._userid;
        var playerinfo = PlayerList.instance;
        //그리드 포문으로 확인해서 색깔 몇개 있는지 확인 48 * 24
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                
                var tile = GameObject.Find($"Tile {i} {j}").GetComponent<SpriteRenderer>();
                for(int n = 0; n < 4; n++)
                {
                    if (Color.Equals(tile.color, playerinfo.GetPlayerColor(n)))
                        playerinfo.TileCount(n);
                }
                
            }
        }
        myPercentText.text = ((width * height) / playerinfo.GetTileCount(id)*100).ToString()+"%";//나의 타일 퍼센트
        
        /*
        var winnercount = tilecount.Max();
        var winnerid = Array.IndexOf(tilecount, winnercount);
        winnerText.text = winnerid.ToString();
        */
    }

}
