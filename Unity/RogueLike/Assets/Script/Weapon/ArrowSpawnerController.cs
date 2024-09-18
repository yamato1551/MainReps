using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawnerController : BaseWeaponSpawner
{
    // Update is called once per frame
    void Update()
    {
        // �^�C�}�[����
        if (isSpawnTimerNotElapsed()) return;

        //�@���̃^�C�}�[
        spawnTimer = Stats.GetRandomSpawnTimer();

        // �G�����Ȃ�
        if (1 > enemySpawner.GetEnemies().Count) return;

        for (int i = 0; i < (int)Stats.SpawnCount; i++)
        {
            // ���퐶��
            ArrowController ctrl = (ArrowController)createWeapon(transform.position);

            // �����_���Ń^�[�Q�b�g������
            List<EnemyController> enemies = enemySpawner.GetEnemies();
            int rnd = Random.Range(0, enemies.Count);
            EnemyController target = enemies[rnd];
            ctrl.Target = target;
        }
    }
}
