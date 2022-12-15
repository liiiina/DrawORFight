using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    [SerializeField] InputField loginInput;
    [SerializeField] Button loginButton;
    [SerializeField] Text loginInfoText;

    static string m_name;
    private int gameinto = 0;

    public static string _username { get { return m_name; } }

    public void OnClickLoginButton()
    {
        if (gameinto == 0)
        {
            var inputText = loginInput.GetComponent<InputField>().text;
            Debug.Log(inputText.Length);
            if (inputText.Length==0)
            {
                loginInfoText.text = "�г����� �ѱ��� �̻� �Է����ּ���";
            }
            else if (inputText.Length > 10)
            {
                loginInfoText.text = "�г����� 10���� ���Ϸ� �Է����ּ���";
            }
            else
            {
                loginInput.interactable = false;
                gameinto++;
                m_name = loginInput.text;
                loginInfoText.text = loginInput.text + "�� �ݰ����ϴ�! ���ӽ��� ��ư�� �����ּ��� :)";
                var gamestartText = loginButton.GetComponentInChildren<Text>();
                gamestartText.text = "GAME START";
            }
        }
        else
        {
            SceneManager.LoadScene("Lobby");
        }
        
    }
    

}
