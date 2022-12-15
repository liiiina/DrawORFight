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
    public Color[] PlayerColor = new Color[4];

    int width = 49;
    int height = 25;
    int [] tilecount = new int[5] { 0, 0, 0, 0, 0 }; // 배열 0으로 초기화

    void Start()
    {
        var id = NetworkManager._userid;
        //그리드 포문으로 확인해서 색깔 몇개 있는지 확인 48 * 24
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                
                var tile = GameObject.Find($"Tile {i} {j}").GetComponent<SpriteRenderer>();
                if (Color.Equals(tile.color,PlayerColor[0]))
                {
                    tilecount[1]++;
                }
                else if (Color.Equals(tile.color, PlayerColor[1]))
                {
                    tilecount[2]++;
                }
                else if (Color.Equals(tile.color, PlayerColor[2]))
                {
                    tilecount[3]++;
                }
                else if (Color.Equals(tile.color, PlayerColor[3]))
                {
                    tilecount[4]++;
                }
                else{
                    continue;
                }
                
            }
        }
        myPercentText.text = ((width * height) / tilecount[id]).ToString()+"%";
        var winnercount = tilecount.Max();
        var winnerid = Array.IndexOf(tilecount, winnercount);
        winnerText.text = winnerid.ToString();
    }

}
