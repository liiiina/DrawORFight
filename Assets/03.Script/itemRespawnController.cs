using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class itemRespawnController : MonoBehaviour
{
    Countdown cd;

    Vector2 randomPosition()
    {
        float rangeX = Random.Range(1, 48.5f);
        float rangeY = Random.Range(1, 25);
        Vector2 randomP = new Vector2(rangeX, rangeY);

        return randomP;

    }
    IEnumerator RandomHealRespawn()
    {
        while (cd._getState)//시간이 끝나지 않았을 때
        {
            yield return new WaitForSeconds(25f);
            GameObject instantHeal = PhotonNetwork.Instantiate("heal", randomPosition(), Quaternion.identity);
        }
    }
    IEnumerator RandomFastRespawn()
    {
        while (cd._getState)
        {
            yield return new WaitForSeconds(15f);
            GameObject instantHeal = PhotonNetwork.Instantiate("fast", randomPosition(), Quaternion.identity);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cd = GameObject.Find("UI").transform.Find("Countdown").GetComponent<Countdown>();
        print(cd._getState);
        StartCoroutine("RandomHealRespawn");
        StartCoroutine("RandomFastRespawn");
    }

}
