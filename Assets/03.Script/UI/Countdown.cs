using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{

    [SerializeField] float setTime = 120.0f;
    [SerializeField] Text countdownText;
    [SerializeField] GameObject resultPanel;
    [SerializeField] GameObject joystick;
    [SerializeField] GameObject resultscript;
    // Start is called before the first frame update

    [SerializeField] int _Min;
    [SerializeField] float _Sec;

    PlayerController m_player;
    GridManager m_gridManager;

    void TimeSet()
    {
        _Min = (int)setTime / 60;
        _Sec = setTime % 60;
    }
    void Start()
    {
        TimeSet();
        //countdownText.text = setTime.ToString();
        countdownText.text = string.Format("{0:D2} : {0:D2}", _Min, (int)_Sec);
    }

    // Update is called once per frame
    void Update()
    {
        if (setTime > 0)
        {
            if (setTime < 60.0f)
            {
                countdownText.color = Color.red;
            }
            setTime -= Time.deltaTime;
            TimeSet();
        }
        else if

            (setTime < 0)
        {
            Time.timeScale = 0.0f;
            resultPanel.SetActive(true);
            joystick.SetActive(false);
            resultscript.SetActive(true);
        }
        int sec = (int)_Sec;
        string tmp = _Min.ToString("D2") + " : " + sec.ToString("D2");
        countdownText.text = tmp;

    }
}
