using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ShieldController : BaseWeapon
{
    // �v���C���[����̋���
    const float Radius = 1f;
    // ���݂̊p�x
    public float Angle;

    // Start is called before the first frame update
    void Start()
    {
        // �t���b�ƕ\������
        transform.localScale = new Vector3(0, 0, 0);
        transform.DOScale(new Vector3(1, 1, 1), 1.5f).SetEase(Ease.OutBounce);

    }

    // Update is called once per frame
    void Update()
    {   
        // �p�x�X�V
        Angle -= stats.MoveSpeed * Time.deltaTime;
        // Cos�֐��Ƀ��W�A���p���w�肷��ƁAx�̍��W��Ԃ��Ă����Aradius�������ă��[���h���W�ɕϊ�����
        float x = Mathf.Cos(Angle * Mathf.Deg2Rad) * Radius;
        // Sin�֐��Ƀ��W�A���p���w�肷��ƁAy�̍��W��Ԃ��Ă����Aradius�������ă��[���h���W�ɕϊ�����
        float y = Mathf.Sin(Angle * Mathf.Deg2Rad) * Radius;
        // �|�W�V�����X�V
        transform.position = transform.root.position + new Vector3(x, y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �G�ȊO
        if (!collision.gameObject.TryGetComponent<EnemyController>(out var enemy)) return;

        // ���Α��֒��˕Ԃ�
        Vector3 forward = enemy.transform.position - transform.root.position;
        enemy.GetComponent<Rigidbody2D>().AddForce(forward.normalized * 5);

        attackEnemy(collision);
    }
}
