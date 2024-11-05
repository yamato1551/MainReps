using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneDirector : MonoBehaviour
{
    // �X�^�[�g�{�^��
    [SerializeField] Button buttonStart;
    // ���̃{�^�����珇�Ԃ�ID�̃L�����N�^�[�f�[�^��ǂݍ���
    [SerializeField] List<Button> buttonPlayer;
    [SerializeField] List<int> characterIds;
    // �I�������L�����N�^�[ID
    public static int CharacterId;
    // Start is called before the first frame update
    void Start()
    {
        int idx = 0;
        foreach (var item in buttonPlayer)
        {
            // �����\��
            item.gameObject.SetActive(false);

            // �f�[�^������Ȃ�
            if (characterIds.Count - 1 < idx) break;

            // �L�����N�^�[�f�[�^
            int charId = characterIds[idx++];
            CharacterStats charStats = CharacterSettings.Instance.Get(charId);

            // �����\��1�ڂ̃f�[�^��\��
            int weaponId = charStats.DefaultWeaponIds[0];
            WeaponSpawnerStats weaponStats = WeaponSpawnerSettings.Instance.Get(weaponId, 1);

            Image imageCharacter = item.transform.Find("ImageCharacter").GetComponent<Image>();
            Image imageWeapon = item.transform.Find("ImageWeapon").GetComponent<Image>();
            Text textName = item.transform.Find("TextName").GetComponent<Text>();

            // �L�����N�^�[�摜
            if (null != charStats.Prefab)
            {
                imageCharacter.sprite = charStats.Prefab.GetComponent<SpriteRenderer>().sprite;
            }

            // ����摜
            imageWeapon.sprite = weaponStats.Icon;

            //���O
            textName.text = charStats.Name;

            // �����ꂽ���̏���
            item.onClick.AddListener(() =>
            {
                // �A�j���[�V������~
                DOTween.KillAll();
                // �I�����L�����N�^�[ID
                CharacterId = charId;
                // �Q�[���V�[����
                SceneManager.LoadScene("GameScene");
            });

        }
        // �{�^����I����Ԃɂ���
        buttonStart.Select();

        SoundController.Instance.PlayerBGM(0, true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Start�{�^��
    public void OnClickStart()
    {
        //�X�^�[�g�{�^���t�F�[�h�A�E�g
        Utils.DOfadeUpdate(buttonStart, 0, 1);
        buttonStart.interactable = false;

        // �I���ł���v���C���[���t�F�[�h�C��
        for (int i = 0; i < buttonPlayer.Count; i++) 
        { 
            var item = buttonPlayer[i];

            Utils.SetAlpha(item, 0);
            item.gameObject.SetActive(true);

            Utils.DOfadeUpdate(item, 1, 1, 0);
        }

        // �{�^����I����Ԃɂ���
        buttonPlayer[0].Select();

        SoundController.Instance.PlaySE(0);
    }
}
