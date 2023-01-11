using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonController : MonoBehaviour
{
    [SerializeField] GameObject exitPanel;
    public void OnClickExitButton()
    {
        exitPanel.SetActive(true);
    }

    public void OnClickYesButton()
    {
        Application.Quit();
    }
    public void OnclickNoButton()
    {
        exitPanel.SetActive(false);
    } 

}
