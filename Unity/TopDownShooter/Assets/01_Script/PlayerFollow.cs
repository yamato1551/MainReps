using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] private Transform Player;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z-5); 

    }
}
