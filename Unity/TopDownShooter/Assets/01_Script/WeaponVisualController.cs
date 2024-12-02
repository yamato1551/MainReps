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

    // èeäÌÇÃêÿÇËë÷Ç¶
    private void SwicthOn(Transform transform)
    {
        SwitchOffGuns();
        transform.gameObject.SetActive(true);
    }

    /// <summary>
    /// èeäÌÇÃèâä˙âª
    /// </summary>
    private void SwitchOffGuns()
    {
        // Ç∑Ç◊ÇƒÇÃèeäÌÇå©Ç¶Ç»Ç≠Ç∑ÇÈ
        for (int i = 0;i < gunTransforms.Length; i++)
        {
            gunTransforms[i].gameObject.SetActive(false);
        }
    }

}
