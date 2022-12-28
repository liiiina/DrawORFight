using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class ButtonController : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject resultpoint;
    public GameObject[] RespawnPoint = new GameObject[4];
    private GameObject tmp;
    private GameObject attackpoint;
    
    PlayerAnimController m_Animplayer;
    Transform respawnTransform;
 
    public GameObject _gettmp { get { return tmp; } }

    public void OnClickChangeButton()
    {
        m_Animplayer.ChangeWeapon();
    }
    public void OnClickAttackButton()
    {
        m_Animplayer.DoAttack();

    }
    public void OnclickRespawnButton()
    {
        GameObject.Find("UI").transform.Find("Panel_Respawn").gameObject.SetActive(false);
        tmp = PhotonNetwork.Instantiate("Player", new Vector3(respawnTransform.position.x, respawnTransform.position.y), Quaternion.identity);
        m_Animplayer = tmp.GetComponent<PlayerAnimController>();
    }
    public void OnClickLobbynButton() => SceneManager.LoadScene("Lobby");




    void Awake()
    {
        var id = NetworkManager._userid;
        GameObject SA = GameObject.Find("SpawnArea");
        Transform SA_id = SA.transform.Find("SpawnArea_" + id);
        respawnTransform = SA_id.GetComponent<Transform>();
        tmp = PhotonNetwork.Instantiate("Player", new Vector3(respawnTransform.position.x, respawnTransform.position.y), Quaternion.identity);
        m_Animplayer = tmp.GetComponent<PlayerAnimController>();
    }
}
