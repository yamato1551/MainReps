﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : BaseWeapon
{
    // ターゲット
    public EnemyController Target;

    // Start is called before the first frame update
    void Start()
    {
        // 進行方向（ベクトル）
        Vector2 forward = Target.transform.position - transform.position;

        // 角度に変換する　tan⁻¹(y/x) * ラジアン角(57.29578)
        float angle = Mathf.Atan2 (forward.y, forward.x) * Mathf.Rad2Deg;
        print("angle:" + angle);
        // 角度を代入
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Update is called once per frame
    void Update()
    {
        // ターゲットがいない
        if (!Target)
        {
            Destroy(gameObject);
            return;
        }
        // 移動
        Vector2 forward = Target.transform.position - transform.position;
        rigidbody2d.position += forward.normalized * stats.MoveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 敵以外
        if (!collision.gameObject.TryGetComponent<EnemyController>(out var enemy)) return;

        // 通常ダメージ
        float attack = stats.Attack;

        // ターゲットと衝突
        if (Target == enemy)
        {
            Target = null;
        }
        // ターゲット以外の敵は三分の一ダメージ
        else
        {
            attack /= 3;
        }
        attackEnemy(collision, attack);
    }
}
