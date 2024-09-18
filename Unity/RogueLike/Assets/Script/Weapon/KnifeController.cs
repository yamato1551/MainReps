﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : BaseWeapon
{
    // Start is called before the first frame update
    void Start()
    {
        // 角度に変換する　tan⁻¹(y/x) * ラジアン角(57.29578)
        float angle = Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg;
        // 角度を代入
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2d.position += forward.normalized * stats.MoveSpeed * Time.deltaTime;

    }

    // トリガーが衝突した時
    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackEnemy(collision);
    }
}
