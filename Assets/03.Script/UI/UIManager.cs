using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject RegistPanel;
    public GameObject LoginPanel;

    public void OnclickRegisterButton()
    {
        RegistPanel.SetActive(true);
    }
    public void OnclickLoginButton()
    {
        LoginPanel.SetActive(true);
    }

    
}
