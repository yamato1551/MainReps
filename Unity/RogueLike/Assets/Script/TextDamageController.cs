using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class TextDamageController : MonoBehaviour
{
    float destroyTime = 1;
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        // �c���ŏ������Ȃ��ď�����
        transform.DOScale(new Vector2(1, 1), destroyTime / 2).SetRelative().OnComplete(() =>
        {
            transform.DOScale(new Vector2(0, 0), destroyTime / 2).OnComplete(() => Destroy(gameObject));
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (!target) return;

        // �e�L�X�g�ʒu�X�V
        Vector3 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, target.transform.transform.position);
        transform.position = pos;

    }

    public void Init(GameObject target, float damage)
    {
        this.target = target;
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();

        text.text = "" + (int)damage;

        // �v���C���[�̃_���[�W�͐ԕ\��
        if (target.GetComponent<PlayerController>()) {
            text.color = Color.red;
        }
    }
}
