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

    //[Space] // インスペクター上にスペースを入れる
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

    // 毎秒固定で呼ばれるupdate 呼ばれる頻度はprojectSettingsで設定できる
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
        // 向く方向ベクトル計算
        Vector3 direction = aimTransform.position - towerTransform.position;
        direction.y = 0;
        // 向く方向の計算
        Quaternion targetRotaion = Quaternion.LookRotation(direction);
        // targetRotaionに向くための処理、towerRotaionSpeedにて回転速度を制御する
        towerTransform.rotation = Quaternion.RotateTowards(towerTransform.rotation, targetRotaion, towerRotaionSpeed);
    }

    private void ApplyBodyRotaion()
    {
        // 左右回転
        transform.Rotate(0, horizontalInput * rotationSpeed, 0);
    }

    private void ApplyMovement()
    {
        Vector3 movement = transform.forward * moveSpeed * verticalInput;
        // 前後移動
        rb.linearVelocity = movement;
    }

    private void UpdateAim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        // ray:マウス位置に出ているraycastのこと
        // out hit:raycastが当たっている場所を格納する
        // Mathf.Infinity：raycastの距離、今回は無限
        // 判定したいレイヤー
        if (Physics.Raycast(ray ,out hit, Mathf.Infinity, whatIsAimMask))
        {
            
            float fixedY = aimTransform.position.y;
            aimTransform.position = new Vector3 (hit.point.x, fixedY, hit.point.z);
        }
    }
}
