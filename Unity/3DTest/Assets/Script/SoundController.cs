using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SoundController : MonoBehaviour
{
    public AudioClip se;
    public List<AudioClip> bgm;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgm[0];
        audioSource.loop = true;
        audioSource.Play();
        // ���̃V�[���ł��폜���ꂸ�����p���I�u�W�F�N�g����錾
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            audioSource.PlayOneShot(se);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (audioSource.isPlaying) {
                audioSource.Stop();
            } else {
                audioSource.Play();
            }
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            audioSource.clip = bgm[1];
            audioSource.Play();
            SceneManager.LoadScene("NextScene");
            
        }
    }
}
