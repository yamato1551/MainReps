using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeaponSpawner : MonoBehaviour
{
    // ����v���n�u
    [SerializeField] GameObject PrefabWeapon;

    // ����f�[�^
    public WeaponSpawnerStats Stats;
    // �^�������_���[�W
    public float TotalDamage;
    // �ғ��^�C�}�[
    public float TotalTimer;

    // �����^�C�}�[
    protected float spawnTimer;
    // ������������̃��X�g
    protected List<BaseWeapon> weapons;
    // �G�������u
    protected EnemySpawnerController enemySpawner;

    // ������
    public void Init(EnemySpawnerController enemySpawner, WeaponSpawnerStats stats) 
    {
        // �ϐ�������
        weapons = new List<BaseWeapon>();
        this.enemySpawner = enemySpawner;
        this.Stats = stats;
    }

    // �ғ��^�C�}�[
    private void FixedUpdate()
    {
        TotalTimer += Time.fixedDeltaTime;
    }

    /// <summary>
    /// ���퐶��
    /// </summary>
    /// <param name="position">����̏����ʒu</param>
    /// <param name="forward">�����̏����l</param>
    /// <param name="parent">����̐e�I�u�W�F�N�g</param>
    /// <returns>�������������ԋp</returns>
    protected BaseWeapon createWeapon(Vector3 position, Vector2 forward, Transform parent = null)
    {
        // ����
        GameObject obj = Instantiate(PrefabWeapon, position, PrefabWeapon.transform.rotation, parent);
        // ���ʃf�[�^�Z�b�g
        BaseWeapon weapon = obj.GetComponent<BaseWeapon>();
        // �f�[�^������
        weapon.Init(this, forward);
        // ���탊�X�g�֒ǉ�
        weapons.Add(weapon);

        return weapon;
    }
    /// <summary>
    /// ���퐶���ȈՔŁi�������Ȃ��j
    /// </summary>
    /// <param name="positionm">����̏����ʒu</param>
    /// <param name="parent">����̐e�I�u�W�F�N�g</param>
    /// <returns>�������������ԋp</returns>
    protected BaseWeapon createWeapon(Vector3 position, Transform parent = null)
    {
        return createWeapon(position, Vector2.zero, parent);
    }

    // ����̃A�b�v�f�[�g���~����
    public void SetEnabled(bool enabled = true)
    {
        this.enabled = enabled;
        // �I�u�W�F�N�g���폜 �ۑ����Ă��镐��̒��ŗ��p���Ȃ��Ȃ�����������X�g����폜���鏈��
        weapons.RemoveAll(item => !item);
        // ��������������~
        foreach (var item in weapons)
        {
            item.enabled = enabled;
            // Rigidbody��~
            item.GetComponent<Rigidbody2D>().simulated = enabled;
        }
    }

    // �^�C�}�[�����`�F�b�N
    protected bool isSpawnTimerNotElapsed()
    {
        // �^�C�}�[����
        spawnTimer -= Time.deltaTime;
        if (0 < spawnTimer) return true;
        return false;
    }

    // ���x���A�b�v���̃f�[�^��Ԃ�
    public WeaponSpawnerStats GetLevelUpStats(bool isNextLevel = false)
    {
        // ���̃��x��
        int nextLv = Stats.Lv + 1;

        // ���̃��x�������邩�ǂ������ׂāA����Ώ㏑��
        WeaponSpawnerStats ret = WeaponSpawnerSettings.Instance.Get(Stats.Id, nextLv);

        // �㏑���f�[�^����
        if (Stats.Lv < ret.Lv)
        {

        }
        else
        {
            // �������A�C�e���̂��̂ɒu��������
            ItemData itemData = ItemSettings.Instance.Get(Stats.LevelUpItemId);
            ret.Description = itemData.Description;
        }

        // ���x����1�グ�ĕԂ����ǂ���
        if (isNextLevel)
        {
            ret.Lv = nextLv;
        }
        return ret;
    }
    //�@���x���A�b�v
    public void LevelUp()
    {
        // ���݂̃��x��
        int lv = Stats.Lv;

        // ���̃��x���̃f�[�^
        WeaponSpawnerStats nextData = GetLevelUpStats();

        // ���݂̃��x���ƈႦ�Ώ㏑��
        if (Stats.Lv < nextData.Lv)
        {
            Stats = nextData;
        }
        else
        {
            // �������A�C�e���̂��̂ɒu��������
            ItemData itemData = ItemSettings.Instance.Get(Stats.LevelUpItemId);
            Stats.AddItemData(itemData);
        }
        Stats.Lv = lv + 1;
    }
}
