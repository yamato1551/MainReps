using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    // �V���O���g�� �ǂ̃V�[���ŌĂ΂�Ă����ꂪ�Ă΂��
    public static SoundController Instance;

    // �Đ����u
    AudioSource audioSource;

    // BGM
    [SerializeField] List<AudioClip> audioClipsBGM;
    // SE
    [SerializeField] List<AudioClip> audioClipsSE;

    private void Awake()
    {
        // �����Ȃ���΃Z�b�g����
        if(null == Instance)
        {
            // �I�[�f�B�I�ݒ�
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;

            // �I�u�W�F�N�g���Z�b�g����
            Instance = this;
            // �V�[�����܂����ł��I�u�W�F�N�g���폜���Ȃ�
            DontDestroyOnLoad(this.gameObject);
        }
        // 2��ڈȍ~�ɐ������ꂽ�I�u�W�F�N�g�͍폜����
        else
        {
            Destroy(this.gameObject);
        }
    }
    // BGM�Đ�
    public void PlayerBGM(int index, bool stop = false)
    {
        if (stop)
        {
            audioSource.clip = audioClipsBGM[index];
            audioSource.Stop();
        }
        else
        {
            audioSource.clip = audioClipsBGM[index];
            audioSource.Play();
        }
        
    }

    // SE�Đ�
    public void PlaySE(int index)
    {
        audioSource.PlayOneShot(audioClipsSE[index]);
    }
}
