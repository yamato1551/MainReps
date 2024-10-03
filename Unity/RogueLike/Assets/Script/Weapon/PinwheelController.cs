using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinwheelController : BaseWeapon
{
    // ���ˉ�
    int reflectionCount = 5;
    // �J�����\���͈�
    Vector2 cameraSize;

    void Start()
    {
        // �����_���ȕ����Ɍ������Ĕ�΂�
        forward = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        // -1����1�̊Ԃŋ�����������Ȃ��悤�ɂ���
        forward.Normalize();

        // �J�����̕\���͈́i�����j�@�A�X�y�N�g�䗦
        // 1920 / 1080 = 1.777...
        float aspect = Screen.width / (float)Screen.height;
        // orthographicSize =�@�J������Size�̒l�i5�j
        // ���F8.89�A�c�F5.00
        cameraSize = new Vector2(Camera.main.orthographicSize * aspect, Camera.main.orthographicSize);
    }

    void Update()
    {
        // ���ˉ񐔏���
        if (0 > reflectionCount)
        {
            Destroy(gameObject);
            return;
        }

        // �Ǎۂňړ��ʂ𔽓]
        Vector2 camera = Camera.main.transform.position;
        Vector2 start = new Vector2(camera.x - cameraSize.x, camera.y - cameraSize.y);
        Vector2 end = new Vector2(camera.x + cameraSize.x, camera.y + cameraSize.y);
        Vector2 pos = rigidbody2d.position;

        // ��ʊO����
        if (pos.x < start.x || end.x < pos.x)
        {
            forward.x *= -1;
            reflectionCount--;
        }
        if (pos.y < start.y || end.y < pos.y)
        {
            forward.y *= -1;
            reflectionCount--;
        }

        // ��]
        transform.Rotate(new Vector3(0, 0, 1000 * Time.deltaTime));
        // �ړ�
        rigidbody2d.position += forward * stats.MoveSpeed * Time.deltaTime;
    }


    // �g���K�[���Փ˂�����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackEnemy(collision);
    }
}
