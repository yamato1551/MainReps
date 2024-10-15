using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSpawnerSettings", menuName = "ScriptableObject/WeaponSpawnerSettings")]
public class WeaponSpawnerSettings : ScriptableObject
{
    // �f�[�^
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
    // ���X�g��ID����f�[�^����������
    public WeaponSpawnerStats Get(int id, int lv)
    {
        // �w�肳�ꂽ���x���̃f�[�^���������1�ԍ������x���̃f�[�^��Ԃ�
        WeaponSpawnerStats ret = null;

        foreach (var item in datas) 
        {
            if (id != item.Id) continue;

            // �w�背�x���ƈ�v
            if (lv == item.Lv)
            {
                return (WeaponSpawnerStats)item.GetCopy();
            }
            // ���̃f�[�^���Z�b�g����Ă��Ȃ����A����𒴂��郌�x��������������ꊷ����
            else if(null == ret)
            {
                ret = item;
            }
            //�@�T���Ă��郌�x����艺�ŁA�b��f�[�^���傫��
            else if (item.Lv<lv && ret.Lv < item.Lv)
            {
                ret = item;
            }
        }
        return (WeaponSpawnerStats)ret.GetCopy();
    }

    // �쐬
    public BaseWeaponSpawner CreateWeaponSpawner(int id, EnemySpawnerController enemySpawner, Transform parent = null)
    {
        // �f�[�^�擾
        WeaponSpawnerStats stats = Instance.Get(id, 1);
        // �I�u�W�F�N�g�쐬
        GameObject obj = Instantiate(stats.PrefabSpawner, parent);
        // �f�[�^�Z�b�g
        BaseWeaponSpawner spawner = obj.GetComponent<BaseWeaponSpawner>();
        spawner.Init(enemySpawner, stats);

        return spawner;
    }
}


// ���퐶�����u
[System.Serializable]
public class WeaponSpawnerStats : BaseStats
{
    // �������u�̃v���n�u
    public GameObject PrefabSpawner;
    // ����̃A�C�R��
    public Sprite Icon;
    // ���x���A�b�v���ɒǉ������A�C�e��ID
    public int LevelUpItemId;

    // ��x�ɐ������鐔
    public float SpawnCount;
    // �����^�C�}�[
    public float SpawnTimerMin;
    public float SpawnTimerMax;

    // �������Ԏ擾
    public float GetRandomSpawnTimer()
    {
        return Random.Range(SpawnTimerMin, SpawnTimerMax);
    }

    // �A�C�e���ǉ�
    public void AddItemData(ItemData itemData)
    {
        foreach(var item in itemData.Bonuses)
        {
            // ����ŗL�p�����[�^
            if (item.Key == StatsType.SpawnCount)
            {
                SpawnCount = applyBonus(SpawnCount, item.Value, item.Type);
            } 
            // �������ԍŏ�
            else if (item.Key == StatsType.SpawnTimerMin)
            {
                SpawnTimerMin = applyBonus(SpawnTimerMin, item.Value, item.Type);
            }
            // �������ԍő�
            else if (item.Key == StatsType.SpawnTimerMax)
            {
                SpawnTimerMax = applyBonus(SpawnTimerMax, item.Value, item.Type);
            }
            // �ʏ�{�[�i�X
            else
            {
                addBonus(item);
            }        
        }
    }
}