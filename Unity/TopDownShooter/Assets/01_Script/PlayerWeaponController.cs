using UnityEditor.ShaderGraph;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        // Fire�ɑΉ�����{�^���������ꂽ��Shoot()�֐����Ă΂��
        player.controls.Character.Fire.performed += context => Shoot();
    }
    /// <summary>
    /// �e�۔��ˏ���
    /// </summary>
    private void Shoot()
    {
        GetComponentInChildren<Animator>().SetTrigger("Fire");
    }
}
