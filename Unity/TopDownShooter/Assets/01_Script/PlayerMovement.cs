using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player player;
    private PlayerControls controls;
    private CharacterController characterController;
    private Animator animator;

    [Header("Movement info")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    private float speed;
    private Vector3 movementDirection;
    [SerializeField] private float gravityScale = 9.81f;
    private float verticalVelocity;
    private bool isRunning;

    [Header("Aim info")]
    [SerializeField] private Transform aim;
    [SerializeField] private LayerMask aimLayerMask;
    private Vector3 lookingDirection;

    public Vector2 moveInput;
    public Vector2 aimInput;

    // Start�����O��1�x�Ăяo�����
    private void Awake()
    {
    }

    private void Start()
    {
        player = GetComponent<Player>();
        // �A�^�b�`����Ă���I�u�W�F�N�g����擾
        characterController = GetComponent<CharacterController>();
        // �q�v�f�ɂȂ��Ă���I�u�W�F�N�g����擾
        animator = GetComponentInChildren<Animator>();
        
        speed = walkSpeed;

        AssignInputEvents();
    }

    private void Update()
    {

        AimTowardsMouse();
        
        ApplyMovement();

        AnimatorController();
    }

    /// <summary>
    /// �A�j���[�V�����R���g���[���[�Őݒ肵���l�𓮂���
    /// </summary>
    private void AnimatorController()
    {
        // ���K���������E���擾���� ��=-1/�E=1
        float xVelocity = Vector3.Dot(movementDirection.normalized, transform.right);
        // ���K�������O����擾���� ��=-1/�O=1
        float zVelocity = Vector3.Dot(movementDirection.normalized, transform.forward);

        // Animator��x/zVelocity�ɏ�Őݒ肵��x/zVelocity��ݒ肷��
        // .1f = 0.1�b�����Ċ��炩�ɒl��ω������� Time.deltaTime�ɂ���ăt���[�����[�g�Ɉˑ����Ȃ��l�ŕω�������
        animator.SetFloat("xVelocity", xVelocity, .1f, Time.deltaTime);
        animator.SetFloat("zVelocity", zVelocity, .1f, Time.deltaTime);

        bool playRunAnimation = isRunning && movementDirection.magnitude > 0;
        animator.SetBool("isRunning", playRunAnimation);
    }

    /// <summary>
    /// RayCast�𗘗p�����}�E�X�ʒu����̌��������Z�o
    /// </summary>
    private void AimTowardsMouse()
    {
        // �J�����ʒu����raycast���Ǝ�
        Ray ray = Camera.main.ScreenPointToRay(aimInput);

        // raycast���w�肵��LayerMask�ɓ������Ă��邩�ǂ����𔻒�
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, aimLayerMask))
        {
            // raycast������������ - �v���C���[�̈ʒu
            lookingDirection = hitInfo.point - transform.position;
            lookingDirection.y = 0f;
            // �����x�N�g���𐳋K�����A�ő�̑傫����1�ɂ���
            // ����ɂ�萳�m�ȕ����x�N�g�����Z�o���邱�Ƃ��\�ɂȂ�
            lookingDirection.Normalize();

            transform.forward = lookingDirection;

            // aim�I�u�W�F�N�g�̈ʒu��ray�����������ʒu�Ɉړ�������
            aim.position = new Vector3(hitInfo.point.x,transform.position.y + 1, hitInfo.point.z);
        }
    }

    /// <summary>
    /// �ړ�����
    /// </summary>
    private void ApplyMovement()
    {
        // ���͂ɂ��l���i�[����
        movementDirection = new Vector3(moveInput.x, 0, moveInput.y);

        ApplyGravity();

        // �����x�N�g��������ꍇ
        if (movementDirection.magnitude > 0)
        {
            // CharacterController�ɑ΂��Ĉړ����������遦�Օ�����X�΂��l�������ړ����ł���
            characterController.Move(movementDirection * Time.deltaTime * speed);
        }
    }

    /// <summary>
    /// �d�͂������鏈��
    /// </summary>
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
    #region New Input System
    /// <summary>
    /// ���̓C�x���g����
    /// </summary>
    private void AssignInputEvents()
    {
        controls = player.controls;

        // Movement�ɑΉ�����{�^���������ꂽ��moveInput�ɑΉ�������͒l��ԋp���� -1~1
        controls.Character.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
        // Movement�ɑΉ�����{�^���������ꂽ��moveInput��0��ԋp����
        controls.Character.Movement.canceled += context => moveInput = Vector2.zero;

        // Aim�ɑΉ�����{�^���������ꂽ��aimInput�ɓ��͒l��ԋp����
        controls.Character.Aim.performed += context =>aimInput = context.ReadValue<Vector2>();
        // Aim�ɑΉ�����{�^���������ꂽ��aimInput��0��ԋp����
        controls.Character.Aim.canceled += context => aimInput = Vector2.zero;

        // Run�ɑΉ�����{�^���������ꂽ��isRunning��true��ԋp����
        controls.Character.Run.performed += context =>
        {
            if (movementDirection.magnitude > 0) {
                speed = runSpeed;
                isRunning = true;
            };
        };
        // Run�ɑΉ�����{�^���������ꂽ��isRunning��false��ԋp����
        controls.Character.Run.canceled += context =>
        {
            speed = walkSpeed;
            isRunning = false;
        };
    }
    #endregion
}
