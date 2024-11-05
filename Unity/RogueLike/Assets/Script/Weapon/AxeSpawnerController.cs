using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSpawnerController : BaseWeaponSpawner
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
        if (isSpawnTimerNotElapsed()) return;

        // �����ō��E�ɏo��
        int dir = (onceSpawnCount % 2 == 0) ? 1 : -1;

        // ����
        AxeController ctrl = (AxeController)createWeapon(transform.position);

        SoundController.Instance.PlaySE(3);

        // �΂ߏ�ɗ͂�������
        ctrl.GetComponent<Rigidbody2D>().AddForce(new Vector2(100 * dir, 350));

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
