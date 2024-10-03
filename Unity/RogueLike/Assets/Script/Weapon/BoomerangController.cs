using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangController : BaseWeapon
{
    // Start is called before the first frame update
    void Start()
    {
        // �����_���ȕ����Ɍ������Ĕ�΂�
        forward = new Vector2 (Random.Range (-1f, 1f), Random.Range(-1f, 1f));
        // -1����1�̊Ԃŋ�����������Ȃ��悤�ɂ���
        forward.Normalize ();
        // ��������t�ɔ�΂�
        Vector2 force = new Vector2(-forward.x * stats.MoveSpeed, -forward.y * stats.MoveSpeed);
        rigidbody2d.AddForce (force);
    }

    // Update is called once per frame
    void Update()
    {
        // ��]
        transform.Rotate(new Vector3(0, 0, 5000 * Time.deltaTime));

        // �ړ�
        rigidbody2d.AddForce(forward * stats.MoveSpeed * Time.deltaTime);
    }

    // �g���K�[���Փ˂�����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackEnemy(collision);
    }
}
