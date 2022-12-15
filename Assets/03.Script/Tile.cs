using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Tile : MonoBehaviourPunCallbacks {
    [SerializeField] private Color _baseColor;
    [SerializeField] private SpriteRenderer _renderer;
    public Color[] PlayerColor = new Color[4];
    

    public void Init()
    {
        _renderer.color = _baseColor;
    }
    /*
    public void DrawTile(Collider2D col,int id)
    {
        var tile = col.gameObject.GetComponent<SpriteRenderer>();
        tile.color = PlayerColor[id];
    }*/

}