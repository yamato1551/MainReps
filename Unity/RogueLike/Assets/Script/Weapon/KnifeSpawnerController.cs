using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawnerController : BaseWeaponSpawner
{
    // ��x�̐����Ɏ�����t����
    int onceSpawnCount;
    float onceSpawnTime = 0.3f;
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        onceSpawnCount = (int)Stats.SpawnCount;
        player = transform.parent.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawnTimerNotElapsed()) return;

        // ���퐶��
        KnifeController ctrl = (KnifeController)createWeapon(transform.position, player.Forward);

        SoundController.Instance.PlaySE(7);

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
