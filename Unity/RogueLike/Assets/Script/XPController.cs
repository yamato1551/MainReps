using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class XPController : MonoBehaviour
{
    GameSceneDirector sceneDirector;
    Rigidbody2D rigidbody2d;
    SpriteRenderer spriteRenderer;

    // 経験値
    float xp;
    // 60秒で消える
    float aliveTimer = 50f;
    float fadeTime = 10f;

    // 初期化
    public void Init(GameSceneDirector sceneDirector , float xp)
    {
        this.sceneDirector = sceneDirector;
        this.xp = xp;

        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    // 30f / 60f.120f.240f
    // １秒間に切り替わる数
    void Update()
    {
        // ゲーム停止中
        if (!sceneDirector.enabled) return;
        // タイマー消化で消え始める
        aliveTimer -= Time.deltaTime;

        if (0 > aliveTimer) { 
            // アルファ値を設定
            Color color = spriteRenderer.color;
            color.a -= 1.0f / fadeTime * Time.deltaTime;
            spriteRenderer.color = color;

            // 見えなくなったら消す
            if (0 >= color.a)
            {
                Destroy(gameObject);
                return;
            }
        }

        // プレイヤーとの距離 Distance：距離を計ってくれるもの
        float dist = Vector2.Distance(transform.position, sceneDirector.Player.transform.position);

        // 取得範囲内なら吸い込まれる
        if (dist < sceneDirector.Player.Stats.PickUpRange)
        {
            // 少し早く移動
            float speed = sceneDirector.Player.Stats.MoveSpeed * 1.1f;
            Vector2 forward = sceneDirector.Player.transform.position - transform.position;
            rigidbody2d.position += forward.normalized * speed * Time.deltaTime;
        }
    }

    // トリガーが衝突したとき
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // プレイヤー判定
        if (!collision.gameObject.TryGetComponent<PlayerController>(out var player)) return;

        //　取得経験値
        player.GetXP(xp);
        Destroy(gameObject);
    }
}
