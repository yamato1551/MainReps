using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpawnerController : BaseWeaponSpawner
{    
    void Update()
    {
        // �I�u�W�F�N�g�̃J�E���g
        weapons.RemoveAll(item => item == null);
        // 1�ł��c���Ă�����I��
        if (0 < weapons.Count) return;

        // �S�������Ȃ�����^�C�}�[����
        if (isSpawnTimerNotElapsed()) return;

        // ���퐶��
        for (int i = 0; i < Stats.SpawnCount; i++)
        {
            ShieldController ctrl = (ShieldController)createWeapon(transform.position, transform);

            SoundController.Instance.PlaySE(6);

            // �����p�x
            ctrl.Angle = 360f / Stats.SpawnCount * i;
        }

        // ���̃^�C�}�[
        spawnTimer = Stats.GetRandomSpawnTimer();
    }
}
