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
    float moveSpeed = 10;

    // ���Init�ֈړ�
    [SerializeField] GameSceneDirector sceneDirector;
    [SerializeField] Slider sliderHP;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        moveCamera();
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
        rigidbody2d.position += dir.normalized * moveSpeed * Time.deltaTime;

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

}
