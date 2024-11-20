using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls controls;
    private CharacterController characterController;

    [Header("Movement info")]
    [SerializeField]private float walkSpeed;
    private Vector3 movementDirection;
    [SerializeField] private float gravityScale = 9.81f;

    private float verticalVelocity;

    public Vector2 moveInput;
    public Vector2 aimInput;

    // Startよりも前に1度呼び出される
    private void Awake()
    {
        controls = new PlayerControls();

        controls.Character.Fire.performed += context => Shoot();
        controls.Character.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
        controls.Character.Movement.canceled += context => moveInput = Vector2.zero;

        controls.Character.Aim.performed += context => aimInput = context.ReadValue<Vector2>();
        controls.Character.Aim.canceled += context => aimInput = Vector2.zero;
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        // 入力による値を格納する
        movementDirection = new Vector3(moveInput.x, 0, moveInput.y);

        ApplyGravity();

        // 方向ベクトルがある場合
        if (movementDirection.magnitude > 0)
        {
            // CharacterControllerに対して移動処理を入れる※遮蔽物や傾斜を考慮した移動ができる
            characterController.Move(movementDirection * Time.deltaTime * walkSpeed);
        }
    }

    private void ApplyGravity()
    {
        // プレイヤーが地面に設置していないこと
        if(characterController.isGrounded == false)
        {
            // 重力をかける　※9.81：重力のデフォ値
            verticalVelocity -= gravityScale * Time.deltaTime;
            movementDirection.y = verticalVelocity;
        }else
        {
            verticalVelocity = -.5f;
        }
    }

    private void Shoot()
    {
        Debug.Log("Shoot");
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
