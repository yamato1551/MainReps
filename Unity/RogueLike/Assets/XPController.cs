using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class XPController : MonoBehaviour
{
    GameSceneDirector sceneDirector;
    Rigidbody2D rigidbody2d;
    SpriteRenderer spriteRenderer;

    // �o���l
    float xp;
    // 60�b�ŏ�����
    float aliveTimer = 50f;
    float fadeTime = 10f;

    // ������
    public void Init(GameSceneDirector sceneDirector , float xp)
    {
        this.sceneDirector = sceneDirector;
        this.xp = xp;

        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // �Q�[����~��
        if (!sceneDirector.enabled) return;
        // �^�C�}�[�����ŏ����n�߂�
        aliveTimer -= Time.deltaTime;

        if (0 < fadeTime) { 
            // �A���t�@�l��ݒ�
            Color color = spriteRenderer.color;
            color.a -= 1.0f / fadeTime * Time.deltaTime;
            spriteRenderer.color = color;

            // �����Ȃ��Ȃ��������
            if (0 >= color.a)
            {
                Destroy(gameObject);
                return;
            }
        }

        // �v���C���[�Ƃ̋��� Distance�F�������v���Ă�������
        float dist = Vector2.Distance(transform.position, sceneDirector.Player.transform.position);

        // �擾�͈͓��Ȃ�z�����܂��
        if (dist < sceneDirector.Player.Stats.PickUpRange)
        {
            // ���������ړ�
            float speed = sceneDirector.Player.Stats.MoveSpeed * 1.1f;
            Vector2 forward = sceneDirector.Player.transform.position - transform.position;
            rigidbody2d.position += forward.normalized * speed * Time.deltaTime;
        }
    }

    // �g���K�[���Փ˂����Ƃ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �v���C���[����
        if (!collision.gameObject.TryGetComponent<PlayerController>(out var player)) return;

        //�@�擾�o���l
        player.GetXP(xp);
        Destroy(gameObject);
    }
}
