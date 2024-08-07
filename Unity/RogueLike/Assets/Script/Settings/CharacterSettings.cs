using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="CharacterSettings", menuName ="ScriptableObject/CharacterSetting")]
public class CharacterSettings : ScriptableObject
{
    // �L�����N�^�[�f�[�^
    public List<CharacterStats> datas;

    static CharacterSettings instance;
    public static CharacterSettings Instance
    {
        get
        {
            if (!instance)
            {
                instance = Resources.Load<CharacterSettings>(nameof(CharacterSettings));
            }
            return instance;
        }
    }
    public CharacterStats Get(int id)
    {
        return (CharacterStats)datas.Find(item => item.Id == id).GetCopy();
    }
}

public enum MoveType
{
    // �v���C���[�Ɍ������Đi��
    TargetPlayer,
    // ����Z�ɐi��
    TagetDirecion
}

public class CharacterStats : BaseStats
{
    // �L�����N�^�[�̃v���n�u
    public GameObject Prefab;
    // ������������ID
    public List<int> DefaultWeaponIds;
    // �����\����ID
    public List<int> UsableWeaponIds;
    //�@�����\��
    public int UsableWeaponMax;
    // �ړ��^�C�v
    public MoveType MoveType;

    // TODO �A�C�e���ǉ�
}
