using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// �Q�[���Ŏg�����ʏ������܂Ƃ߂��֗��N���X
public static class Utils
{
    // �b����0:00�̕�����ɕϊ�
    public static string GetTextTimer(float timer)
    {
        int seconds = (int)timer % 60;
        int minutes = (int)timer / 60;
        return minutes.ToString() + ":" + seconds.ToString("00");
    }

    // �����蔻��̂���^�C�����ǂ������ׂ�
    public static bool IsColliderTile(Tilemap tilemapCollider, Vector2 position)
    {
        // �Z���ʒu�ɕϊ�
        Vector3Int cellPosition = tilemapCollider.WorldToCell(position);

        // �����蔻�肠��
        if (tilemapCollider.GetTile(cellPosition))
        {
            return true;
        }
        return false;
    }
}