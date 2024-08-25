using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// �G�̐����^�C�v
public enum SpawnType
{
    Normal,
    Group,
}

[SerializeField]
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
        
    }
}
