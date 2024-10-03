using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinwheelController : BaseWeapon
{
    // 反射回数
    int reflectionCount = 5;
    // カメラ表示範囲
    Vector2 cameraSize;

    void Start()
    {
        // ランダムな方向に向かって飛ばす
        forward = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        // -1から1の間で強弱を持たせないようにする
        forward.Normalize();

        // カメラの表示範囲（半分）　アスペクト比率
        // 1920 / 1080 = 1.777...
        float aspect = Screen.width / (float)Screen.height;
        // orthographicSize =　カメラのSizeの値（5）
        // 横：8.89、縦：5.00
        cameraSize = new Vector2(Camera.main.orthographicSize * aspect, Camera.main.orthographicSize);
    }

    void Update()
    {
        // 反射回数消化
        if (0 > reflectionCount)
        {
            Destroy(gameObject);
            return;
        }

        // 壁際で移動量を反転
        Vector2 camera = Camera.main.transform.position;
        Vector2 start = new Vector2(camera.x - cameraSize.x, camera.y - cameraSize.y);
        Vector2 end = new Vector2(camera.x + cameraSize.x, camera.y + cameraSize.y);
        Vector2 pos = rigidbody2d.position;

        // 画面外判定
        if (pos.x < start.x || end.x < pos.x)
        {
            forward.x *= -1;
            reflectionCount--;
        }
        if (pos.y < start.y || end.y < pos.y)
        {
            forward.y *= -1;
            reflectionCount--;
        }

        // 回転
        transform.Rotate(new Vector3(0, 0, 1000 * Time.deltaTime));
        // 移動
        rigidbody2d.position += forward * stats.MoveSpeed * Time.deltaTime;
    }


    // トリガーが衝突した時
    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackEnemy(collision);
    }
}
