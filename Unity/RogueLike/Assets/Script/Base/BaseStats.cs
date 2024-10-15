using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// 追加ステータス
public enum BonusType
{
    // 定数で追加
    Bonus,
    // %で追加
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
    // 武器生成用の拡張タイプ
    SpawnCount,
    SpawnTimerMin,
    SpawnTimerMax,
}

[Serializable]
public class BonusStats
{
    // 追加タイプ
    public BonusType Type;
    // 追加するプロパティ
    public StatsType Key;
    // 追加する値
    public float Value;
}

// キャラクターと武器で共通のステータス
public class BaseStats
{
    // Inspector上で表示されるタイトル
    public string Title;
    //データID
    public int Id;
    // 設定レベル
    public int Lv;
    // 名前
    public string Name;
    // 説明文
    [TextArea]public string Description;
    // 攻撃力
    public float Attack;
    // 防御力
    public float Defense;
    // HP
    public float HP;
    //最大HP
    public float MaxHP;
    // 経験値
    public float XP;
    // 最大経験値
    public float MaxXP;
    // 移動速度
    public float MoveSpeed;
    // 経験値取得距離
    public float PickUpRange;
    // 生存時間
    public float AliveTime;

    // StatsTypeとの紐づけ　インデクサを利用する
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

    // ボーナス値
    protected float applyBonus(float currentValue, float value, BonusType type)
    {
        // 固定値を加算
        if (BonusType.Bonus == type)
        {
            return currentValue + value;
        }
        // %で加算
        else if (BonusType.Boost == type)
        {
            return currentValue * (1 + value);
        }
        return currentValue;
    }

    // ボーナス追加
    protected void addBonus(BonusStats bonus)
    {
        float value = applyBonus(this[bonus.Key], bonus.Value, bonus.Type);
        // 最大値があるもの
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

    // コピーしてデータを返す
    public BaseStats GetCopy()
    {
        return (BaseStats)MemberwiseClone();
    }
}

