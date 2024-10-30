using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGameOverController : MonoBehaviour
{
    // �w�i
    [SerializeField] Image panelGameOverBG;
    // �������ԁA���x���A�|�����G
    [SerializeField] Text textSurvivedResult;
    [SerializeField] Text textLevelResult;
    [SerializeField] Text textEnemyResult;
    // ����A���x���A�_���[�W�A���ԁADPS
    [SerializeField] Text textWeaponNameTitle;
    [SerializeField] Text textWeaponLevelTitle;
    [SerializeField] Text textWeaponDamageTitle;
    [SerializeField] Text textWeaponTimeTitle;
    [SerializeField] Text textWeaponDPSTitle;
    // �I���{�^��
    [SerializeField] Button buttonDone;
    // �p�l����̑S�e�L�X�g
    List<Text> resultTexts;

    GameSceneDirector sceneDirector;

    // ������
    public void Init(GameSceneDirector sceneDirector)
    {
        this.sceneDirector = sceneDirector;
        resultTexts = new List<Text>();

        // �S�e�L�X�g���擾
        foreach(var item in gameObject.GetComponentsInChildren<Text>())
        {
            resultTexts.Add(item);
        }
    }
    
    void Update()
    {
        
    }

    // �e�L�X�g�𕡐����ĐV����y���W���Z�b�g
    Text duplicateText(Text parent, string dispText, float y)
    {
        GameObject obj = Instantiate(parent.gameObject, transform);
        Text text = obj.GetComponent<Text>();
        text.color = Color.white;
        text.text = dispText;

        // y���W�Z�b�g
        Vector3 pos = text.rectTransform.anchoredPosition;
        pos.y = y;
        text.rectTransform.anchoredPosition = pos;

        return text;
    }

    // �p�l���\��
    public void DispPanel(List<BaseWeaponSpawner> weaponSpawners)
    {
        // ��������
        textSurvivedResult.text = Utils.GetTextTimer(sceneDirector.GameTimer);
        // ���x��
        textLevelResult.text = "" + sceneDirector.Player.Stats.Lv;
        // �|�����G�̐�
        textEnemyResult.text = "" + sceneDirector.DefeatedEnemyCount;

        // �X�^�[�g�ʒu
        float y = textWeaponNameTitle.rectTransform.anchoredPosition.y;
        // �e�L�X�g�c�T�C�Y
        float h = textWeaponNameTitle.rectTransform.rect.height;

        // ����̃��U���g��1�s������
        foreach(var item in weaponSpawners)
        {
            // �\���ʒu
            y -= h;

            // ���햼
            Text text = duplicateText(textWeaponNameTitle, item.Stats.Name, y);
            resultTexts.Add(text);

            // ���x��
            text = duplicateText(textWeaponLevelTitle,"" + item.Stats.Lv, y);
            resultTexts.Add(text);

            // �_���[�W
            text = duplicateText(textWeaponDamageTitle, "" + (int)item.TotalDamage, y);
            resultTexts.Add(text);

            // ����
            text = duplicateText(textWeaponTimeTitle, Utils.GetTextTimer(item.TotalTimer), y);
            resultTexts.Add(text);

            // DPS
            int sec = (int)(item.TotalTimer + 1);
            int dps = (int)item.TotalDamage / sec;
            text = duplicateText(textWeaponDPSTitle, "" + dps, y);
            resultTexts.Add(text);
        }

        // ���Ԃɕ\��
        Sequence seq = DOTween.Sequence();
        float dispTime = 1.5f;

        // BG
        Utils.SetAlpha(panelGameOverBG, 0);
        seq.Append(panelGameOverBG.DOFade(1, dispTime));

        // ���̃p�l��
        Image panelGameOver = gameObject.GetComponent<Image>();
        Utils.SetAlpha(panelGameOver, 0);
        seq.Append(panelGameOver.DOFade(1, dispTime));

        // �S�e�L�X�g
        for(int  i = 0; i < resultTexts.Count; i++)
        {
            var item = resultTexts[i];
            Utils.SetAlpha(item, 0);

            // 1�ڂ̃e�L�X�g
            if (0 == i)
            {
                seq.Append(item.DOFade(1, dispTime));
            }
            // ����ȊO�͂P�ڂɍ��킹��
            else
            {
                seq.Join(item.DOFade(1, dispTime));
            }
        }

        // ����{�^���Ǝq�I�u�W�F�N�g
        Utils.SetAlpha(buttonDone, 0);

        // �\�����I������烊�X�i�[��o�^
        seq.Append(buttonDone.image.DOFade(1, dispTime)
            .OnComplete(() =>
            {
                buttonDone.onClick.AddListener(sceneDirector.LoadSceneTitle);
            }));

        foreach (var item in buttonDone.GetComponentsInChildren<Graphic>()) 
        {

            seq.Join(item.DOFade(1, dispTime));
        }

        // �Đ�
        seq.Play().SetUpdate(true);

        // �S�ʂɕ\��
        panelGameOverBG.transform.SetAsLastSibling();
        transform.SetAsLastSibling();

        // �p�l���\��
        panelGameOverBG.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }
}
