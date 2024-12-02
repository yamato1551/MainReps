using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    // スクリプト有効時に呼び出される
    private void OnEnable()
    {
        controls.Enable();
    }

    // スクリプト無効時に呼び出される
    private void OnDisable()
    {
        controls.Disable();
    }
}
