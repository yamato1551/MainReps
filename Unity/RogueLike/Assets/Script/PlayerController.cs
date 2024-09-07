using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PlayerController : MonoBehaviour
{

    // 移動とアニメーション
    Rigidbody2D rigidbody2d;
    Animator animator;

    // Initでセットされる
    GameSceneDirector sceneDirector;
    Slider sliderHP;
    Slider sliderXP;

    public CharacterStats Stats;

    // 攻撃のクールダウン
    float attackCoolDownTimer;
    float attackCoolDownTimeMax = 0.5f;

    // 必要XP
    List<int> levelRequirements;
    // 敵生成装置
    EnemySpawnerController enemySpawner;
    // 向き
    public Vector2 Forword;
    // レベルテキスト
    Text textLv;

    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        moveCamera();
        moveSliderHP();
        updateTimer();
    }
    // 初期化
    public void Init(GameSceneDirector sceneDirector, EnemySpawnerController enemySpawner, CharacterStats characterStats, Text textLv, Slider sliderHP, Slider sliderXP)
    {
        // 変数の初期化
        levelRequirements = new List<int>();

        this.sceneDirector = sceneDirector;
        this.enemySpawner = enemySpawner;
        this.Stats = characterStats;
        this.textLv = textLv;
        this.sliderHP = sliderHP;
        this.sliderXP = sliderXP;

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // プレイヤーの向き
        Forword = Vector2.right;

        // 経験値閾値リスト作成
        levelRequirements.Add(0);
        for (int i = 1; i < 1000; i++)
        {
            // 1つ前の閾値
            int prevxp = levelRequirements[i - 1];
            // 41以降はレベル毎に16XPずつ増加
            int addxp = 16;
            
            // レベル2までレベルアップするのに5XP
            if (i==1)
            {
                addxp = 5;
            } 
            else if (20 >= i)
            {
                addxp = 10;
            }
            else if (40 >= i)
            {
                addxp = 13;    
            }
            // 必要経験値
            levelRequirements.Add(prevxp + addxp);
        }

        // LV2の必要経験値
        Stats.MaxXP = levelRequirements[1];

        // UI初期化
        setTextLv();
        setSliderHP();
        setSliderXP();

        moveSliderHP();
    }

    // プレイヤーの移動に関する処理
    void movePlayer()
    {
        Vector2 dir = Vector2.zero;
        string trigger = "";
        if ( Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            dir += Vector2.up;
            trigger = "isUp";
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            dir += Vector2.down;
            trigger = "isDown";
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            dir += Vector2.right;
            trigger = "isRight";
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            dir += Vector2.left;
            trigger = "isLeft";
        }

        // 入力が無ければ抜ける
        if (Vector2.zero == dir) return;

        // プレイヤー移動
        rigidbody2d.position += dir.normalized * Stats.MoveSpeed * Time.deltaTime;

        // アニメーション再生
        animator.SetTrigger(trigger);

        // 移動範囲制御
        // 始点
        if (rigidbody2d.position.x < sceneDirector.WorldStart.x)
        {
            Vector2 pos = rigidbody2d.position;
            pos.x = sceneDirector.WorldStart.x;
            rigidbody2d.position = pos;
        }
        if (rigidbody2d.position.y < sceneDirector.WorldStart.y)
        {
            Vector2 pos = rigidbody2d.position;
            pos.y = sceneDirector.WorldStart.y;
            rigidbody2d.position = pos;
        }
        // 終点
        if (sceneDirector.WorldEnd.x < rigidbody2d.position.x)
        {
            Vector2 pos = rigidbody2d.position;
            pos.x = sceneDirector.WorldEnd.x;
            rigidbody2d.position = pos;
        }
        if (sceneDirector.WorldEnd.y < rigidbody2d.position.y)
        {
            Vector2 pos = rigidbody2d.position;
            pos.y = sceneDirector.WorldEnd.y;
            rigidbody2d.position = pos;
        }
    }

    /// <summary>
    /// カメラ移動制御
    /// </summary>
    void moveCamera()
    {
        Vector3 pos = transform.position;
        pos.z = Camera.main.transform.position.z;
        pos.x = Mathf.Clamp(pos.x, sceneDirector.WorldStart.x - sceneDirector.CamSideStart.x, sceneDirector.WorldEnd.x - sceneDirector.CamSideEnd.x);
        pos.y = Mathf.Clamp(pos.y, sceneDirector.WorldStart.y - sceneDirector.CamSideStart.y, sceneDirector.WorldEnd.y - sceneDirector.CamSideEnd.y);
        
        // 始点
    /*        if (pos.x < sceneDirector.WorldStart.x - sceneDirector.CamSideStart.x)
        {
            pos.x = sceneDirector.WorldStart.x - sceneDirector.CamSideStart.x;
        }
        if (pos.y < sceneDirector.WorldStart.y - sceneDirector.CamSideStart.y)
        {
            pos.y = sceneDirector.WorldStart.y - sceneDirector.CamSideStart.y;
        }
        if (sceneDirector.WorldEnd.x - sceneDirector.CamSideEnd.x < pos.x)
        {
            pos.x = sceneDirector.WorldEnd.x - sceneDirector.CamSideEnd.x;
        }
        if (sceneDirector.WorldEnd.y - sceneDirector.CamSideEnd.y < pos.y)
        {
            pos.y = sceneDirector.WorldEnd.y - sceneDirector.CamSideEnd.y;
        }*/

        // カメラの位置更新
        Camera.main.transform.position = pos;
    }

    // HPスライダー移動
    void moveSliderHP()
    {
        // ワールド座標をスクリーン座標に変換
        // カメラと変換したい2Dの座標を渡すとUIのCanvasの座標を返してくれる関数を利用
        Vector3 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
        pos.y -= 50;
        sliderHP.transform.position = pos;
    }
    
    // ダメージ
    public void Damage(float attack)
    {
        // 非アクティブなら抜ける
        if (!enabled) return;

        float damage = Mathf.Max(0, attack - Stats.Defence);
        Stats.HP -= damage;

        // ダメージ表示
        sceneDirector.DispDamege(gameObject, damage);

        // TODO ゲームオーバー
        if (0 > Stats.HP)
        {

        }
        if (0 > Stats.HP) Stats.HP = 0;
        setSliderHP();
    }

    // HPスライダーの値を更新
    void setSliderHP()
    {
        sliderHP.maxValue = Stats.MaxHP;
        sliderHP.value = Stats.HP;
    }

    // XPスライダーの値を更新
    void setSliderXP()
    {
        sliderXP.maxValue = Stats.MaxXP;
        sliderXP.value = Stats.XP;
    }

    // 衝突したとき
    private void OnCollisionEnter2D(Collision2D collision)
    {
        attackEnemy(collision);
    }

    // 衝突している間
    private void OnCollisionStay2D(Collision2D collision)
    {
        attackEnemy(collision);
    }

    // 衝突が終わった時
    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    // プレイヤーへ攻撃する
    void attackEnemy(Collision2D collision)
    {
        // プレイヤー以外
        if (!collision.gameObject.TryGetComponent<EnemyController>(out var enemy)) return;
        // タイマー未消化
        if (0 < attackCoolDownTimer) return;

        enemy.Damage(Stats.Attack);
        attackCoolDownTimer = attackCoolDownTimeMax;
    }

    // 各種タイマー更新
    void updateTimer()
    {
        if (0 < attackCoolDownTimer)
        {
            attackCoolDownTimer -= Time.deltaTime;
        }
    }

    // レベルテキスト更新
    void setTextLv()
    {
        textLv.text = "LV " + Stats.Lv;
    }

}
