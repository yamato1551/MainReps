using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSpawnerSettings", menuName = "ScriptableObject/WeaponSpawnerSettings")]
public class WeaponSpawnerSettings : ScriptableObject
{
    // データ
    public List<WeaponSpawnerStats> datas;

    static WeaponSpawnerSettings instance;
    public static WeaponSpawnerSettings Instance
    {
        get
        {
            if (!instance)
            {
                instance = Resources.Load<WeaponSpawnerSettings>(nameof(WeaponSpawnerSettings));
            }
            return instance;
        }
    }
    // リストのIDからデータを検索する
    public WeaponSpawnerStats Get(int id, int lv)
    {
        // 指定されたレベルのデータが無ければ1番高いレベルのデータを返す
        WeaponSpawnerStats ret = null;

        foreach (var item in datas) 
        {
            if (id != item.Id) continue;

            // 指定レベルと一致
            if (lv == item.Lv)
            {
                return (WeaponSpawnerStats)item.GetCopy();
            } 
            //　探しているレベル寄り下で、暫定データ寄り大きい
            else if (item.Lv<lv && ret.Lv < item.Lv)
            {
                ret = item;
            }

            // 仮のデータがセットされていないか、それを超えるレベルが合ったら入れ換える
            if (null == ret)
            {
                ret = item;
            }

        }
        return (WeaponSpawnerStats)datas.Find(item => item.Id == id).GetCopy();
    }

    // 作成
    public BaseWeaponSpawner CreateWeaponSpawner(int id, EnemySpawnerController enemySpawner, Transform parent = null)
    {
        // データ取得
        WeaponSpawnerStats stats = Instance.Get(id, 1);
        // オブジェクト作成
        GameObject obj = Instantiate(stats.PrefabSpawner, parent);
        // データセット
        BaseWeaponSpawner spawner = obj.GetComponent<BaseWeaponSpawner>();
        spawner.Init(enemySpawner, stats);

        return spawner;
    }
}


// 武器生成装置
[System.Serializable]
public class WeaponSpawnerStats : BaseStats
{
    // 生成装置のプレハブ
    public GameObject PrefabSpawner;
    // 武器のアイコン
    public Sprite Icon;
    // レベルアップ時に追加されるアイテムID
    public int LevelUpItemId;

    // 一度に生成する数
    public float SpawnCount;
    // 生成タイマー
    public float SpawnTimerMin;
    public float SpawnTimerMax;

    // 生成時間取得
    public float GetRandomSpawnTimer()
    {
        return Random.Range(SpawnTimerMin, SpawnTimerMax);
    }

    // TODO アイテム追加
}