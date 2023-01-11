using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAnimController : MonoBehaviour
{
    PlayerController m_player;
    [SerializeField] Animator m_animator;
    [SerializeField] GameObject movepoint;

    Button attackButton;
    public bool _IsDrawing
    {
        get;
        private set;
    }

    
    public void ChangeWeapon() {
        _IsDrawing = !_IsDrawing;
        SetDraw( _IsDrawing );
        m_player.SPEED = m_player.SPEED;
        if (!_IsDrawing)
        {
            movepoint.SetActive(false);
            m_player.SPEED = 3f;
        }
        else
        {
            movepoint.SetActive(true);
            m_player.SPEED = 4f;
        }
    }

    public void DoAttack()
    {
        m_animator.SetTrigger("Attack");
    }
    public void Attacked()
    {
        m_animator.SetTrigger("Damage");
    }
    public void Die()
    {
        m_animator.SetTrigger("Dead");
    }
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_player = GetComponent<PlayerController>();
        _IsDrawing = false;
    }
    private void Update()
    {
        if (_IsDrawing) attackButton.interactable = false;
        else attackButton.interactable = true;

    }
    private void Awake()
    {
        attackButton = GameObject.Find("UI").transform.Find("Button").transform.Find("Btn_Attack").gameObject.GetComponent<Button>();
    }

    void SetDraw(bool isDraw) { m_animator.SetBool("IsDraw", isDraw); }
    public void SetMove(bool isMove) { m_animator.SetBool("IsMove", isMove); }

}
