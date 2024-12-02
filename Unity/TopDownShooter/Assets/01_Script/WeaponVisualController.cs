using UnityEngine;

public class WeaponVisualController : MonoBehaviour
{
    [SerializeField] private Transform[] gunTransforms;

    [SerializeField] private Transform pistol;
    [SerializeField] private Transform revolver;
    [SerializeField] private Transform autoRifile;
    [SerializeField] private Transform shotgun;
    [SerializeField] private Transform rifle;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
           SwicthOn(pistol);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwicthOn(revolver);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SwicthOn(autoRifile);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SwicthOn(shotgun);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            SwicthOn(rifle);

    }

    // �e��̐؂�ւ�
    private void SwicthOn(Transform transform)
    {
        SwitchOffGuns();
        transform.gameObject.SetActive(true);
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

}
