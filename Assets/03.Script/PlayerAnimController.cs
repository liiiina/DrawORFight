using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimController : MonoBehaviour
{
    PlayerController m_player;
    [SerializeField] Animator m_animator;
    [SerializeField] GameObject movepoint;
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
            //speed증가s
            m_player.SPEED = 4f;
        }
        else
        {
            movepoint.SetActive(true);
            m_player.SPEED = 8f;
        }
    }

    public void DoAttack()
    {
        if (!_IsDrawing) //인라인 안탈때만 공격 가능하게
        {
            m_animator.SetTrigger("Attack");
            //m_player._isAttack = true;
        }
        
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

    void SetDraw(bool isDraw) { m_animator.SetBool("IsDraw", isDraw); }
    public void SetMove(bool isMove) { m_animator.SetBool("IsMove", isMove); }

}
