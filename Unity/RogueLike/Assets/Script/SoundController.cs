using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    // シングルトン どのシーンで呼ばれてもこれが呼ばれる
    public static SoundController Instance;

    // 再生装置
    AudioSource audioSource;

    // BGM
    [SerializeField] List<AudioClip> audioClipsBGM;
    // SE
    [SerializeField] List<AudioClip> audioClipsSE;

    private void Awake()
    {
        // もしなければセットする
        if(null == Instance)
        {
            // オーディオ設定
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;

            // オブジェクトをセットする
            Instance = this;
            // シーンをまたいでもオブジェクトを削除しない
            DontDestroyOnLoad(this.gameObject);
        }
        // 2回目以降に生成されたオブジェクトは削除する
        else
        {
            Destroy(this.gameObject);
        }
    }
    // BGM再生
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

    // SE再生
    public void PlaySE(int index)
    {
        audioSource.PlayOneShot(audioClipsSE[index]);
    }
}
