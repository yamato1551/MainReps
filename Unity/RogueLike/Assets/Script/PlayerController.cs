using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PlayerController : MonoBehaviour
{

    // �ړ��ƃA�j���[�V����
    Rigidbody2D rigidbody2d;
    Animator animator;

    // Init�ŃZ�b�g�����
    GameSceneDirector sceneDirector;
    Slider sliderHP;
    Slider sliderXP;

    public CharacterStats Stats;

    // �U���̃N�[���_�E��
    float attackCoolDownTimer;
    float attackCoolDownTimeMax = 0.5f;

    // �K�vXP
    List<int> levelRequirements;
    // �G�������u
    EnemySpawnerController enemySpawner;
    // ����
    public Vector2 Forword;
    // ���x���e�L�X�g
    Text textLv;

    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        moveCamera();
        moveSliderHP();
        updateTimer();
    }
    // ������
    public void Init(GameSceneDirector sceneDirector, EnemySpawnerController enemySpawner, CharacterStats characterStats, Text textLv, Slider sliderHP, Slider sliderXP)
    {
        // �ϐ��̏�����
        levelRequirements = new List<int>();

        this.sceneDirector = sceneDirector;
        this.enemySpawner = enemySpawner;
        this.Stats = characterStats;
        this.textLv = textLv;
        this.sliderHP = sliderHP;
        this.sliderXP = sliderXP;

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // �v���C���[�̌���
        Forword = Vector2.right;

        // �o���l臒l���X�g�쐬
        levelRequirements.Add(0);
        for (int i = 1; i < 1000; i++)
        {
            // 1�O��臒l
            int prevxp = levelRequirements[i - 1];
            // 41�ȍ~�̓��x������16XP������
            int addxp = 16;
            
            // ���x��2�܂Ń��x���A�b�v����̂�5XP
            if (i==1)
            {
                addxp = 5;
            } 
            else if (20 >= i)
            {
                addxp = 10;
            }
            else if (40 >= i)
            {
                addxp = 13;    
            }
            // �K�v�o���l
            levelRequirements.Add(prevxp + addxp);
        }

        // LV2�̕K�v�o���l
        Stats.MaxXP = levelRequirements[1];

        // UI������
        setTextLv();
        setSliderHP();
        setSliderXP();

        moveSliderHP();
    }

    // �v���C���[�̈ړ��Ɋւ��鏈��
    void movePlayer()
    {
        Vector2 dir = Vector2.zero;
        string trigger = "";
        if ( Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            dir += Vector2.up;
            trigger = "isUp";
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            dir += Vector2.down;
            trigger = "isDown";
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            dir += Vector2.right;
            trigger = "isRight";
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            dir += Vector2.left;
            trigger = "isLeft";
        }

        // ���͂�������Δ�����
        if (Vector2.zero == dir) return;

        // �v���C���[�ړ�
        rigidbody2d.position += dir.normalized * Stats.MoveSpeed * Time.deltaTime;

        // �A�j���[�V�����Đ�
        animator.SetTrigger(trigger);

        // �ړ��͈͐���
        // �n�_
        if (rigidbody2d.position.x < sceneDirector.WorldStart.x)
        {
            Vector2 pos = rigidbody2d.position;
            pos.x = sceneDirector.WorldStart.x;
            rigidbody2d.position = pos;
        }
        if (rigidbody2d.position.y < sceneDirector.WorldStart.y)
        {
            Vector2 pos = rigidbody2d.position;
            pos.y = sceneDirector.WorldStart.y;
            rigidbody2d.position = pos;
        }
        // �I�_
        if (sceneDirector.WorldEnd.x < rigidbody2d.position.x)
        {
            Vector2 pos = rigidbody2d.position;
            pos.x = sceneDirector.WorldEnd.x;
            rigidbody2d.position = pos;
        }
        if (sceneDirector.WorldEnd.y < rigidbody2d.position.y)
        {
            Vector2 pos = rigidbody2d.position;
            pos.y = sceneDirector.WorldEnd.y;
            rigidbody2d.position = pos;
        }
    }

    /// <summary>
    /// �J�����ړ�����
    /// </summary>
    void moveCamera()
    {
        Vector3 pos = transform.position;
        pos.z = Camera.main.transform.position.z;
        pos.x = Mathf.Clamp(pos.x, sceneDirector.WorldStart.x - sceneDirector.CamSideStart.x, sceneDirector.WorldEnd.x - sceneDirector.CamSideEnd.x);
        pos.y = Mathf.Clamp(pos.y, sceneDirector.WorldStart.y - sceneDirector.CamSideStart.y, sceneDirector.WorldEnd.y - sceneDirector.CamSideEnd.y);
        
        // �n�_
    /*        if (pos.x < sceneDirector.WorldStart.x - sceneDirector.CamSideStart.x)
        {
            pos.x = sceneDirector.WorldStart.x - sceneDirector.CamSideStart.x;
        }
        if (pos.y < sceneDirector.WorldStart.y - sceneDirector.CamSideStart.y)
        {
            pos.y = sceneDirector.WorldStart.y - sceneDirector.CamSideStart.y;
        }
        if (sceneDirector.WorldEnd.x - sceneDirector.CamSideEnd.x < pos.x)
        {
            pos.x = sceneDirector.WorldEnd.x - sceneDirector.CamSideEnd.x;
        }
        if (sceneDirector.WorldEnd.y - sceneDirector.CamSideEnd.y < pos.y)
        {
            pos.y = sceneDirector.WorldEnd.y - sceneDirector.CamSideEnd.y;
        }*/

        // �J�����̈ʒu�X�V
        Camera.main.transform.position = pos;
    }

    // HP�X���C�_�[�ړ�
    void moveSliderHP()
    {
        // ���[���h���W���X�N���[�����W�ɕϊ�
        // �J�����ƕϊ�������2D�̍��W��n����UI��Canvas�̍��W��Ԃ��Ă����֐��𗘗p
        Vector3 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
        pos.y -= 50;
        sliderHP.transform.position = pos;
    }
    
    // �_���[�W
    public void Damage(float attack)
    {
        // ��A�N�e�B�u�Ȃ甲����
        if (!enabled) return;

        float damage = Mathf.Max(0, attack - Stats.Defence);
        Stats.HP -= damage;

        // �_���[�W�\��
        sceneDirector.DispDamege(gameObject, damage);

        // TODO �Q�[���I�[�o�[
        if (0 > Stats.HP)
        {

        }
        if (0 > Stats.HP) Stats.HP = 0;
        setSliderHP();
    }

    // HP�X���C�_�[�̒l���X�V
    void setSliderHP()
    {
        sliderHP.maxValue = Stats.MaxHP;
        sliderHP.value = Stats.HP;
    }

    // XP�X���C�_�[�̒l���X�V
    void setSliderXP()
    {
        sliderXP.maxValue = Stats.MaxXP;
        sliderXP.value = Stats.XP;
    }

    // �Փ˂����Ƃ�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        attackEnemy(collision);
    }

    // �Փ˂��Ă����
    private void OnCollisionStay2D(Collision2D collision)
    {
        attackEnemy(collision);
    }

    // �Փ˂��I�������
    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    // �v���C���[�֍U������
    void attackEnemy(Collision2D collision)
    {
        // �v���C���[�ȊO
        if (!collision.gameObject.TryGetComponent<EnemyController>(out var enemy)) return;
        // �^�C�}�[������
        if (0 < attackCoolDownTimer) return;

        enemy.Damage(Stats.Attack);
        attackCoolDownTimer = attackCoolDownTimeMax;
    }

    // �e��^�C�}�[�X�V
    void updateTimer()
    {
        if (0 < attackCoolDownTimer)
        {
            attackCoolDownTimer -= Time.deltaTime;
        }
    }

    // ���x���e�L�X�g�X�V
    void setTextLv()
    {
        textLv.text = "LV " + Stats.Lv;
    }

}
