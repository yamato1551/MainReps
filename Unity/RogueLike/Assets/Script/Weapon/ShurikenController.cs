using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenController : BaseWeapon
{
    // Update is called once per frame
    void Update()
    {
        // 回転
        transform.Rotate(new Vector3(0, 0, 1000 * Time.deltaTime));
        // 移動
        rigidbody2d.position += forward.normalized * stats.MoveSpeed * Time.deltaTime;
    }

    // トリガー衝突したとき
    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackEnemy(collision);
    }
}
