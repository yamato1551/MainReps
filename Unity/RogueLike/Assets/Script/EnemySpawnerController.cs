using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Random = UnityEngine.Random;

// �G�̐����^�C�v
public enum SpawnType
{
    Normal,
    Group,
}

[Serializable]
public class EnemySpawnData
{
    // �����p
    public string Title;

    // �o���o�ߎ���
    public int ElapsedMinutes;
    public int ElapsedSeconds;
    // �o���^�C�v
    public SpawnType spawnType;
    // ��������
    public float SpawnDuration;
    // ������
    public int SpawnCountMax;
    // ��������GID
    public List<int> EnemyIds;
}

// �G����
public class EnemySpawnerController : MonoBehaviour
{
    // �G�f�[�^
    [SerializeField] List<EnemySpawnData> enemySpawnDatas;
    // ���������G
    List<EnemyController> enemies;

    // �V�[���f�B���N�^�[
    GameSceneDirector sceneDirector;
    // �����蔻��̂���^�C���}�b�v
    Tilemap tilemapCollider;
    // ���݂̎Q�ƃf�[�^
    EnemySpawnData enemySpawnData;
    // �o�ߎ���
    float oldSeconds;
    float spawnTimer;
    // ���݂̃f�[�^�ʒu
    int spawnDataIndex;
    // �G�̏o���ʒu
    const float SpawnRadius = 13;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �G�����f�[�^�X�V
        updateEnemySpawnData();

        // ����
        spawnEnemy();
    }

    // ������
    public void Init(GameSceneDirector sceneDirector, Tilemap tileMapCollider)
    {
        this.sceneDirector = sceneDirector;
        this.tilemapCollider = tileMapCollider;

        // ���������G��ۑ�
        enemies = new List<EnemyController>();
        spawnDataIndex = -1;
    }

    // �G�𐶐�
    void spawnEnemy()
    {
        // ���݂̃f�[�^
        if (null == enemySpawnData) return;
        
        // �^�C�}�[����
        spawnTimer -= Time.deltaTime;
        if (0 < spawnTimer) return;

        if (SpawnType.Group == enemySpawnData.spawnType)
        {
            spawnGroup();
        }
        else if (SpawnType.Normal == enemySpawnData.spawnType)
        {
            spawnNormal();
        }

        spawnTimer = enemySpawnData.SpawnDuration;
    }

    // �ʏ퐶��
    void spawnNormal()
    {
        // �v���C���[�ʒu
        Vector3 center = sceneDirector.Player.transform.position;

        // �G����
        for (int i = 0; i < enemySpawnData.SpawnCountMax; i++)
        {
            // �v���C���[�̎��肩��o��������
            float angle = 360 / enemySpawnData.SpawnCountMax * i;
            // Cos�֐��Ƀ��W�A���p���w�肷��ƁAx�̍��W��Ԃ��Ă����Aradius�������ă��[���h���W�ɕϊ�����
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * SpawnRadius;
            // Sin�֐��Ƀ��W�A���p���w�肷��ƁAx�̍��W��Ԃ��Ă����Aradius�������ă��[���h���W�ɕϊ�����
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * SpawnRadius;
            // �����ʒu
            Vector2 pos = center + new Vector3(x, y, 0);
            // �����蔻��̂���^�C����Ȃ琶�����Ȃ�
            if (Utils.IsColliderTile(tilemapCollider, pos)) continue;
            // ����
            createRandomEnemy(pos);
        }
    }
    // �O���[�v����
    void spawnGroup()
    {
        // �v���C���[�ʒu
        Vector3 center = sceneDirector.Player.transform.position;

        // �v���C���[�̎��肩��o��������
        float angle = Random.Range(0, 360);
        // Cos�֐��Ƀ��W�A���p���w�肷��ƁAx�̍��W��Ԃ��Ă����Aradius�������ă��[���h���W�ɕϊ�����
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * SpawnRadius;
        // Sin�֐��Ƀ��W�A���p���w�肷��ƁAx�̍��W��Ԃ��Ă����Aradius�������ă��[���h���W�ɕϊ�����
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * SpawnRadius;

        // �����ʒu
        center += new Vector3(x, y, 0);
        float radius = 0.5f;

        // �G����
        for (int i = 0; i < enemySpawnData.SpawnCountMax; i++)
        {
            // �v���C���[�̎��肩��o��������
            angle = 360 / enemySpawnData.SpawnCountMax * i;
            // Cos�֐��Ƀ��W�A���p���w�肷��ƁAx�̍��W��Ԃ��Ă����Aradius�������ă��[���h���W�ɕϊ�����
            x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            // Sin�֐��Ƀ��W�A���p���w�肷��ƁAx�̍��W��Ԃ��Ă����Aradius�������ă��[���h���W�ɕϊ�����
            y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            // �����ʒu
            Vector2 pos = center + new Vector3(x, y, 0);
            // �����蔻��̂���^�C����Ȃ琶�����Ȃ�
            if (Utils.IsColliderTile(tilemapCollider, pos)) continue;
            // ����
            createRandomEnemy(pos);
        }
    }
    // �����_����ID�̓G����
    void createRandomEnemy(Vector3 pos)
    {
        // �f�[�^���烉���_����ID���擾
        int rnd = Random.Range(0, enemySpawnData.EnemyIds.Count);
        int id = enemySpawnData.EnemyIds[rnd];

        // �G����
        EnemyController enemy = CharacterSettings.Instance.CreateEnemy(id, sceneDirector, pos);
        enemies.Add(enemy);
    }

    // �o�ߕb���œG�����f�[�^�����ւ���
    void updateEnemySpawnData()
    {
        // �o�ߕb���ɈႢ�������
        if (oldSeconds == sceneDirector.OldSeconds) return;

        // 1��̃f�[�^���Q��
        int idx = spawnDataIndex + 1;

        // �f�[�^�̍Ō�
        if (enemySpawnDatas.Count - 1 < idx) return;

        // �ݒ肳�ꂽ�o�ߎ��Ԃ𒴂��Ă�����f�[�^����ꊷ����
        EnemySpawnData data = enemySpawnDatas[idx];
        int elapsedSeconds = data.ElapsedMinutes * 60 + data.ElapsedSeconds;

        if (elapsedSeconds < sceneDirector.GameTimer)
        {
            enemySpawnData = enemySpawnDatas[idx];

            // ����p�̐ݒ�
            spawnDataIndex = idx;
            spawnTimer = 0;
            oldSeconds = sceneDirector.OldSeconds;
        }
    }

    // �S�Ă̓G��Ԃ�
    public List<EnemyController> GetEnemies()
    {
        enemies.RemoveAll(item => !item);
        return enemies;
    }
 
}
