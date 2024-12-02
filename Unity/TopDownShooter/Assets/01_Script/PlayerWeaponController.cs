using UnityEditor.ShaderGraph;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        // Fire‚É‘Î‰‚·‚éƒ{ƒ^ƒ“‚ª‰Ÿ‚³‚ê‚½‚çShoot()ŠÖ”‚ªŒÄ‚Î‚ê‚é
        player.controls.Character.Fire.performed += context => Shoot();
    }
    /// <summary>
    /// ’eŠÛ”­Ëˆ—
    /// </summary>
    private void Shoot()
    {
        GetComponentInChildren<Animator>().SetTrigger("Fire");
    }
}
