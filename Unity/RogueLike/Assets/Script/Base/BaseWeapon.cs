using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    //�@�e�̐������u
    protected BaseWeaponSpawner spawner;
    // ����X�e�[�^�X
    protected WeaponSpawnerStats stats;
    // ��������
    protected Rigidbody2D rigidbody2d;
    // ����
    protected Vector2 forward;

    // ������
    public void Init(BaseWeaponSpawner spawner, Vector2 forward)
    {
        // �e�̐���
        this.spawner = spawner;
        // ����f�[�^�Z�b�g
        this.stats = (WeaponSpawnerStats)spawner.Stats.GetCopy();
        // �i�ޕ���
        this.forward = forward;
        // ��������
        this.rigidbody2d = GetComponent<Rigidbody2D>();
        // �������Ԃ�����ΐݒ肷��
        if (-1 < stats.AliveTime)
        {
            Destroy(gameObject, stats.AliveTime);
        }
    }

    // �G�ւ̍U��
    protected void attackEnemy(Collider2D collider2d, float attack)
    {
        // �G�ꂽ�I�u�W�F�N�g��EnemyController�������Ă��邩���肵�A�����Ă����ꍇenemy�Ɋi�[����
        if (!collider2d.gameObject.TryGetComponent<EnemyController>(out var enemy)) return;
        // �U��
        float damage = enemy.Damage(attack);
        // ���_���[�W�v�Z
        spawner.TotalDamage += damage;

        // HP�ݒ肪����Ύ������_���[�W
        if (0 > stats.HP) return;
        stats.HP--;
        if (0 > stats.HP) Destroy(gameObject);
    }

    // �G�֍U���i�f�t�H���g�̍U���́j
    protected void attackEnemy(Collider2D collider2d)
    {
        attackEnemy(collider2d, stats.Attack);
    }
}
