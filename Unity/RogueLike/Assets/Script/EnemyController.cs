using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public CharacterStats Stats;

    GameSceneDirector sceneDirector;
    Rigidbody2D rigidbody2d;

    // 攻撃のクールダウン
    float attackCoolDownTimer;
    float attackCoolDownTimeMax = 0.5f;
    // 向き
    Vector2 forword;

    // 状態
    enum State
    {
        Alive,
        Dead
    }
    State state;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateTimer();
        moveEnemy();
    }

    // 初期化
    public void Init(GameSceneDirector sceneDirector, CharacterStats characterStats)
    {
        this.sceneDirector = sceneDirector;
        this.Stats = characterStats;

        rigidbody2d = GetComponent<Rigidbody2D>();

        // アニメーション
        // ランダムで緩急を付ける
        float random = Random.Range(0.8f, 1.2f);
        float speed = 1 / Stats.MoveSpeed * random;

        // サイズ
        float addx = 0.8f;
        float x = addx * random;
        transform.DOScale(x, speed).SetRelative().SetLoops(-1, LoopType.Yoyo);

        // 回転
        float addz = 10f;
        float z = Random.Range(-addz, addz) * random;
        // 初期値
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.x = z;
        // 目標値
        transform.eulerAngles = rotation;
        transform.DORotate(new Vector3(0, 0, -z), speed).SetLoops(-1, LoopType.Yoyo);

        // 進む方向
        PlayerController player = sceneDirector.Player;
        Vector2 dir = player.transform.position - transform.position;
        forword = dir;
        state = State.Alive;
    }
}
