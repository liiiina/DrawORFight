using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerDrawController : MonoBehaviour
{
    PlayerController player;
    public PhotonView PV;
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.CompareTag("Tile"))
        {
            AudioManager.SoundPlay();
            if (player != null)
                player.Draw(collision);
            else print("draw");
        }
    }
    private void Awake()
    {
        player = gameObject.GetComponentInParent<PlayerController>();
    }

}
