using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSpawnerController : BaseWeaponSpawner
{
    // 一度の生成に時差を付ける
    int onceSpawnCount;
    float onceSpawnTime = 0.3f;

    void Start()
    {
        // 固有のパラメータをセット
        onceSpawnCount = (int)Stats.SpawnCount;
    }

    void Update()
    {
        // タイマー消化
        if (isSpawnTimerNotElapsed()) return;

        // 偶数で左右に出す
        int dir = (onceSpawnCount % 2 == 0) ? 1 : -1;

        // 生成
        AxeController ctrl = (AxeController)createWeapon(transform.position);

        SoundController.Instance.PlaySE(3);

        // 斜め上に力を加える
        ctrl.GetComponent<Rigidbody2D>().AddForce(new Vector2(100 * dir, 350));

        // 次の生成タイマー
        spawnTimer = onceSpawnTime;
        onceSpawnCount--;

        // １回の生成が終わったらリセット
        if (1 > onceSpawnCount)
        {
            spawnTimer = Stats.GetRandomSpawnTimer();
            onceSpawnCount = (int)Stats.SpawnCount;
        }
    }
}
