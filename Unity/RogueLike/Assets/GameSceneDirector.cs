using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameSceneDirector : MonoBehaviour
{
    //　タイルマップ
    [SerializeField] GameObject grid;
    [SerializeField] Tilemap tilemapCollider;

    // マップ全体座標
    // タイルマップ
    public Vector2 TileMapStart;
    public Vector2 TileMapEnd;
    // プレイヤー
    public Vector2 WorldStart;
    public Vector2 WorldEnd;
    // カメラ
    public Vector2 CamSideStart = new Vector2(float.MaxValue, float.MaxValue);
    public Vector2 CamSideEnd = new Vector2(float.MinValue, float.MinValue);


    // Start is called before the first frame update
    void Start()
    {
        // カメラの移動できる範囲
        foreach (Transform item in grid.GetComponentInChildren<Transform>())
        {
            // 開始位置
            if (TileMapStart.x > item.position.x)
            {
                TileMapStart.x = item.position.x;
            }
            if (TileMapStart.y > item.position.y)
            {
                TileMapStart.y = item.position.y;
            }
            // 終了位置
            if (TileMapEnd.x < item.position.x)
            {
                TileMapEnd.x = item.position.x;
            }
            if (TileMapEnd.y < item.position.y)
            {
                TileMapEnd.y = item.position.y;
            }
        }

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
        print("WorldStart : " + WorldStart);
        print("WorldEnd : " + WorldEnd);
        print("TileMapStart : " + TileMapStart);
        print("TileMapEnd : " + TileMapEnd);

        camCorners();
        print("camSideStart : " + CamSideStart);
        print("camSideEnd : " + CamSideEnd);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
