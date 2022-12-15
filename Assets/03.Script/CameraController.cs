using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController _target;
    public float _speed = 5f;


    private void LateUpdate()
    {
        if (_target == null)
            return;
        //target�� ��ȿ�� ������ �Ǿ��ִ��� :
        transform.position = Vector3.Lerp(transform.position, _target.transform.position, Time.deltaTime*_speed);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
}
