using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangSpawnerController : BaseWeaponSpawner
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawnTimerNotElapsed()) return;
        // ���퐶��
        for (int i = 0; i < Stats.SpawnCount; i++)
        {
            createWeapon(transform.position);
        }
        spawnTimer = Stats.GetRandomSpawnTimer();
    }
}
