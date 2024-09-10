using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashSpawnerController : BaseWeaponSpawner
{
    // ��x�̐����Ɏ�����t����
    int onceSpawnCount;
    float onceSpawnTime = 0.3f;

    void Start()
    {
        // �ŗL�̃p�����[�^���Z�b�g
        onceSpawnCount = (int)Stats.SpawnCount;
    }

    void Update()
    {
        // �^�C�}�[����
        spawnTimer -= Time.time;
        if (0 < spawnTimer) return;

        // �����ō��E�ɏo��
        int dir = (onceSpawnCount % 2 == 0) ? 1 : -1;

        // �ꏊ
        Vector3 pos = transform.position;
        pos.x + = 2f * dir;

        // ����
        SlashController ctrl = (SlashController)createWeapon(pos, transform);

        // ���E�Ŋp�x��ς���
        ctrl.transform.eulerAngles = ctrl.transform.eulerAngles * dir;

        // ���̐����^�C�}�[
        spawnTimer = onceSpawnTime;
        onceSpawnCount--;

        // �P��̐������I������烊�Z�b�g
        if (1 > onceSpawnCount) 
        { 
            spawnTimer = Stats.GetRandomSpawnTimer();
            onceSpawnCount = (int)Stats.SpawnCount;
        }
    }
}
