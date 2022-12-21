using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks,IPunObservable
{
    [SerializeField] VariableJoystick joy;
    [SerializeField] float _speed = 2f;
    [SerializeField] Transform pos;
    [SerializeField] Vector2 boxSize;
    [SerializeField] Canvas _canvas;
    [SerializeField] GameObject m_canvas;
    [SerializeField] GameObject hpPrefab;
    [SerializeField] SpriteRenderer SR;
    [SerializeField] Image Health;
    [SerializeField] GameObject AttackPoint;

    public Text NicknameText;
    public PhotonView PV;
    public Color[] PlayerColor = new Color[4];
    private int _id;

    PlayerAnimController m_aimplayer;

    ButtonController m_button;
    Rigidbody2D rigid;
    CameraController m_cam;
    Tile tilescript;
    Collider2D col;
    Vector2 moveVec;
    Vector3 curPos;

    bool attack = false;
    float x;
    float y;
    float xMin = 1;
    float xMax = 48.5f;
    float yMin = 1;
    float yMax = 25;
    string tilename;
    

    public float getspeed { get { return _speed; } set { _speed = value; } }
    public bool _isAttack { get { return attack; } set { attack = value; } }

    #region Coroutine
    IEnumerator Die(float time)
    {
        m_cam._target = null;
        m_aimplayer.Die();
        GameObject.Find("UI").transform.Find("Panel_Respawn").gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
    }
    #endregion

    #region PunRPC

    [PunRPC]
    void FlipXRPC(float x) => SR.flipX = x < 0;
    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);
    [PunRPC]
    void DrawTile(int id,string name)
    {
        //Debug.Log(id);
        var coll_tile = GameObject.Find(name).GetComponent<SpriteRenderer>();
        /*
        Debug.Log(coll_tile);
        Debug.Log(coll_tile.color);*/
        coll_tile.color = PlayerList.instance.GetPlayerColor(id);
    }
    [PunRPC]
    void RotateXRPC(float x)
    {
        if (x < 0)
        {
            var box = AttackPoint.GetComponent<BoxCollider2D>();
            // 위치 옮기기
        }
    }

    #endregion

    public void OnAttackPoint()
    {
        AttackPoint.SetActive(true);
    }
    public void OffAttackPoint()
    {
        AttackPoint.SetActive(false);
    }
    void Move()
    {
        moveVec = new Vector2(x, y) * _speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + moveVec);
        
        if (moveVec.sqrMagnitude==0)
        {
            m_aimplayer.SetMove(false);
        }
        else
        {
            PV.RPC("FlipXRPC", RpcTarget.AllBuffered, x);
            PV.RPC("RotateXRPC", RpcTarget.AllBuffered, x);
            m_aimplayer.SetMove(true);
        }
        x = Mathf.Clamp(transform.position.x, xMin, xMax);
        y = Mathf.Clamp(transform.position.y, yMin, yMax);
        transform.position = new Vector2(x, y);
    }
    public void Attack(Collider2D col) // 다시 설정
    {
        PlayerController tmp = col.gameObject.GetComponent<PlayerController>();
        Debug.Log(tmp._id);
        
        if (tmp != null&&!PV.IsMine)
        {
            tmp.TakeDamage(0.25f);
        }
        attack = false;
    }

    void TakeDamage(float damage)
    {  
        if (PV.IsMine)
        {
            m_aimplayer.Attacked();
            Health.fillAmount -= damage;
        }
        if (Health.fillAmount == 0 && PV.IsMine) StartCoroutine("Die", 1.4f);
    }

 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile")&& m_aimplayer._IsDrawing)
        {
            tilename = collision.gameObject.name;
            PV.RPC("DrawTile", RpcTarget.AllBuffered,_id,tilename);
        }
        //공격
        else if ( collision.CompareTag("Player") &&
                  PV.IsMine == false )
        {
            Debug.Log(collision);
            Attack(collision);
        }
    }
    
    void Update()
    {
        var camP = m_cam.GetComponent<Camera>();
        m_canvas.transform.position = camP.WorldToScreenPoint(gameObject.transform.position + new Vector3(0, 1.15f, 0));
        if (PV != null && PV.IsMine&&joy!=null)
        {
            x = joy.Horizontal;
            y = joy.Vertical;
            Move();
        }
        // 부드럽게 동기화
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_aimplayer.Attacked();
            TakeDamage(0.25f);
        }
        
    }
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        m_aimplayer = GetComponent<PlayerAnimController>();
        if(PV.IsMine) joy = GameObject.Find("UI").transform.Find("Joystick").gameObject.GetComponent<VariableJoystick>();
    }

    void Start()
    {
        _id = NetworkManager._userid-1;
        PhotonNetwork.NickName = LoginManager._username;
        //Debug.Log(PhotonNetwork.NickName);
        m_cam = Camera.main.GetComponent<CameraController>();
        if(PV.IsMine) m_cam._target = this;
        if (PV != null)
        {
            NicknameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
            NicknameText.color = PV.IsMine ? Color.white : Color.red;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(Health.fillAmount);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            Health.fillAmount = (float)stream.ReceiveNext();
        }
    }
}
