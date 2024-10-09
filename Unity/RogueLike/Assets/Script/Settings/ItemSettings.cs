using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemSettings", menuName = "ScriptableObject/ItemSettings")]
public class ItemSettings : ScriptableObject
{
    // �A�C�e���f�[�^
    public List<ItemData> datas;

    static ItemSettings instance;
    public static ItemSettings Instance
    {
        get
        {
            if (!instance)
            {
                instance = Resources.Load<ItemSettings>(nameof(ItemSettings));
            }
            return instance;
        }
    }
    // ���X�g��ID����f�[�^����������
    public ItemData Get(int id)
    {
        return (ItemData)datas.Find(item => item.Id == id).GetCopy();
    }
}

[System.Serializable]
public class ItemData
{
    public string Title;

    // �ŗLID
    public int Id;

    // �A�C�e����
    public string Name;

    // ����
    [TextArea] public string Description;

    // �A�C�R��
    public Sprite Icon;

    // �{�[�i�X
    public List<BonusStats> Bonuses;

    // �R�s�[�����f�[�^��n��
    public ItemData GetCopy()
    {
        return (ItemData)MemberwiseClone();
    }
}
