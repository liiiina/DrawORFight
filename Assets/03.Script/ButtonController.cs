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
    [SerializeField] GameObject settingPanel;
    [SerializeField] Button img_shot;
    public GameObject[] RespawnPoint = new GameObject[4];
    private GameObject tmp;
    private GameObject attackpoint;
    
    PlayerAnimController m_Animplayer;
    Transform respawnTransform;

    bool isAttack = false;


    public GameObject _gettmp { get { return tmp; } }

    IEnumerator Attack()
    {
        var cooltime = 0.7f;
        isAttack = true;
        AudioManager.shotPlay();
        m_Animplayer.DoAttack();
        img_shot.interactable = false;
        yield return new WaitForSeconds(cooltime);
        img_shot.interactable = true;
        isAttack = false;
    }

    public void OnClickChangeButton()
    {
        m_Animplayer.ChangeWeapon();
    }
    public void OnClickAttackButton()
    {
        if(!isAttack)
            StartCoroutine("Attack");
    }
    public void OnclickRespawnButton()
    {
        GameObject.Find("UI").transform.Find("Panel_Respawn").gameObject.SetActive(false);
        tmp = PhotonNetwork.Instantiate("Player", new Vector3(respawnTransform.position.x, respawnTransform.position.y), Quaternion.identity);
        m_Animplayer = tmp.GetComponent<PlayerAnimController>();
    }
    public void OnClickLobbynButton()
    {
        print("로비로 고고씽");
        SceneManager.LoadSceneAsync("Lobby");
    }

    public void OnClickSettingButton()
    {
        settingPanel.SetActive(true);
    }


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
