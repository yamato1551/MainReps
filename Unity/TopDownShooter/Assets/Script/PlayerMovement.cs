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

    // Start�����O��1�x�Ăяo�����
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
        // ���͂ɂ��l���i�[����
        movementDirection = new Vector3(moveInput.x, 0, moveInput.y);

        ApplyGravity();

        // �����x�N�g��������ꍇ
        if (movementDirection.magnitude > 0)
        {
            // CharacterController�ɑ΂��Ĉړ����������遦�Օ�����X�΂��l�������ړ����ł���
            characterController.Move(movementDirection * Time.deltaTime * walkSpeed);
        }
    }

    private void ApplyGravity()
    {
        // �v���C���[���n�ʂɐݒu���Ă��Ȃ�����
        if(characterController.isGrounded == false)
        {
            // �d�͂�������@��9.81�F�d�͂̃f�t�H�l
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
