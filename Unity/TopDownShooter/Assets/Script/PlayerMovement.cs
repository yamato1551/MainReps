using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls controls;
    private CharacterController characterController;
    private Animator animator;

    [Header("Movement info")]
    [SerializeField]private float walkSpeed;
    private Vector3 movementDirection;
    [SerializeField] private float gravityScale = 9.81f;
    private float verticalVelocity;

    [Header("Aim info")]
    [SerializeField] private Transform aim;
    [SerializeField] private LayerMask aimLayerMask;
    private Vector3 lookingDirection;

    public Vector2 moveInput;
    public Vector2 aimInput;

    // Startよりも前に1度呼び出される
    private void Awake()
    {
        controls = new PlayerControls();

        // Fireに対応するボタンが押されたらShoot()関数が呼ばれる
        controls.Character.Fire.performed += context => Shoot();
        // Movementに対応するボタンが押されたらmoveInputに対応する入力値を返却する -1~1
        controls.Character.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
        // Movementに対応するボタンが離されたらmoveInputに0を返却する
        controls.Character.Movement.canceled += context => moveInput = Vector2.zero;

        // Aimに対応するボタンが離されたらaimInputに入力値を返却する -1~1
        controls.Character.Aim.performed += context => aimInput = context.ReadValue<Vector2>();
        // Aimに対応するボタンが離されたらaimInputに0を返却する
        controls.Character.Aim.canceled += context => aimInput = Vector2.zero;
    }

    private void Start()
    {
        // アタッチされているオブジェクトから取得
        characterController = GetComponent<CharacterController>();
        // 子要素になっているオブジェクトから取得
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        ApplyMovement();

        AimTowardsMouse();

        AnimatorController();
    }

    private void AnimatorController()
    {
        // 正規化した左右を取得する 左=-1/右=1
        float xVelocity = Vector3.Dot(movementDirection.normalized, transform.right);
        // 正規化した前後を取得する 後=-1/前=1
        float zVelocity = Vector3.Dot(movementDirection.normalized, transform.forward);

        // Animatorのx/zVelocityに上で設定したx/zVelocityを設定する
        // .1f = 0.1秒かけて滑らかに値を変化させる Time.deltaTimeによってフレームレートに依存しない値で変化させる
        animator.SetFloat("xVelocity", xVelocity, .1f, Time.deltaTime);
        animator.SetFloat("zVelocity", zVelocity, .1f, Time.deltaTime);
    }

    /// <summary>
    /// RayCastを利用したマウス位置からの向く方向算出
    /// </summary>
    private void AimTowardsMouse()
    {
        // カメラ位置からraycastを照射
        Ray ray = Camera.main.ScreenPointToRay(aimInput);

        // raycastが指定したLayerMaskに当たっているかどうかを判定
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, aimLayerMask))
        {
            // raycastが当たった個所 - プレイヤーの位置
            lookingDirection = hitInfo.point - transform.position;
            lookingDirection.y = 0f;
            // 方向ベクトルを正規化し、最大の大きさを1にする
            // これにより正確な方向ベクトルを算出することが可能になる
            lookingDirection.Normalize();

            transform.forward = lookingDirection;

            // aimオブジェクトの位置をrayが当たった位置に移動させる
            aim.position = new Vector3(hitInfo.point.x,transform.position.y, hitInfo.point.z);
        }
    }

    /// <summary>
    /// 移動処理
    /// </summary>
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

    /// <summary>
    /// 重力をかける処理
    /// </summary>
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

    /// <summary>
    /// 弾丸発射処理
    /// </summary>
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
