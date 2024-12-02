using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    // �X�N���v�g�L�����ɌĂяo�����
    private void OnEnable()
    {
        controls.Enable();
    }

    // �X�N���v�g�������ɌĂяo�����
    private void OnDisable()
    {
        controls.Disable();
    }
}
