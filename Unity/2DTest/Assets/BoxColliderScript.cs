using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("����");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("�g���K�[");
    }
}
