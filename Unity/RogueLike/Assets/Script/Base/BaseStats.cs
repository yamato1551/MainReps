using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// �ǉ��X�e�[�^�X
public enum BonusType
{
    // �萔�Œǉ�
    Bonus,
    // %�Œǉ�
    Boost,
}

public enum StatsType
{
    Attack,
    Defence,
    MoveSpeed,
    HP,
    MaxHP,
    XP,
    MaxXP,
    PickUpRange,
    AliveTime,
    // ���퐶���p�̊g���^�C�v
    SpawnCount,
    SpawnTimerMin,
    SpawnTimerMax,
}

[Serializable]
public class BonusStats
{
    // �ǉ��^�C�v
    public BonusType Type;
    // �ǉ�����v���p�e�B
    public StatsType Key;
    // �ǉ�����l
    public float Value;
}

// �L�����N�^�[�ƕ���ŋ��ʂ̃X�e�[�^�X
public class BaseStats
{
    // Inspector��ŕ\�������^�C�g��
    public string Title;
    //�f�[�^ID
    public int Id;
    // �ݒ背�x��
    public int Lv;
    // ���O
    public string Name;
    // ������
    [TextArea]public string Description;
    // �U����
    public float Attack;
    // �h���
    public float Defense;
    // HP
    public float HP;
    //�ő�HP
    public float MaxHP;
    // �o���l
    public float XP;
    // �ő�o���l
    public float MaxXP;
    // �ړ����x
    public float MoveSpeed;
    // �o���l�擾����
    public float PickUpRange;
    // ��������
    public float AliveTime;

    // StatsType�Ƃ̕R�Â��@�C���f�N�T�𗘗p����
    public float this[StatsType key]
    {
        get => key switch
        {
            StatsType.Attack => Attack,
            StatsType.Defence => Defense,
            StatsType.MoveSpeed => MoveSpeed,
            StatsType.HP => HP,
            StatsType.MaxHP => MaxHP,
            StatsType.XP => XP,
            StatsType.MaxXP => MaxXP,
            StatsType.PickUpRange => PickUpRange,
            StatsType.AliveTime => AliveTime,
            _ => 0
        };
        set
        {
            switch (key)
            {
                case StatsType.Attack:
                    Attack = value;
                    break;
                case StatsType.Defence:
                    Defense = value;
                    break;
                case StatsType.MoveSpeed:
                    MoveSpeed = value;
                    break;
                case StatsType.HP:
                    HP = value;
                    break;
                case StatsType.MaxHP:
                    MaxHP = value;
                    break;
                case StatsType.XP:
                    XP = value;
                    break;
                case StatsType.MaxXP:
                    MaxXP = value;
                    break;
                case StatsType.PickUpRange:
                    PickUpRange = value;
                    break;
                case StatsType.AliveTime:
                    AliveTime = value;
                    break;
            }
        }
    }

    // �{�[�i�X�l
    protected float applyBonus(float currentValue, float value, BonusType type)
    {
        // �Œ�l�����Z
        if (BonusType.Bonus == type)
        {
            return currentValue + value;
        }
        // %�ŉ��Z
        else if (BonusType.Boost == type)
        {
            return currentValue * (1 + value);
        }
        return currentValue;
    }

    // �{�[�i�X�ǉ�
    protected void addBonus(BonusStats bonus)
    {
        float value = applyBonus(this[bonus.Key], bonus.Value, bonus.Type);
        // �ő�l���������
        if (StatsType.HP == bonus.Key)
        {
            value = Mathf.Clamp(value, 0, MaxHP);
        }
        else if (StatsType.XP == bonus.Key)
        {
            value = Mathf.Clamp(value, 0, MaxXP);
        }

        this[bonus.Key] = value;
    }

    // �R�s�[���ăf�[�^��Ԃ�
    public BaseStats GetCopy()
    {
        return (BaseStats)MemberwiseClone();
    }
}

