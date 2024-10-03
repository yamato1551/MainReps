using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : BaseWeapon
{
    // 状態
    enum State 
    {
        Bomb,
        Explosion,
        DamegeFloor,
        Destroy,
    }
    State state;

    // アニメーター
    Animator animator;
    // アニメーション毎のタイマー
    Dictionary<State, float> animationTimer;
    // ダメージフロア滞在時間
    float damageFloorCoolDownTimer = 0.5f;
    // 敵毎のタイマー（ダメージフロア図）
    Dictionary<EnemyController, float> damageFloorTimer;
    void Start()
    {
        // 初期化
        animationTimer = new Dictionary<State, float>();
        damageFloorTimer = new Dictionary<EnemyController, float>();
        animator = GetComponent<Animator>();

        // 爆弾時
        animationTimer.Add(State.Bomb, Random.Range(0.5f, 1.5f));
        // 爆発時
        animationTimer.Add(State.Explosion, 0.66f);
        // ダメージフロア時
        animationTimer.Add(State.DamegeFloor, 30f);

        // 初期状態
        state = State.Bomb;
    }

    // Update is called once per frame
    void Update()
    {
        // タイマー消化で次の状態へ
        if (animationTimer.ContainsKey(state))
        {
            animationTimer[state] -= Time.deltaTime;
            if (0 > animationTimer[state])
            {
                changeState(++state);
            }
        }
    }

    // 爆弾の状態を変える
    void changeState(State next)
    {
        // 爆発
        if (State.Explosion == next)
        {
            animator.SetTrigger("isExplosion");
            rigidbody2d.gravityScale = 0;
            rigidbody2d.velocity = Vector2.zero;
        } 
        else if (State.DamegeFloor == next)
        {
            animator.SetTrigger("isDamageFloor");
            // 下側に入れる
            GetComponent<SpriteRenderer>().sortingOrder = 2;
        } 
        else if (State.Destroy == next)
        {
            Destroy(gameObject);
        }

        // 現在の状態
        state = next;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
