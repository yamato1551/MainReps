using UnityEngine;

public class WeaponVisualController : MonoBehaviour
{
    [SerializeField] private Transform[] gunTransforms;

    [SerializeField] private Transform pistol;
    [SerializeField] private Transform revolver;
    [SerializeField] private Transform autoRifile;
    [SerializeField] private Transform shotgun;
    [SerializeField] private Transform rifle;

    private Transform currentGun;

    [Header("Left hand IK")]
    [SerializeField] private Transform leftHand;

    private void Start()
    {
        SwitchOn(pistol);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
           SwitchOn(pistol);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchOn(revolver);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SwitchOn(autoRifile);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SwitchOn(shotgun);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            SwitchOn(rifle);

    }

    /// <summary>
    /// �e��̐؂�ւ�
    /// </summary>
    /// <param name="gunTransfome"></param>
    private void SwitchOn(Transform gunTransfome)
    {
        SwitchOffGuns();
        gunTransfome.gameObject.SetActive(true);
        currentGun = gunTransfome;

        AttachLeftHand();
    }

    /// <summary>
    /// �e��̏�����
    /// </summary>
    private void SwitchOffGuns()
    {
        // ���ׂĂ̏e��������Ȃ�����
        for (int i = 0;i < gunTransforms.Length; i++)
        {
            gunTransforms[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ����̈ʒu����
    /// </summary>
    private void AttachLeftHand()
    {
        Transform targetTransform = currentGun.GetComponentInChildren<LeftHandTargetTransform>().transform;

        leftHand.localPosition = targetTransform.localPosition;
        leftHand.localRotation = targetTransform.localRotation;
    }

}
