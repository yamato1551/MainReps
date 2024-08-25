using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameSceneDirector : MonoBehaviour
{
    //�@�^�C���}�b�v
    [SerializeField] GameObject grid;
    [SerializeField] Tilemap tilemapCollider;

    // �}�b�v�S�̍��W
    // �^�C���}�b�v
    public Vector2 TileMapStart = new Vector2(float.MaxValue, float.MaxValue);
    public Vector2 TileMapEnd = new Vector2(float.MinValue, float.MinValue);
    // �v���C���[
    public Vector2 WorldStart;
    public Vector2 WorldEnd;
    // �J����
    public Vector2 CamSideStart = new Vector2(float.MaxValue, float.MaxValue);
    public Vector2 CamSideEnd = new Vector2(float.MinValue, float.MinValue);

    public PlayerController Player;

    [SerializeField] Transform parentTextDamage;
    [SerializeField] GameObject prefabTextDamage;

    // �^�C�}�[
    [SerializeField] Text textTimer;
    public float GameTimer;
    public float OldSeconds;

    // Start is called before the first frame update
    void Start()
    {
        OldSeconds = -1;

        // �J�����̈ړ��ł���͈�
        foreach (Transform item in grid.GetComponentInChildren<Transform>())
        {

            Vector3 pos = item.position;
            // �J�n�ʒu�i�����̊p�j
            TileMapStart.x = Mathf.Min(TileMapStart.x, pos.x);
            TileMapStart.y = Mathf.Min(TileMapStart.y, pos.y);

            // �I���ʒu�i�E��̊p�j
            TileMapEnd.x = Mathf.Max(TileMapEnd.x, pos.x);
            TileMapEnd.y = Mathf.Max(TileMapEnd.y, pos.y);

            #region
            //// �J�n�ʒu
            //if (TileMapStart.x > item.position.x)
            //{
            //    TileMapStart.x = item.position.x;
            //}
            //if (TileMapStart.y > item.position.y)
            //{
            //    TileMapStart.y = item.position.y;
            //}
            //// �I���ʒu
            //if (TileMapEnd.x < item.position.x)
            //{
            //    TileMapEnd.x = item.position.x;
            //}
            //if (TileMapEnd.y < item.position.y)
            //{
            //    TileMapEnd.y = item.position.y;
            //}
            #endregion
        }
        // ���ʂ̕\��
        Debug.Log("TileMap Start (�����̊p): " + TileMapStart);
        Debug.Log("TileMap End (�E��̊p): " + TileMapEnd);
        // �g���Ă����
        #region
        // ��ʏc�����̕`��͈́i�f�t�H5�^�C���j
        //float cameraSize = Camera.main.orthographicSize;
        // ��ʏc����i16:9�z��j
        //float aspect = (float)Screen.width / (float)Screen.height;
        //print("aspect:"+ aspect);
        // �v���C���[�̈ړ��ł���͈�
        #endregion

        WorldStart = new Vector2(TileMapStart.x * 1.5f, TileMapStart.y * 1.5f);
        WorldEnd = new Vector2(TileMapEnd.x * 1.5f, TileMapEnd.y * 1.5f);
        print("WorldStart : " + WorldStart);
        print("WorldEnd : " + WorldEnd);

        camCorners();
        print("camSideStart : " + CamSideStart);
        print("camSideEnd : " + CamSideEnd);
    }

    // Update is called once per frame
    void Update()
    {
        // �Q�[���^�C�}�[�X�V
        updateGameTimer();
    }

    // �Q�[���^�C�}�[
    void updateGameTimer()
    {
        GameTimer += Time.deltaTime;

        // �O��ƕb���������Ȃ珈�������Ȃ�
        int seconds = (int)GameTimer % 60;
        if (seconds == OldSeconds) return;

        textTimer.text = Utils.GetTextTimer(seconds);
        OldSeconds = seconds;
    }

    // �_���[�W�\��
    public void DispDamege(GameObject target, float damage)
    {
        GameObject obj = Instantiate(prefabTextDamage, parentTextDamage);
        obj.GetComponent<TextDamageController>().Init(target, damage);
    }

    /// <summary>
    /// �J�����̎����䂩��\���͈͎擾
    /// </summary>
    void camCorners()
    {
        // �J�����̋߃N���b�v���ʂ܂ł̋���
        float nearClipPlane = Camera.main.nearClipPlane;

        // �J�����̉��N���b�v���ʂ܂ł̋���
        float farClipPlane = Camera.main.farClipPlane;
        
        // �J�����̎���p
        float fieldOfView = Camera.main.fieldOfView;
        
        // �J�����̃A�X�y�N�g��
        float aspect = Camera.main.aspect;
        
        // �߃N���b�v���ʂ̃R�[�i�[�|�C���g���i�[���邽�߂̔z��
        Vector3[] nearCorners = new Vector3[4];
        
        // ���N���b�v���ʂ̃R�[�i�[�|�C���g���i�[���邽�߂̓����
        Vector3[] farCorners = new Vector3[4];
        
        // �J�����̋߃N���b�v���ʂ̃R�[�i�[�|�C���g�v�Z
        Camera.main.CalculateFrustumCorners(new Rect(0, 0, 1, 1), nearClipPlane, Camera.MonoOrStereoscopicEye.Mono, nearCorners);
        
        // �J�����̉��N���b�v���ʂ̃R�[�i�[�|�C���g���v�Z
        Camera.main.CalculateFrustumCorners(new Rect(0, 0, 1, 1), farClipPlane, Camera.MonoOrStereoscopicEye.Mono, farCorners);
        
        /*print("nearCorners : " + nearCorners);
          print("farCorners : " + farCorners);*/
        
        // �J�����̈ʒu���擾
        Vector3 cameraPos = Camera.main.transform.position;
        
        // ���[���h���W�n�ł̎�����̃R�[�i�[�|�C���g���v�Z
        for (int i = 0; i < 4; i++)
        {
            nearCorners[i] = Camera.main.transform.TransformPoint(nearCorners[i]);
            farCorners[i] = Camera.main.transform.TransformPoint(farCorners[i]);
        }
        
        /*print("nearCorners : " + nearCorners);
          print("farCorners : " + farCorners);*/
        
        foreach (Vector3 corner in farCorners)
        {
            // �J�n�ʒu
            if (CamSideStart.x > corner.x)
            {
                CamSideStart.x = corner.x;
            }
            if (CamSideStart.y > corner.y)
            {
                CamSideStart.y = corner.y;
            }
            // �I���ʒu
            if (CamSideEnd.x < corner.x)
            {
                CamSideEnd.x = corner.x;
            }
            if (CamSideEnd.y < corner.y)
            {
                CamSideEnd.y = corner.y;
            }
        }
    }
}
