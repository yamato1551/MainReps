using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

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

    // �A���t�@�l�ݒ�
    public static void SetAlpha(Graphic graphic, float alpha)
    {
        // ���̃J���[
        Color color = graphic.color;

        // �A���t�@�l�ݒ�
        color.a = alpha;
        graphic.color = color;
    }

    // �A���t�@�l�ݒ�i�{�^���j
    public static void SetAlpha(Button button, float alpha)
    {
        // ���̃J���[
        SetAlpha(button.image, alpha);

        // �q�I�u�W�F�N�g�S�āFGetComponentsInChildren���q�I�u�W�F�N�g����w�肵���R���|�[�l���g��S�Ď擾����
        foreach (var item in button.GetComponentsInChildren<Graphic>())
        {
            SetAlpha(item, alpha);
        }
    }

    // �^�C���X�P�[���𖳎�����DOFade
    public static void DOfadeUpdate(Graphic graphic, float endValue, float duration, float delay = 0, Action action = null)
    {
        // DoTween���g�����t�F�[�h
        graphic.DOFade(endValue, duration)
            // �^�C���X�P�[��
            .SetUpdate(true)
            //�@�f�B���C
            .SetDelay(delay)
            // �I�����ɌĂяo���֐�
            .OnComplete(() =>
            {
                if (null != action) action();
            });
    }

    // DOFadeUpdate�̃{�^���o�[�W����
    public static void DOfadeUpdate(Button button, float endValue, float duration, float delay = 0, Action action = null)
    {
        // �{�^��
        DOfadeUpdate(button.image, endValue, duration, delay, action);
        // �q�I�u�W�F�N�g
        foreach(var item in button.GetComponentsInChildren<Graphic>())
        {
            DOfadeUpdate(item, endValue, duration, delay, action);
        }
    }
}