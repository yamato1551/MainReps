using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameSceneDirector : MonoBehaviour
{
    //　タイルマップ
    [SerializeField] GameObject grid;
    [SerializeField] Tilemap tilemapCollider;

    // マップ全体座標
    // タイルマップ
    public Vector2 TileMapStart = new Vector2(float.MaxValue, float.MaxValue);
    public Vector2 TileMapEnd = new Vector2(float.MinValue, float.MinValue);
    // プレイヤー
    public Vector2 WorldStart;
    public Vector2 WorldEnd;
    // カメラ
    public Vector2 CamSideStart = new Vector2(float.MaxValue, float.MaxValue);
    public Vector2 CamSideEnd = new Vector2(float.MinValue, float.MinValue);

    public PlayerController Player;

    [SerializeField] Transform parentTextDamage;
    [SerializeField] GameObject prefabTextDamage;

    // タイマー
    [SerializeField] Text textTimer;
    public float GameTimer;
    public float OldSeconds;

    // 敵生成
    [SerializeField] EnemySpawnerController enemySpawner;

    // プレイヤー生成
    [SerializeField] Slider sliderHP;
    [SerializeField] Slider sliderXP;
    [SerializeField] Text textLv;

    // 経験値
    [SerializeField] List<GameObject> prefabXP;

    // レベルアップパネル
    [SerializeField] PanelLevelUpController panelLevelUp;

    // Start is called before the first frame update
    void Start()
    {
        // DOTweenのキャパシティ確保
        DOTween.SetTweensCapacity(1000, 100);

        // プレイヤー作成
        int playerId = 0;
        Player = CharacterSettings.Instance.CreatePlayer(playerId, this, enemySpawner, textLv, sliderHP, sliderXP);

        // 初期設定
        OldSeconds = -1;
        enemySpawner.Init(this, tilemapCollider);
        panelLevelUp.Init(this);
        // カメラの移動できる範囲
        foreach (Transform item in grid.GetComponentInChildren<Transform>())
        {

            Vector3 pos = item.position;
            // 開始位置（左下の角）
            TileMapStart.x = Mathf.Min(TileMapStart.x, pos.x);
            TileMapStart.y = Mathf.Min(TileMapStart.y, pos.y);

            // 終了位置（右上の角）
            TileMapEnd.x = Mathf.Max(TileMapEnd.x, pos.x);
            TileMapEnd.y = Mathf.Max(TileMapEnd.y, pos.y);

            #region
            //// 開始位置
            //if (TileMapStart.x > item.position.x)
            //{
            //    TileMapStart.x = item.position.x;
            //}
            //if (TileMapStart.y > item.position.y)
            //{
            //    TileMapStart.y = item.position.y;
            //}
            //// 終了位置
            //if (TileMapEnd.x < item.position.x)
            //{
            //    TileMapEnd.x = item.position.x;
            //}
            //if (TileMapEnd.y < item.position.y)
            //{
            //    TileMapEnd.y = item.position.y;
            //}
            #endregion
        }
        // 結果の表示
        Debug.Log("TileMap Start (左下の角): " + TileMapStart);
        Debug.Log("TileMap End (右上の角): " + TileMapEnd);
        // 使ってたやつ
        #region
        // 画面縦半分の描画範囲（デフォ5タイル）
        //float cameraSize = Camera.main.orthographicSize;
        // 画面縦横比（16:9想定）
        //float aspect = (float)Screen.width / (float)Screen.height;
        //print("aspect:"+ aspect);
        // プレイヤーの移動できる範囲
        #endregion

        WorldStart = new Vector2(TileMapStart.x * 1.5f, TileMapStart.y * 1.5f);
        WorldEnd = new Vector2(TileMapEnd.x * 1.5f, TileMapEnd.y * 1.5f);

        camCorners();
    }

    // Update is called once per frame
    void Update()
    {
        // ゲームタイマー更新
        updateGameTimer();
    }

    // ゲームタイマー
    void updateGameTimer()
    {
        GameTimer += Time.deltaTime;

        // 前回と秒数が同じなら処理をしない
        int seconds = (int)GameTimer % 60;
        if (seconds == OldSeconds) return;
        textTimer.text = Utils.GetTextTimer(GameTimer);
        OldSeconds = seconds;
    }

    // ダメージ表示
    public void DispDamage(GameObject target, float damage)
    {
        GameObject obj = Instantiate(prefabTextDamage, parentTextDamage);
        obj.GetComponent<TextDamageController>().Init(target, damage);
    }

    /// <summary>
    /// カメラの視錐台から表示範囲取得
    /// </summary>
    void camCorners()
    {
        // カメラの近クリップ平面までの距離
        float nearClipPlane = Camera.main.nearClipPlane;

        // カメラの遠クリップ平面までの距離
        float farClipPlane = Camera.main.farClipPlane;

        // カメラの視野角
        float fieldOfView = Camera.main.fieldOfView;

        // カメラのアスペクト比
        float aspect = Camera.main.aspect;

        // 近クリップ平面のコーナーポイントを格納するための配列
        Vector3[] nearCorners = new Vector3[4];

        // 遠クリップ平面のコーナーポイントを格納するための入れる
        Vector3[] farCorners = new Vector3[4];

        // カメラの近クリップ平面のコーナーポイント計算
        Camera.main.CalculateFrustumCorners(new Rect(0, 0, 1, 1), nearClipPlane, Camera.MonoOrStereoscopicEye.Mono, nearCorners);

        // カメラの遠クリップ平面のコーナーポイントを計算
        Camera.main.CalculateFrustumCorners(new Rect(0, 0, 1, 1), farClipPlane, Camera.MonoOrStereoscopicEye.Mono, farCorners);

        /*print("nearCorners : " + nearCorners);
          print("farCorners : " + farCorners);*/

        // カメラの位置を取得
        Vector3 cameraPos = Camera.main.transform.position;

        // ワールド座標系での視錐台のコーナーポイントを計算
        for (int i = 0; i < 4; i++)
        {
            nearCorners[i] = Camera.main.transform.TransformPoint(nearCorners[i]);
            farCorners[i] = Camera.main.transform.TransformPoint(farCorners[i]);
        }

        /*print("nearCorners : " + nearCorners);
          print("farCorners : " + farCorners);*/

        foreach (Vector3 corner in farCorners)
        {
            // 開始位置
            if (CamSideStart.x > corner.x)
            {
                CamSideStart.x = corner.x;
            }
            if (CamSideStart.y > corner.y)
            {
                CamSideStart.y = corner.y;
            }
            // 終了位置
            if (CamSideEnd.x < corner.x)
            {
                CamSideEnd.x = corner.x;
            }
            if (CamSideEnd.y < corner.y)
            {
                CamSideEnd.y = corner.y;
            }
        }
    }

    // 経験値取得
    public void CreateXP(EnemyController enemy)
    {
        float xp = Random.Range(enemy.Stats.XP, enemy.Stats.MaxXP);
        if (0 > xp) return;

        // 5未満
        GameObject prefab = prefabXP[0];
        // 10以上
        if (10 <= xp)
        {
            prefab = prefabXP[2];
        }
        // 5以上
        else if (5 <= xp)
        {
            prefab = prefabXP[1];
        }
        //　初期化
        GameObject obj = Instantiate(prefab, enemy.transform.position, Quaternion.identity);
        XPController ctrl = obj.GetComponent<XPController>();
        ctrl.Init(this, xp);
    }

    // ゲーム再開/停止
    void setEnabled(bool enabled = true)
    {
        this.enabled = enabled;
        Time.timeScale = (enabled) ? 1 : 0;
        Player.SetEnabled(enabled);
    }

    // ゲーム再開
    public void PlayGame(BonusData bonusData = null)
    {
        // アイテム追加
        Player.AddBonusData(bonusData);
        // TODO ステータス反映

        //　ゲーム再開
        setEnabled();
    }

    // レベルアップ時
    public void DispPanelLevelUp()
    {
        // 追加したアイテム
        List<WeaponSpawnerStats> items = new List<WeaponSpawnerStats>();

        // 生成数
        int randomCount = panelLevelUp.GetButtonCount();
        // 武器の数が足りない場合は減らす
        int listCount = Player.GetUsableWeaponIds().Count;

        if (listCount < randomCount)
        {
            randomCount = listCount;
        }

        // ボーナスをランダムで生成
        for (int i = 0; i < randomCount; i++)
        {
            // 装備可能武器からランダム
            WeaponSpawnerStats randomItem = Player.GetRandomSpawnerStats();
            // データ無し
            if (null == randomItem) continue;

            // かぶりチェック
            WeaponSpawnerStats findItem = items.Find(item => item.Id == randomItem.Id);

            // かぶり無し
            if (null == findItem)
            {
                items.Add(randomItem);
            }
            else
            // もう一回
            {
                i--;
            }

            // レベルアップパネル表示
            panelLevelUp.DispPanel(items);
            // ゲーム停止
            setEnabled(false);
        }
    }

    // 宝箱パネルを表示
    public void DispPanelTreasureChest()
    {
        // TODO

    }
}
