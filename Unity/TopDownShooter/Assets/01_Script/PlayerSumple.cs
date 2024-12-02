using UnityEngine;

public class PlayerSumple : MonoBehaviour
{
    [Header("Gun data")]
    [SerializeField] private Transform gunPoint;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Movement data")]
    [SerializeField]private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private Rigidbody rb;
    private float verticalInput;
    private float horizontalInput;

    [Header("Tower data")]
    [SerializeField] private Transform towerTransform;
    [SerializeField] private float towerRotaionSpeed;

    //[Space] // �C���X�y�N�^�[��ɃX�y�[�X������
    [Header("Aim data")]
    [SerializeField] private LayerMask whatIsAimMask;
    [SerializeField] private Transform aimTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAim();
        CheckInputs();
    }

    private void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            shoot();
        }
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        if (verticalInput < 0)
        {
            horizontalInput = -Input.GetAxis("Horizontal");
        }
    }

    // ���b�Œ�ŌĂ΂��update �Ă΂��p�x��projectSettings�Őݒ�ł���
    private void FixedUpdate()
    {
        ApplyMovement();
        ApplyBodyRotaion();
        ApplyTowerRotaion();
    }

    private void shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
        bullet.GetComponent<Rigidbody>().linearVelocity = gunPoint.forward * bulletSpeed;
        Destroy(bullet, 7);

    }

    private void ApplyTowerRotaion()
    {
        // tankTower.LookAt(aimTransform);
        // ���������x�N�g���v�Z
        Vector3 direction = aimTransform.position - towerTransform.position;
        direction.y = 0;
        // ���������̌v�Z
        Quaternion targetRotaion = Quaternion.LookRotation(direction);
        // targetRotaion�Ɍ������߂̏����AtowerRotaionSpeed�ɂĉ�]���x�𐧌䂷��
        towerTransform.rotation = Quaternion.RotateTowards(towerTransform.rotation, targetRotaion, towerRotaionSpeed);
    }

    private void ApplyBodyRotaion()
    {
        // ���E��]
        transform.Rotate(0, horizontalInput * rotationSpeed, 0);
    }

    private void ApplyMovement()
    {
        Vector3 movement = transform.forward * moveSpeed * verticalInput;
        // �O��ړ�
        rb.linearVelocity = movement;
    }

    private void UpdateAim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        // ray:�}�E�X�ʒu�ɏo�Ă���raycast�̂���
        // out hit:raycast���������Ă���ꏊ���i�[����
        // Mathf.Infinity�Fraycast�̋����A����͖���
        // ���肵�������C���[
        if (Physics.Raycast(ray ,out hit, Mathf.Infinity, whatIsAimMask))
        {
            
            float fixedY = aimTransform.position.y;
            aimTransform.position = new Vector3 (hit.point.x, fixedY, hit.point.z);
        }
    }
}
