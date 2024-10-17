using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTreasureChestController : MonoBehaviour
{
    [SerializeField] Image imageClose;
    [SerializeField] Image imageOpen;
    [SerializeField] Image imageItem;
    [SerializeField] Image imageBackFX;
    [SerializeField] Image imageBackFXShiny;
    [SerializeField] Button buttonOpen;
    [SerializeField] Button buttonClose;
    [SerializeField] Text textDescription;

    GameSceneDirector sceneDirector;
    // �擾�A�C�e��
    ItemData itemData;
    // �擾�A�C�e���摜�����ʒu
    Vector3 imageItemInitPos;
    // �󔠉摜�����X�P�[��
    Vector3 imageCloseInitScale;
    // �擾�A�C�e���A�j���[�V�����ʒu
    Vector2 itemTargetPos = new Vector2 (0, 70);

    // ������
    public void Init(GameSceneDirector sceneDirector)
    {
        this.sceneDirector = sceneDirector;

        // �����ʒu
        imageItemInitPos = imageItem.rectTransform.anchoredPosition;
        imageCloseInitScale = imageClose.rectTransform.localScale;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    // �󔠃p�l����\��
    public void DispPanel(ItemData item)
    {
        // ����擾�����A�C�e��
        itemData = item;

        // �ꏊ�ȂǏ�����
        imageItem.rectTransform.anchoredPosition = imageItemInitPos;
        imageClose.rectTransform.localScale = imageCloseInitScale;
        imageClose.rectTransform.localEulerAngles = Vector3.zero;
        imageBackFX.rectTransform.anchoredPosition = itemTargetPos;
        imageBackFXShiny.rectTransform.anchoredPosition = itemTargetPos;

        // �A�C�e���摜
        imageItem.sprite = item.Icon;
        Utils.SetAlpha(imageItem, 0);

        // �A�C�e������
        textDescription.text = item.Description;
        Utils.SetAlpha(textDescription, 0);

        // ������
        imageClose.gameObject.SetActive(true);
        Utils.SetAlpha(imageClose, 1);

        // �J������
        imageClose.gameObject.SetActive(false);
        Utils.SetAlpha(imageOpen, 1);

        // �{�^���I�[�v��
        buttonOpen.gameObject.SetActive(true);
        Utils.SetAlpha(buttonOpen, 1);

        // �N���[�Y�{�^��
        buttonClose.gameObject.SetActive(false);
        Utils .SetAlpha(buttonClose, 1);

        // ���o
        Utils.SetAlpha(imageBackFX, 0);
        Utils.SetAlpha(imageBackFXShiny, 0);

        // �p�l���{��
        gameObject.SetActive(true);
    }

    // �I�[�v���{�^���N���b�N
    public void OnClickOpen()
    {
        // �{�^����\��
        buttonOpen.gameObject.SetActive(false);

        // ������
        Transform image = imageClose.transform;

        // �A�j���[�V�����ݒ�
        Vector3 punchScale = new Vector3(1.5f, 1.5f, 0);
        Vector3 punchRotate = new Vector3(0, 0, 30f);
        Vector3 endScale = new Vector3(1.5f, 0.5f, 0);
        float duration = 0.5f;

        // �V�[�P���X�ŃA�j���[�V���������ԂɍĐ�
        Sequence seq = DOTween.Sequence();

        // �󔠒e�ށi�傫���A���ԁA�U�����A�e�͐��j
        seq.Append(image.DOPunchScale(punchScale, duration, 5, 1));
        // �󔠉�]�A�j���[�V����
        seq.Join(
            image.DOPunchRotation(punchRotate, duration)
            // �A�j���[�V�����̎n�܂�ƏI��肪�������A���Ԃ������Ȃ�
            .SetEase(Ease.InOutQuad)
            // �J��Ԃ��񐔂ƃ^�C�v
            .SetLoops(1, LoopType.Yoyo)
        );

        // �ׂ��A�j���[�V����(Append�őO�̃A�j���[�V�������I����Ă���Đ�)
        seq.Append
        (
            image.DOScale(endScale, duration)
            .SetEase(Ease.OutBounce)
        );

        // �Đ�
        seq.Play().SetUpdate(true).OnComplete(() => dispItem());
    }

    // ��]�̂݊֐���
    void�@doRotateLoops(Image image, int dir = 1)
    {
        image.transform
            .DORotate(new Vector3(0, 0, 360f) * dir, 60f, RotateMode.FastBeyond360)
            .SetUpdate(true)
            .SetEase(Ease.Linear)
            .SetLoops(1, LoopType.Restart);
    }

    // �A�C�e���\��
    void dispItem()
    {
        // �󔠕\��
        imageClose.gameObject.SetActive(false);
        imageOpen.gameObject.SetActive(true);

        // �擾�A�C�e��
        RectTransform image = imageItem.rectTransform;

        // �A�C�e���\������
        float itemDuration = 1f;
        // ���o�\������
        float fxDuration = itemDuration / 2;

        // �A�j���[�V���������ԂɍĐ�
        Sequence seq = DOTween.Sequence();

        // �J�����󔠂��t�F�[�h�A�E�g�i�S�̂̊J�n��x�点��j
        seq.Append(imageOpen.DOFade(0, itemDuration).SetDelay(0.5f));

        // �A�C�e���t�F�[�h�C�����ړ�
        seq.Append(imageItem.DOFade(1, itemDuration));
        seq.Join(image.DOAnchorPos(itemTargetPos, itemDuration));

        // ���o�t�F�[�h�C��
        seq.Append(imageBackFX.DOFade(1, fxDuration));
        seq.Join(imageBackFXShiny.DOFade(0.8f, fxDuration));

        // �����t�F�[�h�C��
        seq.Append
        (
            textDescription.DOFade(1, fxDuration)
            .OnComplete(() => buttonClose.gameObject.SetActive(true))
        );

        // ����{�^���Ǝq�I�u�W�F�N�g���t�F�[�h�C��
        seq.Append(buttonClose.image.DOFade(1, fxDuration));
        foreach (var item in buttonClose.GetComponentsInChildren<Graphic>())
        {
            seq.Join(item.DOFade(1, fxDuration));
        }

        // �J�n
        seq.Play().SetUpdate(true);

        // �������[�v�n�̓V�[�P���X�ƕʂœ�����
        doRotateLoops(imageBackFX);
        doRotateLoops(imageBackFXShiny, -1);
    }

    //�@�N���[�Y�{�^��
    public void OnClickClose()
    {
        // ���[�v�n���~�߂�
        imageBackFX.DOKill();
        imageBackFXShiny.DOKill();
        // �p�l����\��
        gameObject.SetActive(false);
        // �Q�[���J�n
        sceneDirector.PlayGame(new BonusData(itemData));
    }
}
