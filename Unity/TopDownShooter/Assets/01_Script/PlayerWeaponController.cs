using UnityEditor.ShaderGraph;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        // Fireに対応するボタンが押されたらShoot()関数が呼ばれる
        player.controls.Character.Fire.performed += context => Shoot();
    }
    /// <summary>
    /// 弾丸発射処理
    /// </summary>
    private void Shoot()
    {
        GetComponentInChildren<Animator>().SetTrigger("Fire");
    }
}
