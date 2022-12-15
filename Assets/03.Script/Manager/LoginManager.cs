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
                loginInfoText.text = "닉네임을 한글자 이상 입력해주세요";
            }
            else if (inputText.Length > 10)
            {
                loginInfoText.text = "닉네임을 10글자 이하로 입력해주세요";
            }
            else
            {
                loginInput.interactable = false;
                gameinto++;
                m_name = loginInput.text;
                loginInfoText.text = loginInput.text + "님 반갑습니다! 게임시작 버튼을 눌러주세요 :)";
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
