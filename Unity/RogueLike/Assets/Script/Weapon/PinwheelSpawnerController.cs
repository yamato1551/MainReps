using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PinwheelSpawnerController : BaseWeaponSpawner
{
    void Update()
    {
        if (isSpawnTimerNotElapsed()) return;

        for (int i = 0;i < Stats.SpawnCount;i++)
        {
            createWeapon(transform.position);
        }
        spawnTimer = Stats.GetRandomSpawnTimer();
    }
}
