using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    // �G����
    [SerializeField] EnemySpawnerController enemySpawner;

    // �v���C���[����
    [SerializeField] Slider sliderHP;
    [SerializeField] Slider sliderXP;
    [SerializeField] Text textLv;

    // �o���l
    [SerializeField] List<GameObject> prefabXP;

    // ���x���A�b�v�p�l��
    [SerializeField] PanelLevelUpController panelLevelUp;

    // �󔠊֘A
    [SerializeField] PanelTreasureChestController panelTreasureChest;
    [SerializeField] GameObject prefabTreasureChest;
    [SerializeField] List<int> treasureChestItemIds;
    [SerializeField] float treasureChestTimerMin;
    [SerializeField] float treasureChestTimerMax;
    float treasureChestTimer;

    // ����ɕ\������A�C�R��
    [SerializeField] Transform canvas;
    [SerializeField] GameObject prefabImagePlayerIcon;
    Dictionary<BaseWeaponSpawner, GameObject> playerWeaponIcons;
    Dictionary<ItemData, GameObject> playerItemIcons;
    const int PlayerIconStartX = 20;
    const int PlayerIconStartY = -40;

    // �|�����G�̃J�E���g
    [SerializeField] Text textDefeatedEnemy;
    public int DefeatedEnemyCount;

    // �Q�[���I�[�o�[
    [SerializeField] PanelGameOverController panelGameOver;

    // �I������
    [SerializeField] float GameOverTime;
    // Start is called before the first frame update
    void Start()
    {
        // DOTween�̃L���p�V�e�B�m��
        DOTween.SetTweensCapacity(1000, 100);

        // �ϐ�������
        playerWeaponIcons = new Dictionary<BaseWeaponSpawner, GameObject>();
        playerItemIcons = new Dictionary<ItemData, GameObject>();

        // �v���C���[�쐬
        int playerId = TitleSceneDirector.CharacterId;
        Player = CharacterSettings.Instance.CreatePlayer(playerId, this, enemySpawner, textLv, sliderHP, sliderXP);

        // �����ݒ�
        OldSeconds = -1;
        enemySpawner.Init(this, tilemapCollider);
        panelLevelUp.Init(this);
        panelTreasureChest.Init(this);
        panelGameOver.Init(this);

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

        // �����l
        treasureChestTimer = Random.Range(treasureChestTimerMin, treasureChestTimerMax);
        DefeatedEnemyCount = -1;

        camCorners();

        // �A�C�R���X�V
        dispPlayerIcon();

        // �|�����G�X�V
        AddDefeatedEnemy();

        // TimeScale���Z�b�g
        setEnabled();

        SoundController.Instance.PlayerBGM(0);
    }

    // Update is called once per frame
    void Update()
    {
        // �Q�[���^�C�}�[�X�V
        updateGameTimer();
        // �󔠐���
        updateTreasureChestSpawner();
        if (GameOverTime < GameTimer)
        {
            DispPanelGameOver();
        }
    }

    // �Q�[���^�C�}�[
    void updateGameTimer()
    {
        GameTimer += Time.deltaTime;

        // �O��ƕb���������Ȃ珈�������Ȃ�
        int seconds = (int)GameTimer % 60;
        if (seconds == OldSeconds) return;
        textTimer.text = Utils.GetTextTimer(GameTimer);
        OldSeconds = seconds;
    }

    // �_���[�W�\��
    public void DispDamage(GameObject target, float damage)
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

    // �o���l�擾
    public void CreateXP(EnemyController enemy)
    {
        float xp = Random.Range(enemy.Stats.XP, enemy.Stats.MaxXP);
        if (0 > xp) return;

        // 5����
        GameObject prefab = prefabXP[0];
        // 10�ȏ�
        if (10 <= xp)
        {
            prefab = prefabXP[2];
        }
        // 5�ȏ�
        else if (5 <= xp)
        {
            prefab = prefabXP[1];
        }
        //�@������
        GameObject obj = Instantiate(prefab, enemy.transform.position, Quaternion.identity);
        XPController ctrl = obj.GetComponent<XPController>();
        ctrl.Init(this, xp);
    }

    // �Q�[���ĊJ/��~
    void setEnabled(bool enabled = true)
    {
        this.enabled = enabled;
        Time.timeScale = (enabled) ? 1 : 0;
        Player.SetEnabled(enabled);
    }

    // �Q�[���ĊJ
    public void PlayGame(BonusData bonusData = null)
    {
        // �A�C�e���ǉ�
        Player.AddBonusData(bonusData);
        // �X�e�[�^�X���f
        dispPlayerIcon();
        //�@�Q�[���ĊJ
        setEnabled();
    }

    // ���x���A�b�v��
    public void DispPanelLevelUp()
    {
        // �ǉ������A�C�e��
        List<WeaponSpawnerStats> items = new List<WeaponSpawnerStats>();

        // ������
        int randomCount = panelLevelUp.GetButtonCount();
        // ����̐�������Ȃ��ꍇ�͌��炷
        int listCount = Player.GetUsableWeaponIds().Count;

        if (listCount < randomCount)
        {
            randomCount = listCount;
        }

        // �{�[�i�X�������_���Ő���
        for (int i = 0; i < randomCount; i++)
        {
            // �����\���킩�烉���_��
            WeaponSpawnerStats randomItem = Player.GetRandomSpawnerStats();
            // �f�[�^����
            if (null == randomItem) continue;

            // ���Ԃ�`�F�b�N
            WeaponSpawnerStats findItem = items.Find(item => item.Id == randomItem.Id);

            // ���Ԃ薳��
            if (null == findItem)
            {
                items.Add(randomItem);
            }
            else
            // �������
            {
                i--;
            }

            // ���x���A�b�v�p�l���\��
            panelLevelUp.DispPanel(items);
            // �Q�[����~
            setEnabled(false);
        }
    }

    // �󔠃p�l����\��
    public void DispPanelTreasureChest()
    {
        // �����_���A�C�e��
        ItemData item = getRandomItemData();
        // �f�[�^����
        if (null == item) return;

        // �p�l���\��
        panelTreasureChest.DispPanel(item);
        // �Q�[�����f
        setEnabled(false);
    }

    // �A�C�e���������_���ŕԂ�
    ItemData getRandomItemData()
    {
        if (1 > treasureChestItemIds.Count) return null;

        // ���I
        int rnd = Random.Range(0, treasureChestItemIds.Count);
        return ItemSettings.Instance.Get(treasureChestItemIds[rnd]);
    }

    // �󔠍쐬
    void updateTreasureChestSpawner()
    {
        // �^�C�}�[
        treasureChestTimer -= Time.deltaTime;
        // �^�C�}�[������
        if (0 < treasureChestTimer) return;

        // �����ꏊ
        float x = Random.Range(WorldStart.x, WorldEnd.x);
        float y = Random.Range(WorldStart.y, WorldEnd.y);

        // �����蔻��̂���^�C���ォ�ǂ���
        if (Utils.IsColliderTile(tilemapCollider, new Vector2(x, y)))return;

        // ����(prefabTreasureChest��x,y���W�̈ʒu�ɁA��]�Ȃ��Ő�������)
        GameObject obj = Instantiate(prefabTreasureChest, new Vector3(x,y,0), Quaternion.identity);
        obj.GetComponent<TreasureChestController>().Init(this);

        // ���̃^�C�}�[�Z�b�g
        treasureChestTimer = Random.Range(treasureChestTimerMin, treasureChestTimerMax);

    }

    // �v���C���[�A�C�R���Z�b�g
    void setPlayerIcon(GameObject obj, Vector2 pos, Sprite icon, int conut)
    {
        // �摜
        Transform image = obj.transform.Find("ImageIcon");
        image.GetComponent<Image>().sprite = icon;

        // �e�L�X�g
        Transform text = obj.transform.Find("TextCount");
        text.GetComponent<TextMeshProUGUI>().text = "" + conut;

        // �ꏊ
        obj.GetComponent<RectTransform>().anchoredPosition = pos;
    }

    // �A�C�R���̕\�����X�V
    void dispPlayerIcon()
    {
        // ����A�C�R���\���ʒu
        float x = PlayerIconStartX;
        float y = PlayerIconStartY;
        float w = prefabImagePlayerIcon.GetComponent<RectTransform>().sizeDelta.x + 1;

        foreach(var item in Player.WeaponSpawners)
        {
            // �쐬�ς݂̃f�[�^������Ύ擾����
            playerWeaponIcons.TryGetValue(item, out GameObject obj);

            // ������΍쐬����
            if (!obj)
            {
                obj = Instantiate(prefabImagePlayerIcon, canvas);
                playerWeaponIcons.Add(item, obj);
            }

            // �A�C�R���Z�b�g
            setPlayerIcon(obj, new Vector2(x, y), item.Stats.Icon, item.Stats.Lv);

            // ���̈ʒu
            x += w;
        }

        // �A�C�e���̃A�C�R���ʒu�\��
        x = PlayerIconStartX;
        y = PlayerIconStartY - w;

        foreach (var item in Player.ItemDatas)
        {
            // �쐬�ς݂̃f�[�^������Ύ擾����
            playerItemIcons.TryGetValue(item.Key, out GameObject obj);

            // ������΍쐬����
            if (!obj)
            {
                obj = Instantiate(prefabImagePlayerIcon, canvas);
                playerItemIcons.Add(item.Key, obj);
            }

            // �A�C�R���Z�b�g
            setPlayerIcon(obj, new Vector2(x, y), item.Key.Icon, item.Value);

            // ���̈ʒu
            x += w;
        }
    }

    // �|�����G���J�E���g
    public void AddDefeatedEnemy()
    {
        DefeatedEnemyCount++;
        textDefeatedEnemy.text = "" + DefeatedEnemyCount;
    }

    // �^�C�g����
    public void LoadSceneTitle()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("TitleScene");
    }

    // �Q�[���I�[�o�[�p�l����\��
    public void DispPanelGameOver()
    {
        // �p�l���\��
        panelGameOver.DispPanel(Player.WeaponSpawners);
        // �Q�[�����f
        setEnabled(false);
    }
}
