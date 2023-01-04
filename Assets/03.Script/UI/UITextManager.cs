using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextManager : MonoBehaviour
{
    [SerializeField] Text tx;

    private string m_text = "Draw Or Fight";
    private bool isTyping = false;

    private void Start()
    {
        StartCoroutine(OnType(0.2f, m_text));
    }

    IEnumerator OnType(float interval, string Say)
    {
        isTyping = !isTyping;
        foreach (char item in Say)
        {
            tx.text += item;
            yield return new WaitForSeconds(interval);
        }
        yield return new WaitForSeconds(interval*5f);
        isTyping = !isTyping;
    }
    private void Update()
    {
        if (!isTyping)
        {
            tx.text = "";
            StartCoroutine(OnType(0.2f, m_text));
        }
    }
}
