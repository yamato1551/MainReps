using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : BaseWeapon
{
    // ���
    enum State 
    {
        Bomb,
        Explosion,
        DamegeFloor,
        Destroy,
    }
    State state;

    // �A�j���[�^�[
    Animator animator;
    // �A�j���[�V�������̃^�C�}�[
    Dictionary<State, float> animationTimer;
    // �_���[�W�t���A�؍ݎ���
    float damageFloorCoolDownTimer = 0.5f;
    // �G���̃^�C�}�[�i�_���[�W�t���A�}�j
    Dictionary<EnemyController, float> damageFloorTimer;
    void Start()
    {
        // ������
        animationTimer = new Dictionary<State, float>();
        damageFloorTimer = new Dictionary<EnemyController, float>();
        animator = GetComponent<Animator>();

        // ���e��
        animationTimer.Add(State.Bomb, Random.Range(0.5f, 1.5f));
        // ������
        animationTimer.Add(State.Explosion, 0.66f);
        // �_���[�W�t���A��
        animationTimer.Add(State.DamegeFloor, 30f);

        // �������
        state = State.Bomb;
    }

    // Update is called once per frame
    void Update()
    {
        // �^�C�}�[�����Ŏ��̏�Ԃ�
        if (animationTimer.ContainsKey(state))
        {
            animationTimer[state] -= Time.deltaTime;
            if (0 > animationTimer[state])
            {
                changeState(++state);
            }
        }
    }

    // ���e�̏�Ԃ�ς���
    void changeState(State next)
    {
        // ����
        if (State.Explosion == next)
        {
            animator.SetTrigger("isExplosion");
            rigidbody2d.gravityScale = 0;
            rigidbody2d.velocity = Vector2.zero;
        } 
        else if (State.DamegeFloor == next)
        {
            animator.SetTrigger("isDamageFloor");
            // �����ɓ����
            GetComponent<SpriteRenderer>().sortingOrder = 2;
        } 
        else if (State.Destroy == next)
        {
            Destroy(gameObject);
        }

        // ���݂̏��
        state = next;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
