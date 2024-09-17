using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashController : BaseWeapon
{
    // ƒgƒŠƒK[‚ªÕ“Ë‚µ‚½
    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackEnemy(collision);
    }
}
