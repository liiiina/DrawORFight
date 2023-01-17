using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonController : MonoBehaviour
{
    [SerializeField] GameObject exitPanel;
    [SerializeField] GameObject TutoPanel_1;
    [SerializeField] GameObject TutoPanel_2;
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
    public void OnClickBeforePanel()
    {
        TutoPanel_2.SetActive(false);
        TutoPanel_1.SetActive(true);
    }
    public void OnClickNextPanel()
    {
        TutoPanel_1.SetActive(false);
        TutoPanel_2.SetActive(true);
    }
    public void OnClickTutoButton()
    {
        if (TutoPanel_1.activeInHierarchy)
        {
            TutoPanel_1.SetActive(false);
        }
        else if (TutoPanel_2.activeInHierarchy)
        {
            TutoPanel_2.SetActive(false);
        }
        else
        {
            TutoPanel_1.SetActive(true);
        }
    }

}
