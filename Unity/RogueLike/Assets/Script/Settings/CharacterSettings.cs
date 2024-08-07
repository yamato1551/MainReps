using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="CharacterSettings", menuName ="ScriptableObject/CharacterSetting")]
public class CharacterSettings : ScriptableObject
{
    // キャラクターデータ
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
    // プレイヤーに向かって進む
    TargetPlayer,
    // 一方校に進む
    TagetDirecion
}

public class CharacterStats : BaseStats
{
    // キャラクターのプレハブ
    public GameObject Prefab;
    // 初期装備武器ID
    public List<int> DefaultWeaponIds;
    // 装備可能武器ID
    public List<int> UsableWeaponIds;
    //　装備可能数
    public int UsableWeaponMax;
    // 移動タイプ
    public MoveType MoveType;

    // TODO アイテム追加
}
